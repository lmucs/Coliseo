import express from 'express';
import _ from 'lodash';

import v1 from './v1';
import {User, Score} from '../../database';
import {asyncWrap} from '../../helper';

const router = express.Router();

// gethighscoreforuser
// postscore

router.get('/user/:username'); //user info

const getScores = async (req, res, next) => {
  let scores;
  if (req.params.username) {
    console.log(req.params.username);
    const user = await User.findOne({
      where: {
        username: req.params.usernamePattern.toLowerCase(),
      },
    });
    if (user === null) {
      const err = new Error(req.app.locals.userNotFound);
      err.status = 404;
      return next(err);
    }
    scores = user.getScores({scope: {
      order: [['score', 'DESC']],
    }});
  } else {
    const topScoresRaw = await Score.findAll({
      include: [{model: User, required: true}],
      order: [['score', 'DESC']],
    });
    scores = topScoresRaw.map(obj => ({
      score: obj.dataValues.score,
      username: obj.dataValues.user.dataValues.username,
    }));
  }
  res.json(scores);
};
router.get('/scores/:username?', asyncWrap(getScores));

router.post('/scores/:username');

export default router;
