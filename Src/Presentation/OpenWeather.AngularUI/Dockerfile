FROM node:lts-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
RUN npx ngcc --properties es2023 browser module main --first-only --create-ivy-entry-points
COPY . .
RUN npm run build --prod

FROM nginx:stable-alpine
RUN rm -rf /usr/share/nginx/html/*
VOLUME /var/cache/nginx
COPY ./config/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist/open-weather.angular-ui/ /usr/share/nginx/html
#CMD ["nginx", "-g", "daemon off;"]