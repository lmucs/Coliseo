import Sequelize from 'sequelize';
import config from 'config';
import _ from 'lodash';

const dbConfig = config.get('Database');

const sequelize = new Sequelize(dbConfig.db, dbConfig.user, dbConfig.password,
  _.merge(dbConfig, {
    define: {
      allowNull: false,
    },
  })
);

export const User = sequelize.define('user', {
  username: {
    type: Sequelize.STRING,
    unique: true,
  },
  password: {
    type: Sequelize.STRING,
  },
  email: {
    type: Sequelize.STRING,
    validate: {
      isEmail: true,
    },
  },
  verifiedEmail: {
    type: Sequelize.BOOLEAN,
    defaultValue: false,
  },
  registrationTime: {
    type: Sequelize.DATE,
    defaultValue: Sequelize.NOW,
  },
  userID: {
    type: Sequelize.INTEGER,
    autoIncrement: true,

  },
});

export default sequelize;
