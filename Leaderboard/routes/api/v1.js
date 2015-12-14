import express from 'express';
import _ from 'lodash';
import js2xmlparser from 'js2xmlparser';

import v1 from './v1';
import {User, Score} from '../../database';
import {asyncWrap} from '../../helper';
import {UserNotFoundError} from '../../errors';

const router = express.Router();

const getUser = async (req, res, next) => {
  const user = await User.findOne({
    where: {
      username: req.params.username.toLowerCase(),
    },
  });
  if (user === null) {
    return next(new UserNotFoundError());
  }
  const sanitizedUser = _.pick(user.get(), ['username', 'biography']);
  console.log(sanitizedUser);

  res.set('Content-Type', 'text/xml');
  return res.send(js2xmlparser('user', sanitizedUser));
};

router.get('/user/:username', asyncWrap(getUser));

const getScores = async (req, res, next) => {
  let scores;
  if (req.params.username) {
    const user = await User.findOne({
      where: {
        username: req.params.username.toLowerCase(),
      },
    });
    if (user === null) {
      return next(new UserNotFoundError());
    }
    scores = await user.getScores({
      order: [['score', 'DESC']],
    }).map(score => ({
      username: user.get('username'),
      score: score.get('score')
    }));
  } else {
    const topScoresRaw = await Score.findAll({
      include: [{model: User, required: true}],
      order: [['score', 'DESC']],
    });
    scores = topScoresRaw.map(obj => ({
      score: obj.get('score'),
      username: obj.get('user').get('username'),
    }));
  }
  res.set('Content-Type', 'text/xml');
  return res.send(js2xmlparser('scores', {scores}));
};
router.get('/scores/:username?', asyncWrap(getScores));

router.post('/scores/:username'); // TODO!

export default router;
