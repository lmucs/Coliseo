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
  const scores = _(topScoresRaw)
    .pluck('dataValues')
    .map(obj => ({
      score: obj.score,
      username: obj.user.dataValues.username,
      userid: obj.user.dataValues.id,
    })).value();
  res.render('leaderboard', {scores});
};

router.get('/', asyncWrap(handleLeaderboard));

router.get('/user', (req, res, next) => {
  res.render('user');
});

export default router;
