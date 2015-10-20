import app from '../app';
import request from 'supertest';
import fs from 'fs';
import path from 'path';
import chai from 'chai';

const should = chai.should();

const htmlFolder = path.resolve(__dirname, 'html');

const equalsHtml = fileName => fs.readFileSync(path.resolve(htmlFolder, fileName), 'utf8');

describe('GET', () => {
  describe('GET /', () => {
    it('should return the index page', done => {
      request(app)
        .get('/')
        .expect(equalsHtml('index.html'))
        .end(done);
    });
  });
  describe('GET /users/register', () => {
    it('should return the registration page', done => {
      request(app)
        .get('/users/register')
        .expect(equalsHtml('register.html'))
        .end(done);
    });
  });
});

describe('POST', () => {
  describe('POST /users/register', () => {
    context('Username is invalid', () => {
      it('should return an error for the registration', done =>  {
        request(app)
          .post('/users/register')
          .send('username=123')
          .send('password=48274081725827')
          .send('confirm=48274081725827')
          .send('email=fahepaf@gmail.com')
          .expect(400)
          .expect(new RegExp(app.locals.regUserError))
          .end(done);
      });
    });

    context('Password is too short', () => {
      it('should return an error for the regsitration', done => {
        request(app)
          .post('/users/register')
          .send('username=abc')
          .send('password=abc')
          .send('confirm=abc')
          .send('email=fahepaf@gmail.com')
          .expect(400)
          .expect(new RegExp(app.locals.regPasswordError))
          .end(done);
      });
    });

    context('Passwords do not match', () => {
      it('should return an error for the regsitration', done => {
        request(app)
          .post('/users/register')
          .send('username=abc')
          .send('password=48274081725827')
          .send('confirm=48274081725828')
          .send('email=fahepaf@gmail.com')
          .expect(400)
          .expect(new RegExp(app.locals.regConfirmError))
          .end(done);
      });
    });

    // TODO: Test email being invalid
  });
});
