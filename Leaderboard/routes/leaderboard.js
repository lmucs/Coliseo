import express from 'express';
import _ from 'lodash';

import {User, Score} from '../database';
import {asyncWrap} from '../helper';
const router = express.Router();

const handleLeaderboard = async (req, res, next) => {
  const topScoresRaw = await Score.findAll({
    include: [{model: User, required: true}],
    order: [['score', 'DESC']],
    limit: 10,
  });

  // Sequelize is dumb and doesn't have an obvious way to select ONLY the data
  // that you're interested in in a query. So we have to do some ugly
  // functional programming on the raw query result to fix it.
  const scores = _.map(topScoresRaw, obj => ({
      score: obj.dataValues.score,
      username: obj.dataValues.user.dataValues.username,
      userid: obj.dataValues.user.dataValues.id,
    }));
  res.render('leaderboard', {scores});
};

router.get('/', asyncWrap(handleLeaderboard));

router.get('/user', (req, res, next) => {
  res.render('user');
});

export default router;
