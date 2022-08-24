FROM node:16.16.0 AS builder

WORKDIR /usr/src/app

COPY ui /usr/src/app/

WORKDIR /usr/src/app/ui

RUN yarn

EXPOSE 3000

CMD ["yarn", "start"]