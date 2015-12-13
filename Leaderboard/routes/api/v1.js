import express from 'express';
import _ from 'lodash';

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
    const err = new Error(req.app.locals.userNotFound);
    err.status = 404;
    return next(err);
  }
  const sanitizedUser = _.pick(user.get(), ['username', 'biography']);
  return res.json(sanitizedUser);
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
      const err = new UserNotFoundError();
      err.status = 404;
      return next(err);
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
  return res.json(scores);
};
router.get('/scores/:username?', asyncWrap(getScores));

router.post('/scores/:username'); // TODO!

export default router;
