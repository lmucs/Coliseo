import express from 'express';
import * as path from 'path';
import favicon from 'serve-favicon';
import cookieParser from 'cookie-parser';
import bodyParser from 'body-parser';
import hbs from 'hbs';
import _ from 'lodash';

import routes from './routes';
import setupLocals from './locals'; // Populate our app with custom locals
import secret from './secret';
import setupSession from './session';
import {User, Score} from './database';
import {calculateSaltHash} from './cryptography';

const app = express();
setupLocals(app);
hbs.localsAsTemplateData(app);
import './handlebars_helpers';

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'hbs');

// uncomment after placing your favicon in /public
app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
setupSession(app);
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}));
app.use(cookieParser(secret));
app.use(express.static(path.join(__dirname, 'public')));

app.use('/', routes);

// catch 404 and forward to error handler
app.use((req, res, next) => {
  const err = new Error(`Not Found: ${req.originalUrl}`);
  err.status = 404;
  next(err);
});

// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
  app.use((err, req, res, next) => {
    res.status(err.status || 500);
    res.render('error', {
      message: err.message,
      error: err,
    });
    console.error(err.stack);
  });
  // Setup test user and password
  (async () => {
    const testUser = await User.create({
      username: 'test',
      email: 'example@example.com',
      password: calculateSaltHash('test'),
      biography: `
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis pharetra ex
consequat velit finibus dapibus. Morbi vulputate sapien et purus placerat,
eget tristique urna hendrerit. Mauris et orci ullamcorper, porta enim
quis, consectetur dolor. Sed ullamcorper, ligula vel maximus feugiat, ex
turpis facilisis risus, eu commodo lacus risus id ante. Curabitur bibendum
ante lorem, nec ullamcorper nisi placerat ac. Aliquam erat volutpat.
Praesent facilisis, lorem eget accumsan convallis, tortor diam cursus
orci, eu semper tortor odio id arcu. Pellentesque non nibh elit. Vivamus
eget arcu lorem. Sed sit amet interdum sapien.
`.trim(),

    });
    const scores = [];
    for (let i = 0; i < 100; i++) {
      scores.push({score: _.random(9000)});
    }
    await Score.bulkCreate(scores);
    testUser.addScores(await Score.findAll());
  })();
}

// production error handler
// no stacktraces leaked to user
app.use((err, req, res, next) => {
  res.status(err.status || 500);
  res.render('error', {
    message: err.message,
    error: {},
  });
});

export default app;
