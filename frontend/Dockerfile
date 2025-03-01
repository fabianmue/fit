# build
FROM node:latest AS build
WORKDIR /app
COPY ./ ./
RUN npm ci
RUN npm run build --prod

# serve
FROM nginx:alpine
COPY --from=build /app/dist/fit-frontend/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
