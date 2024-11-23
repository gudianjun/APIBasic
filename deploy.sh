#!/bin/sh

# 停止容器
sudo docker stop apibasic-container

# 删除容器
sudo docker rm apibasic-container

# 编译镜像
sudo docker build -t apibasic-image .

# 运行容器
sudo docker run -d -p 8080:8080 -p 8081:8081 --name apibasic-container apibasic-image
