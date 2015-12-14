import express from 'express';

import v1 from './v1';

const router = express.Router();
router.use('/v1', v1);

// We want to have separate error handling because we're using JSON here.

router.use((req, res, next) => {
  const err = new Error(`Not Found: ${req.originalUrl}`);
  err.status = 404;
  next(err);
});

if (router.get('env') === 'development') {
  router.use((err, req, res, next) => {
    res.status(err.status || 500);
    res.json({
      message: err.message,
      error: err,
    });
    console.error(err.stack);
  });
}

router.use((err, req, res, next) => {
  res.status(err.status || 500);
  let error = {};
  if (req.app.get('env') === 'development') {
    error = err;
  }
  res.json({
    message: err.message,
    error,
  });
});

export default router;
