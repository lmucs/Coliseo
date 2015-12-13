import express from 'express';
import _ from 'lodash';

import {User, Score} from '../database';
import {asyncWrap} from '../helper';
import {UserNotFoundError} from '../errors';
const router = express.Router();

const handleLeaderboard = async (req, res, next) => {
  const topScoresRaw = await Score.findAll({
    include: [{model: User, required: true}],
    order: [['score', 'DESC']],
    limit: 10,
  });

  const scores = topScoresRaw.map(obj => ({
    score: obj.get('score'),
    username: obj.get('user').get('username'),
  }));
  return res.render('leaderboard', {scores});
};

router.get('/', asyncWrap(handleLeaderboard));

const handleGetUser = async (req, res, next) => {
  const user = await User.findOne({username: req.params.username});
  if (user === null) {
    let err = new UserNotFoundError();
    err.status = 404;
    return next(err);
  }
  const scores = await user.getScores({
    order: [['score', 'DESC']],
  }).map(score => score.get());
  const userView = user.get();
  userView.scores = scores;
  if (req.session.user && req.session.user.username === userView.username) {
    res.locals.isOwnUser = true;
  }
  return res.render('user', userView);
};

const handlePostUser = async (req, res, next) => {

};

router.get('/user/:username', asyncWrap(handleGetUser));

export default router;
