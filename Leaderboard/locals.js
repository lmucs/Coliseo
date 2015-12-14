import _ from 'lodash';

const appConfig = {
  // App configuration
  appName: 'Coliseo',
  primaryColor: 'brown',
  accentColor: 'blue',
};

const englishLocals = {
  english: true,

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
  logout: 'Logout',
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

const spanishLocals = {
  spanish: true,
  regEmailError: 'El correo electrónico es inválido o ya fue usado',
  regUserError: 'El nombre de usuario es inválido o ya fue usado',
  regPasswordError: 'La contraseña no es lo suficientemente segura',
  regConfirmError: 'Las dos contraseñas no coinciden',
  loginError: 'O el nombre de usuario o la contraseña son inválidos',
  clientSideUserInvalid: 'El nombre de usuario solo puede contener caracteres' +
                         ' alfabéticos',
  clientSideEmailInvalid: 'La dirección proveída no es una dirección de ' +
                          'correo electrónico válida',

  // Application Errors
  userNotFound: 'El usuario es inexistente',

  // Success
  regSuccess: 'Inscripción exitosa',
  regSuccess2: 'Por favor, revise su correo electrónico para verificar su ' +
               'cuenta',

  // Info
  aboutColiseo: 'Videojuego con soporte de Oculus Rift creado en un semestre ' +
                'para CMSI 401 en LMU' +
                ' Consiste de un videojuego creado en Unity y una ' +
                'clasificación en Node.js',
  wikiLocation: 'La Wiki se encuentra en: <a class="wiki" ' +
                'href="https://github.com/lmucs/Coliseo/wiki">here</a>',
  graphicLocation: 'Gráfico del Coliseo hecho por <a class="freepik" ' +
                   'href="http://www.freepik.com/">Freepik</a> from ' +
                   '<a class="flaticon" href="http://www.flaticon.com">' +
                   'Flaticon</a> está bajo la licencia <a class="cc" ' +
                   'href="http://creativecommons.org/licenses/by/3.0/">' +
                   '"Creative Commons BY 3.0"</a>. Made with ' +
                   '<a class="logomaker" href="http://logomakr.com">' +
                   '"Logo Maker"</a>',

  // Navigation
  register: 'Inscripción' ,
  login: 'Iniciar sesión',
  logout: 'Cerrar sesión',
  leaderboard: 'Clasificación',
  userProfile: 'Perfil de usuario',

  // User profile

  highScoreTitle: 'Puntajes máximos:',
  biographyTitle: 'Biografía',

  // Definitions

  usernameTitle: 'Nombre de usuario',
  scoreTitle: 'Puntuación',
  rankTitle: 'Categoría',
  emailTitle: 'Correo electrónico',
  passwordTitle: 'Contraseña',
  confirmPasswordTitle: 'Confirmar contraseña',
  submit: 'Enviar',
};

export default (req, res, next) => {
  if (req.query.language) {
    req.session.language = req.query.language;
  }
  _.assign(res.locals, appConfig);
  if (req.session.language === 'spanish') {
    _.assign(res.locals, spanishLocals);
  } else {
    _.assign(res.locals, englishLocals);
  }
  next();
};
