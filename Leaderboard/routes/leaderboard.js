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
  const user = await User.findOne({
    where: {
      username: req.params.username,
    },
  });
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
    console.log(res.locals.isOwnUser);
    res.locals.isOwnUser = true;
  }
  return res.render('user', userView);
};

const handlePostUser = async (req, res, next) => {
  if (req.session.user.username === req.params.username) {
    console.log(req.body);
    const user = await User.findOne({
      where: {username: req.params.username.toLowerCase(),}}
    );
    user.biography = req.body.biography;
    await user.save();
  }
  return res.redirect('/leaderboard/user/' + req.params.username);
};

router.get('/user/:username', asyncWrap(handleGetUser));
router.post('/user/:username', asyncWrap(handlePostUser));

export default router;
