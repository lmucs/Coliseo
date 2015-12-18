import express from 'express';
import _ from 'lodash';
import js2xmlparser from 'js2xmlparser';
import auth from 'basic-auth';

import v1 from './v1';
import {User, Score} from '../../database';
import {asyncWrap} from '../../helper';
import {UserNotFoundError} from '../../errors';
import {calculateHash} from '../../cryptography';

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

  res.set('Content-Type', 'text/xml');
  return res.send(js2xmlparser('user', sanitizedUser));
};

const getAuthToken = async (req, res, next) => {
  const user = auth(req);
  const userModel = await User.findOne({
    where: {
      username: user.name.toLowerCase(),
    }
  });
  if (userModel === null) {
    console.log('Error finding user');
    res.status(401);
    return res.send();
  }
  let [iterations, salt, hash] = userModel.password.split('$');
  iterations = parseInt(iterations, 10);
  if (hash !== calculateHash(user.pass, salt, iterations)) {
    console.log('Error confirming password');
    return res.status(401).send();
  } else {
    console.log('User is valid!!!!!!!!!!!');
    res.send();
  }
};

router.get('/auth', asyncWrap(getAuthToken));

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
      scoreId: obj.get('id'),
      username: obj.get('user').get('username'),
    }));
  }
  res.set('Content-Type', 'text/xml');
  return res.send(js2xmlparser('leaderboard', {scores}));
};

router.get('/scores/:username?', asyncWrap(getScores));

const postScore = async (req, res, next) => {
  const user = auth(req);
console.log(user);
  const userModel = await User.findOne({
    where: {
      username: user.name.toLowerCase(),
    }
  });
  if (userModel === null) {
    console.log('Error finding user');
    res.status(401);
    return res.send();
  }
  let [iterations, salt, hash] = userModel.password.split('$');
  iterations = parseInt(iterations, 10);
  if (hash !== calculateHash(user.pass, salt, iterations)) {
    console.log('Error confirming password');
    return res.status(401).send();
  } else {
    const {score} = req.body;
    const intScore = parseInt(score); // so we have an int
    console.log(intScore);
    const scoresAbove = await Score.findAll({
      include: [{model: User, required: true}],
      where: {
        score: {
          gte: intScore
        },
      },
      order: [['score', 'ASC']],
      limit: 5,
    });
    const scoresBelow = await Score.findAll({
      include: [{model: User, required: true}],
      where: {
        score: {
          lt: intScore,
        },
      },
      order: [['score', 'DESC']],
      limit: 5,
    });
    const newScore = await userModel.createScore({score});
    const scoresAround = scoresAbove
      .reverse()
      .concat(newScore, scoresBelow)
      .map(obj => {
        const user = obj.get('user') || userModel;
        return {
          score: obj.get('score'),
          username: user.get('username'),
          scoreId: obj.get('id'),
        };
      });
    res.set('Content-Type', 'text/xml');
    return res.send(js2xmlparser('scoresAround', {
      score: scoresAround,
      submittedScoreId: newScore.get('id'),
    }));
  }
};

router.post('/scores/', asyncWrap(postScore));

export default router;
