FROM node:22-alpine AS build
WORKDIR /app

COPY package.json package-lock.json nx.json jest.config.ts jest.preset.js tsconfig.base.json ./
COPY apps ./apps
RUN npm ci
RUN npm run build

FROM nginx:1.29-alpine AS final
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/apps/vitrina/browser /usr/share/nginx/html
EXPOSE 80
