import Sequelize from 'sequelize';
import config from 'config';
import _ from 'lodash';
import log from '../logging';
import path from 'path';
import fs from 'fs';

const dbConfig = config.get('Database');

const sequelize = new Sequelize(dbConfig.db, dbConfig.user, dbConfig.password,
  _.merge(dbConfig, {
    define: {
      allowNull: false,
    },
  })
);

const model = file => sequelize.import(path.join(__dirname, file));

export const User = model('user.js');

export default () => sequelize.sync(_.merge(dbConfig.syncOptions, {
  logging: log.info,
})).then(() => {
  log.info("Database initialized");
});
