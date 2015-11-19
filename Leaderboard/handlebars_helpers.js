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

hbs.registerPartials(path.join(__dirname, 'views', 'partials'));
