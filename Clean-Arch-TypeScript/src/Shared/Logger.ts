import winston from "winston";
import "winston-daily-rotate-file";
import moment from "moment";

const localTimestamp = () => {
  return moment().format("YYYY-MM-DD HH:mm:ss"); // Customize as needed
};

const infoFileTransport = new winston.transports.DailyRotateFile({
  filename: "logs/%DATE%-info.log",
  datePattern: "YYYY-MM-DD",
  maxSize: "20m",
  maxFiles: "14d",
  level: "info",
  format: winston.format.combine(
    winston.format.timestamp({ format: localTimestamp }),
    winston.format.json()
  ),
});

const errorFileTransport = new winston.transports.DailyRotateFile({
  filename: "logs/%DATE%-error.log",
  datePattern: "YYYY-MM-DD",
  maxSize: "20m",
  maxFiles: "14d",
  level: "error",
  format: winston.format.combine(
    winston.format.timestamp({ format: localTimestamp }),
    winston.format.json()
  ),
});

const consoleTransport = new winston.transports.Console({
  format: winston.format.combine(
    winston.format.colorize(),
    winston.format.timestamp({ format: localTimestamp }),
    winston.format.printf(({ level, message, timestamp }) => {
      return `${timestamp} [${level.toUpperCase()}] ${message}`;
    })
  ),
});

const logger = winston.createLogger({
  level: "info",
  format: winston.format.json(),
  transports: [consoleTransport, infoFileTransport, errorFileTransport],
});

export default logger;
