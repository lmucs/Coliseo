const _ = require('lodash');

const locals = {
  appName: 'Fight or Flight',
  primaryColor: 'indigo',
  accentColor: 'pink',
};
const setLocals = app => _.assign(app.locals, locals);
export default setLocals;
