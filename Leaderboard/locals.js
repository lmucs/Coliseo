import _ from 'lodash';

export const locals = {
  // App configuration
  appName: 'Coliseo',
  primaryColor: 'indigo',
  accentColor: 'pink',

  // Confirmation  errors
  regEmailError: 'The email is not valid or is already taken',
  regUserError: 'The username is not valid or is already taken',
  regPasswordError: 'The password is not strong enough',
  regConfirmError: 'The two passwords do not match',
  loginError: 'Either the username or the password are invalid',
  clientSideUserInvalid: 'Username must contain alphabetic characters only',
  clientSideEmailInvalid: 'Given address is not a valid email address',

  // Application Errors
  userNotFound: 'The requested user does not exist',

  // Success
  regSuccess: 'Registration succeeded',
  regSuccess2: 'Please check your email to verify your account',

  // Info
  aboutColiseo: 'An Oculus Rift game built in a semester for CMSI 401 at LMU.' +
                ' Consists of an Unity Game and a Node.js leaderboard server.',
  wikiLocation: 'The Wiki is located: <a class="wiki" ' +
                'href="https://github.com/lmucs/Coliseo/wiki">here</a>',
  graphicLocation: 'Coliseum graphic by <a class="freepik" ' +
                   'href="http://www.freepik.com/">Freepik</a> from ' +
                   '<a class="flaticon" href="http://www.flaticon.com">' +
                   'Flaticon</a> is licensed under <a class="cc" ' +
                   'href="http://creativecommons.org/licenses/by/3.0/">' +
                   '"Creative Commons BY 3.0"</a>. Made with ' +
                   '<a class="logomaker" href="http://logomakr.com">' +
                   '"Logo Maker"</a>',

  // Navigation
  register: 'Register',
  login: 'Login',
  leaderboard: 'Leaderboard',
  userProfile: 'User Profile',

  // User profile

  highScoreTitle: 'High Scores:',
  biographyTitle: 'Biography',

  // Definitions

  usernameTitle: 'Username',
  scoreTitle: 'Score',
  rankTitle: 'Rank',
  emailTitle: 'Email',
  passwordTitle: 'Password',
  confirmPasswordTitle: 'Confirm Password',
  submit: 'Submit',
};
export default app => _.assign(app.locals, locals);
