# build
FROM node:22.14-alpine AS build
WORKDIR /app
COPY ./ ./
RUN npm ci
RUN npm run build

# serve
FROM nginx:alpine
COPY --from=build /app/dist/fit-frontend/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 443
