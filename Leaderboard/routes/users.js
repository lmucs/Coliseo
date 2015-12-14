import express from 'express';
import _ from 'lodash';

import {User} from '../database';
import {asyncWrap} from '../helper';
import {calculateHash, calculateSaltHash} from '../cryptography';

const router = express.Router();
const patterns = {
  usernamePattern: '^[A-Za-z]+$',
  passwordPattern: '^.{12,}$',
};
const usernameRegex = new RegExp(patterns.usernamePattern);
const passwordRegex = new RegExp(patterns.passwordPattern);

/* Utility Functions */

const setErrors = (user, password, confirm, email) => ({
  isUserError: user,
  isPasswordError: password,
  isConfirmError: confirm,
  isEmailError: email,
});

/* Handlers */

const handleRegistrationError = (errors, req, res, next) =>
  res.status(400)
     .render('register', _.merge(errors, patterns, req.body));

const handleRegistration = async (req, res, next) => {
  const {username, password, confirm, email} = req.body;

  let errors = setErrors(
    !usernameRegex.test(username),
    !passwordRegex.test(password),
    password !== confirm,
    false // TODO
  );
  if (_.any(errors)) {
    console.log('User registration validation failed');
    return handleRegistrationError(errors, req, res, next);
  } else {
    let userModel;
    try {
      userModel = await User.create({
        username: username.toLowerCase(),
        password: calculateSaltHash(password),
        email: email,
      });
    } catch (e) {
      if (e.name !== 'SequelizeValidationError') {
        next(e);
      }

      console.error('Error inserting user into database', e);
      const errorFields = _.pluck(e.errors, 'path');
      errors = setErrors(
        errorFields.indexOf('username') >= 0,
        errorFields.indexOf('password') >= 0,
        false, // we aren't passing in password confirmation to the database
        errorFields.indexOf('email') >= 0
      );
      return handleRegistrationError(errors, req, res, next);
    }
    req.session.regenerate((err) => {
      if (err) {
        return next(err);
      } else {
        req.session.user = userModel;
        req.session.authenticated = true;
        return res.redirect('/');
      }
    });
  }
};

/* GET users listing. */
router.get('/', (req, res, next) => {
  res.send('respond with a resource');
});

router
  .route('/register')
    .get((req, res, next) => {
      // MDL will do client side verification of the input fields as long as we
      // give them the pattern - this way we can ensure client and server validation
      // are identical.
      return res.render('register', patterns);
    }).post(asyncWrap(handleRegistration));

const handleLogin = async (req, res, next) => {
  const userModel = await User.findOne({
    where: {
      username: req.body.username.toLowerCase(),
    }
  });
  if (userModel === null) {
    return res.status(401).render('login', {isLoginError: true});
  }
  let [iterations, salt, hash] = userModel.password.split('$');
  iterations = parseInt(iterations, 10);
  if (hash !== calculateHash(req.body.password, salt, iterations)) {
    return res.status(401).render('login', {isLoginError: true});
  } else {
    req.session.regenerate((err) => {
      if (err) {
        return next(err);
      } else {
        req.session.user = userModel;
        req.session.authenticated = true;
        return res.redirect('/');
      }
    });
  }
};
router
  .route('/login')
    .get((req, res, next) => {
      return res.render('login', patterns);
    }).post(asyncWrap(handleLogin));

router.get('/email-verify', (req, res, next) => {
  return res.render('email-verify');
});

router.get('/logout', (req, res, next) => {
  req.session.destroy();
  return res.redirect('/');
});

export default router;
