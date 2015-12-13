import express from 'express';
import users from './users';
import leaderboard from './leaderboard';

const router = express.Router();

/* GET home page. */
router.get('/', (req, res, next) => {
  res.render('index');
});

router.use('/users', users);
router.use('/leaderboard', leaderboard);

export default router;
