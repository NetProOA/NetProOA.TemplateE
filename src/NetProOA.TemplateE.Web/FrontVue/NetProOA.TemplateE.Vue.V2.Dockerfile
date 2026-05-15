FROM node:18.12.1 AS builder
WORKDIR /app
COPY ./src/NetProOA.Ks.Web/FrontVue/ ./
RUN npm config set registry https://registry.npmmirror.com
RUN npm install
RUN npm run build --reset-cache

FROM 192.168.31.185/public/nginx:alpine
ENV NODE_ENV=production
EXPOSE 80
COPY --from=builder /app/dist /usr/share/nginx/html/
RUN rm /etc/nginx/conf.d/default.conf
COPY --from=builder /app/deploy/nginxk3s.conf /etc/nginx/conf.d/default.conf
CMD ["nginx", "-g", "daemon off;"]
