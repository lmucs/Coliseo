export default (sequelize, DataTypes) => sequelize.define('user', {
  username: {
    type: DataTypes.STRING,
    unique: true,
  },
  password: {
    type: DataTypes.STRING,
  },
  email: {
    type: DataTypes.STRING,
    validate: {
      isEmail: true,
    },
  },
  verifiedEmail: {
    type: DataTypes.BOOLEAN,
    defaultValue: false,
  },
  registrationTime: {
    type: DataTypes.DATE,
    defaultValue: DataTypes.NOW,
  },
  userID: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    primaryKey: true,
  },
});
