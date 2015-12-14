export default (sequelize, DataTypes) => sequelize.define('user', {
  username: {
    type: DataTypes.STRING,
    unique: true,
    allowNull: false,
    validate: {
      isAlpha: true,
    },
  },
  password: {
    type: DataTypes.STRING(750),
    allowNull: false,
  },
  email: {
    type: DataTypes.STRING,
    allowNull: false,
    validate: {
      isEmail: true,
    },
  },
  biography: {
    type: DataTypes.TEXT,
  },
});
