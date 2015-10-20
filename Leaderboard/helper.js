/*
 * Thanks to https://strongloop.com/strongblog/async-error-handling-expressjs-es7-promises-generators/
 * for the code.
 *
 * We need this so that express handles errors properly in async functions
 * because express is dumb.
 */
export const asyncWrap = fn => (...args) => fn(...args).catch(args[2]);
