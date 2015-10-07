import config from 'config';
import winston from 'winston';
import expressWinston from 'express-winston';

const winstonConfig = config.get('Logger');
const loggerConfig = {
  transports: [
    new winston.transports.Console(winstonConfig),
  ],
  statusLevels: true,
  level: winstonConfig.transport.level,
  expressFormat: true,
  meta: false,
  handleExceptions: true,
  humanReadableUnhandledExceptions: true,
  prettyPrint: true,
  colorize: true,
  silent: false,
};

const logger = new winston.Logger(loggerConfig);

Object.defineProperty(logger, 'exception', {
  enumerable: false,
  configurable: false,
  writable: false,
  value: (message, exception) => {
    logger.error(message);
    logger.silly(JSON.stringify(exception, null, 2));
    logger.error(exception);
  },
});

export const errorLogger = expressWinston.errorLogger(loggerConfig);
export const requestLogger = expressWinston.logger(loggerConfig);
export default logger;
