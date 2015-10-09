import express from 'express';
import path from 'path';
import favicon from 'serve-favicon';
import cookieParser from 'cookie-parser';
import bodyParser from 'body-parser';
import hbs from 'hbs';
import session from 'express-session';
import connectSessionSequelize from 'connect-session-sequelize';

import routes from './routes/index';
import users from './routes/users';
import setupLocals from './locals'; // Populate our app with custom locals
import {errorLogger, requestLogger} from './logging';
import secret from './secret';
import {sequelize as db} from './database';

const SequelizeStore = connectSessionSequelize(session.Store);
const app = express();
setupLocals(app);
hbs.localsAsTemplateData(app);
import './handlebars_helpers';

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'hbs');

// uncomment after placing your favicon in /public
//app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.use(session({
  secret,
  resave: false,
  saveUninitialized: false,
  store: new SequelizeStore({db,}),
}));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use(requestLogger);
app.use('/', routes);
app.use('/users', users);
app.use(errorLogger);

// catch 404 and forward to error handler
app.use((req, res, next) => {
  const err = new Error('Not Found');
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
  });
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
