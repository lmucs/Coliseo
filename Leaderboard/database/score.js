export default (sequelize, DataTypes) => sequelize.define('score', {
  score: {
    type: DataTypes.INTEGER,
    allowNull: false,
    validate: {
      min: 0,
    },
  },
});
