import * as fs from 'fs';
import config from 'config';
import * as crypto from 'crypto';
// TODO: Is this the best way to handle application secrets?
const {filename, secretLength, encoding} = config.get('Secret');
let secret;

try {
  secret = fs.readFileSync(filename, 'utf8');
} catch (e) {
  secret = crypto.randomBytes(secretLength)
                 .toString(encoding);
  fs.writeFileSync(filename, secret);
}

export default secret;
