import _ from 'lodash';
import config from 'config';
import * as crypto from 'crypto';
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
  const hash = crypto.pbkdf2Sync(password, salt, iterations, hashLength)
                     .toString(encoding);
  return hash;
};
