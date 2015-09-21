const app = require('../app'),
      request = require('supertest'),
      fs = require('fs'),
      path = require('path');

const htmlFolder = path.resolve(__dirname, 'html');

const equalsHtml = fileName => fs.readFileSync(path.resolve(htmlFolder, fileName), 'utf8');

describe('GET', () => {
  describe('/', () => {
    it('should return the index page', done => {
      request(app)
        .get('/')
        .expect(equalsHtml('index.html'), done);
    });
  });
  describe('/users/register', () => {
    it('should return the registration page', done => {
      request(app)
        .get('/users/register')
        .expect(equalsHtml('register.html'), done);
    });
  });
});
