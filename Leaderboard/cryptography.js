import _ from 'lodash';
import config from 'config';
import crypto from 'crypto';

import log from './logging';
const {
  saltLength,
  iterationsBase,
  iterationsMax,
  hashLength,
  encoding,
} = config.get('Crypto');
export const calculateSaltHash = password => {
  const iterations = _.random(iterationsBase, iterationsMax);
  const salt = crypto.randomBytes(saltLength)
                     .toString(encoding);
  const hash = calculateHash(password, salt, iterations);
  return `${iterations}$${salt}$${hash}`;
};

export const calculateHash = (password, salt, iterations) => {
  log.profile('Hash calculation');
  const hash = crypto.pbkdf2Sync(password, salt, iterations, hashLength)
                     .toString(encoding);
  log.profile('Hash calculation');
  return hash;
};
