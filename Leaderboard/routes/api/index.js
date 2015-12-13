import express from 'express';

import v1 from './v1';
import {User, Score} from '../../database';
import {asyncWrap} from '....//helper';

const router = express.Router();

// gethighscoreforuser
// postscore

router.get('/user/:username'); //user info

const getScores = async (req, res, next) => {
  if (req.params.username !== null) {
    const user = await User.findOne({
      where: {
        username: req.params.usernamePattern.toLowerCase(),
      },
    });
  }
};
router.get('/scores/:username?', asyncWrap(getScores));

router.post('/scores/:username');
