docker run --name mysql-container -e MYSQL_ROOT_PASSWORD=111111 -e MYSQL_DATABASE=mydatabase -e MYSQL_USER=sa -e MYSQL_PASSWORD=111111 -p 3306:3306 -d mysql:latest


   dotnet ef dbcontext scaffold "Server=localhost;Port=3306;Database=mydatabase;User=root;Password=111111;AllowPublicKeyRetrieval=True;" Pomelo.EntityFrameworkCore.MySql -o Models -c MyDbContext
   