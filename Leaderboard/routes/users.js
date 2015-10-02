import express from 'express';
import _ from 'lodash';
import {User} from '../database';
const router = express.Router();

const patterns = {
  usernamePattern: '^[A-Za-z]+$',
  passwordPattern: '^.{12,}$',
};

const usernameRegex = new RegExp(patterns.usernamePattern);
const passwordRegex = new RegExp(patterns.passwordPattern);

/* GET users listing. */
router.get('/', (req, res, next) => {
  res.send('respond with a resource');
});

router.route('/register').get((req, res, next) => {
  // MDL will do client side verification of the input fields as long as we
  // give them the pattern - this way we can ensure client and server validation
  // are identical.
  res.render('register', patterns);
}).post((req, res, next) => {
  const errors = {
    isUserError: !usernameRegex.test(req.body.username),
    isPasswordError: !passwordRegex.test(req.body.password),
    isConfirmError: req.body.password !== req.body.confirm,
    isEmailError: false, // TODO
  };
  if (_.any(errors)) {
    _.assign(res.locals, errors);
    res.status(400).render('register', _.merge(errors, patterns, req.body));
  } else {
    
  }
});

export default router;
