import express from 'express';
import _ from 'lodash';
import crypto from 'crypto';
import config from 'config';

import log from '../logging';
import {User} from '../database';
import {asyncWrap} from '../helper';

const {
  saltLength,
  iterationsBase,
  iterationsMax,
  hashLength,
  encoding
} = config.get('Crypto');

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

const calculateSaltHash = password => {
  const iterations = _.random(iterationsBase, iterationsMax);
  const salt = crypto.randomBytes(saltLength)
                     .toString(encoding);
  const hash = calculateHash(password, salt, iterations);
  return `${iterations}$${salt}$${hash}`;
};

const calculateHash = (password, salt, iterations) => {
  log.profile('Hash calculation');
  const hash = crypto.pbkdf2Sync(password, salt, iterations, hashLength)
                     .toString(encoding);
  log.profile('Hash calculation');
  return hash;
};

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
    log.warn('User registration validation failed');
    return handleRegistrationError(errors, req, res, next);
  } else {
    // TODO: make sure that no duplicates exist case insensitively
    try {
      await User.create({
        username: username,
        password: calculateSaltHash(password),
        email: email,
      });
    } catch (e) {
      if (e.name !== 'SequelizeValidationError') {
        throw e;
      }

      log.exception('Error inserting user into database', e);
      const errorFields = _.pluck(e.errors, 'path');
      errors = setErrors(
        errorFields.indexOf('username') >= 0,
        errorFields.indexOf('password') >= 0,
        false, // we aren't passing in password confirmation to the database
        errorFields.indexOf('email') >= 0
      );
      return handleRegistrationError(errors, req, res, next);
    }

    return res.redirect('email-verify');
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
  const userModel = await Users.find({username: req.username});
  const [iterations, salt, hash] = userModel.password.split('$');
  if (hash !== calculateHash(req.password, salt, iterations)) {
    return res.status(401).render('login', {isLoginError: true});
  } else {
    req.session.regenrate(function(err) {
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

export default router;
