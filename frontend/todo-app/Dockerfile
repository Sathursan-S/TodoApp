FROM node:20-bullseye-slim as build

WORKDIR /app

COPY package.json package-lock.json ./

RUN npm install --legacy-peer-deps

COPY . .

RUN npm run build

FROM node:20-bullseye-slim

RUN npm i -g serve

WORKDIR /app

COPY --from=build /app/dist ./dist

EXPOSE 3000

ENV PROXY_API=$PROXY_API

CMD ["serve", "-s", "dist"]
