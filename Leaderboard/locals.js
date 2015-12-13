import _ from 'lodash';

export const locals = {
  // App configuration
  appName: 'Coliseo',
  primaryColor: 'indigo',
  accentColor: 'pink',

  // Confirmation  errors
  regEmailError: 'The email was not valid or is already taken',
  regUserError: 'The username was not valid or was already taken',
  regPasswordError: 'The password was not long enough',
  regConfirmError: 'The two passwords did not match',
  loginError: 'Either the username or the password were invalid',

  // User profile errors
  userProfileNotFound: 'The requested user profile does not exist',
};
export default app => _.assign(app.locals, locals);
