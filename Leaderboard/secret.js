import fs from 'fs';
import config from 'config';
import crypto from 'crypto';
// TODO: Is this the best way to handle application secrets?
const {filename, secretLength, encoding} = config.get('Secret');
let secret;

try {
  secret = fs.readFileSync(filename);
} catch (e) {
  secret = crypto.randomBytes(secretLength)
                 .toString(encoding);
  fs.writeFileSync(filename, secret);
}

export default secret;
