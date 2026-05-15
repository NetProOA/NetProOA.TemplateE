# 引入docker上传环境
FROM 192.168.31.105/public/nginx:alpine
WORKDIR /usr/src/app/
USER root
COPY ./dist ./dist
# 删除文件
RUN rm -rf /usr/share/nginx/html/*
# 复制文件
COPY ./deploy/nginxk8s.conf /etc/nginx/nginx.conf
COPY ./dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
