import session from 'express-session';
import connectSessionSequelize from 'connect-session-sequelize';
import moment from 'moment';
import _ from 'lodash';

import {sequelize as db} from './database';
import secret from './secret';

const SequelizeStore = connectSessionSequelize(session.Store);
moment().format();

export default (app) => {
  app.use(session({
    secret,
    resave: false,
    store: new SequelizeStore({db,}),
  }));

  app.use((req, res, next) => {
    req.session.age = req.session.age || moment();
    const difference = moment().diff(req.session.age, 'minutes');
    if (difference > 30) {
      return req.session.destroy(next);
    } else if (difference > 15) {
      const session = _.clone(req.session);
      req.session.regenerate((err) => {
        _.merge(req.session, session);
        return next(err);
      });
    } else {
      next();
    }
  });
};
