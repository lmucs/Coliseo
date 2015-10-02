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
};

export const errorLogger = expressWinston.errorLogger(loggerConfig);
export const requestLogger = expressWinston.logger(loggerConfig);
export default new winston.Logger(loggerConfig);
