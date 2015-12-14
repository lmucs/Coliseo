import _ from 'lodash';

const locals = {
  // App configuration
  appName: 'Fight or Flight',
  primaryColor: 'brown',
  accentColor: 'blue',

  // Confirmation  errors
  regEmailError: 'The email was not valid or is already taken',
  regUserError: 'The username was not valid or was already taken',
  regPasswordError: 'The password was not long enough',
  regConfirmError: 'The two passwords did not match',
  loginError: 'Either the username or the password were invalid',
};
export default app => _.assign(app.locals, locals);
