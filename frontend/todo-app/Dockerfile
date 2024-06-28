FROM node:20-alpine

WORKDIR /app

COPY package.json .
COPY node_modules .

RUN npm install

RUN npm i -g serve

COPY . .

RUN npm run build

EXPOSE 3000

# Set the base url
ENV PROXY_API=$PROXY_API

CMD [ "serve", "-s", "dist" ]