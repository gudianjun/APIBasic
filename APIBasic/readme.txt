docker run --name mysql-container -e MYSQL_ROOT_PASSWORD=111111 -e MYSQL_DATABASE=mydatabase -e MYSQL_USER=sa -e MYSQL_PASSWORD=111111 -p 3306:3306 -d mysql:latest
安装Ef数据库工具
dotnet tool install --global dotnet-ef
 
dotnet ef dbcontext scaffold "Server=localhost;Port=3306;Database=mydatabase;User=root;Password=111111;AllowPublicKeyRetrieval=True;" Pomelo.EntityFrameworkCore.MySql -o ./Models --context-dir ./Data -c MySqlDbContext --force


发布docker
docker build -t apibasic-image .
docker run -d -p 8080:8080 -p 8081:8081 --name apibasic-container apibasic-image
docker run -d -p 8081:8081 --name apibasic-container apibasic-image
导入数据
1,拷贝render.sql到容器
2，进入容器找到文件，并执行一下命令
 docker exec -it mysql-container /bin/bash
mysql -h localhost -u root -p --default-character-set=utf8 mydatabase < render.sql

API服务器功能描述
1,日志记录
查看LoggingMiddleware，记录公共日志。
2，支持内存缓存功能
3，使用Mysql数据库，上下文对象MySqlDbContext，模型保存在Models中
4，系统其他配置使用在appsetting.json中的APIConfig配置，管理该对象的类为APIConfig
5，增加了健康检查中间件，用来检测数据库连接是否可用以及服务是否可用。 地址为
http://localhost:5049/health
6，系统返回数据经过压缩，通过AddResponseCompression方法
7，添加复杂逻辑验证。查看Validations中继承AbstractValidator的对象，用来实现复杂校验逻辑。
8，添加速率限制服务。配置文件中的IpRateLimiting属性用来配置访问接口速率。
9，配置了跨域访问，查看配置中的AllowedOrigins，如果里面配置了"*"，则认为时全域访问。
10，分页查询必须使用PagedRequest和PagedResponse对象。
11，为了追踪日志，系统会自动添加请求和返回头，X-Request-ID，客户也可以自己添加该请求头。
12，用户Token验证中OnTokenValidated中添加排他处理。
13，系统支持角色验证，角色名称配置在Roles中。
14，客户端请求时，需要区分设备，请使用Audience中的字符串进行设定。
15，返回值定义在ApiResponse中。
15，自定义字段验证错误信息，查看InvalidModelStateResponseFactory
 