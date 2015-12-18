import hbs from 'hbs';
import * as path from 'path';

const blocks = {};

hbs.registerHelper('extend', (name, context) => {
  let block = blocks[name];
  if (!block) {
    block = blocks[name] = [];
  }

  block.push(context.fn(this));
});

hbs.registerHelper('block', name => {
  const val = (blocks[name] || []).join('\n');

  // clear the block
  blocks[name] = [];
  return val;
});

// Handlebars index begins at zero, but for display purposes we want to start it
// at 1.
hbs.registerHelper('counter', index => index + 1);

hbs.registerPartials(path.join(__dirname, 'views', 'partials'));
