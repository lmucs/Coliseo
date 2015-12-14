import Sequelize from 'sequelize';
import config from 'config';
import _ from 'lodash';
import * as path from 'path';
import * as fs from 'fs';

const dbConfig = config.get('Database');

export const sequelize = new Sequelize(
  dbConfig.db,
  dbConfig.user,
  dbConfig.password,
  _.merge(dbConfig, {
    define: {
      allowNull: false,
    },
    logging: console.log,
  })
);

const model = file => sequelize.import(path.join(__dirname, file));

export const User = model('user.js');
export const Score = model('score.js');

User.hasMany(Score, {
  onDelete: 'cascade',
});
Score.belongsTo(User);

export default async () => {
  await sequelize.sync(_.merge(dbConfig.syncOptions, {
    logging: console.log,
  }));
  console.log('Database initialized');
};
