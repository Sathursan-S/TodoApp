# Use a multi-stage build to reduce final image size
FROM node:20-bullseye-slim as build

WORKDIR /app

# Copy package.json and package-lock.json separately
COPY package.json package-lock.json ./

# Install dependencies
RUN npm install --legacy-peer-deps

# Copy the rest of the application code
COPY . .

# Build the application
RUN npm run build

# Use a smaller image for the final stage
FROM node:20-bullseye-slim

# Install serve globally
RUN npm i -g serve

WORKDIR /app

# Copy the built files from the previous stage
COPY --from=build /app/dist ./dist

EXPOSE 3000

# Set the base url
ENV PROXY_API=$PROXY_API

CMD ["serve", "-s", "dist"]
