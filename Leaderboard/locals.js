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
                   '"Creative Commons BY 3.0"</a>. Hecho con ' +
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

const arabicLocals = {
  arabic: true,
  regEmailError: 'إن البريد الإلكتروني غير صالح أو اتخذت بالفعل',
  regUserError: 'اسم المستخدم غير صالح أو اتخذت بالفعل',
  regPasswordError: 'كلمة المرور ليست قوية بما فيه الكفاية',
  regConfirmError: 'إن كلمات السر اثنين لا تتطابق',
  loginError: 'إما اسم المستخدم أو كلمة المرور غير صالحة',
  clientSideUserInvalid: 'يجب أن اسم المستخدم يحتوي على أحرف أبجدية فقط',
  clientSideEmailInvalid: 'نظرا عنوان ليس عنوان بريد إلكتروني صالح',

  userNotFound: 'لا وجود للمستخدم طلب',

  regSuccess: 'التسجيل نجحت',
  regSuccess2: 'الرجاء التحقق من بريدك الالكتروني للتحقق من حسابك،',

  aboutColiseo: 'لعبة كوة المتصدع التي بنيت في فصل دراسي لCMSI 401 في LMU.' +
                 'يتكون من لعبة الوحدة وخادم المتصدرين Node.js.',
  wikiLocation: '<a class="wiki href="https://github.com/lmucs/Coliseo/wiki">هنا</a> إن يكي يقع',
  graphicLocation: 'Coliseum graphic by <a class="freepik" ' +
                   'href="http://www.freepik.com/">Freepik</a> from ' +
                   '<a class="flaticon" href="http://www.flaticon.com">' +
                   'Flaticon</a> is licensed under <a class="cc" ' +
                   'href="http://creativecommons.org/licenses/by/3.0/">' +
                   '"Creative Commons BY 3.0"</a>. Made with ' +
                   '<a class="logomaker" href="http://logomakr.com">' +
                   '"Logo Maker"</a>',

  register: 'التسجيل',
  login: 'تسجيل الدخول',
  leaderboard: 'المتصدرين',

  userProfile: 'العضو',
  highScoreTitle: 'أعلى النتائج:',
  biographyTitle: 'السيرة الذاتية',

  usernameTitle: 'اسم المستخدم',
  scoreTitle: 'النتيجة',
  rankTitle: 'الرتبة',
  emailTitle: 'البريد الإلكتروني',
  passwordTitle: 'كلمة المرور',
  confirmPasswordTitle: 'تأكيد كلمة المرور',
  submit: "إرسال"
};

const russianLocals = {
  russian: true,

  regEmailError: 'Электронная почта не действует или уже принято',
  regUserError: 'Имя пользователя, не действует или уже принято',
  regPasswordError: 'Пароль не достаточно сильны',
  regConfirmError: 'Пароли не совпадают',
  loginError: 'Либо имя пользователя или пароль являются недопустимыми',
  clientSideUserValidation: 'Имя пользователя должно содержать только символы алфавита',
  clientSideEmailValidation: 'Учитывая адрес не является действительным адресом электронной почты',

  userNotFound: 'Запрашиваемая пользователь не существует',

  aboutColiseo: 'Мы построили эту игру в школе (ЛМЮ). Класс КМСИ 401', // TODO?

  register: 'регистр',
  login: 'логин',
  logout: 'выйти',
  leaderboard: 'Лидеры',
  userProfile: 'Профиль пользователя',

  highScoreTitle: 'Рекорд',
  scoreTitle: 'Оценка',
  rankTitle: 'Ранг',
  emailTitle: 'E-Почта',
  passwordTitle: 'Пароль',
  confirmPasswordTitle: 'Подтвердите Пароль',
  submit: 'Представить',
};

export default (req, res, next) => {
  if (req.query.language) {
    req.session.language = req.query.language;
  }
  _.assign(res.locals, appConfig);
  if (req.session.language === 'spanish') {
    _.assign(res.locals, spanishLocals);
  } else if (req.session.language === 'arabic') {
    _.assign(res.locals, arabicLocals);
  } else if (req.session.language === 'russian') {
    _.assign(res.locals, russianLocals);
  } else {
    _.assign(res.locals, englishLocals);
  }
  next();
};
