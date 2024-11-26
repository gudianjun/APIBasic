using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIBasic.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "account_type",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false, comment: "帐号类型")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", comment: "号帐类型名称(0:普通帐号 1：企业帐号 2：设计师帐号 11：管理员帐号)", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                },
                comment: "帐号类型\r\n")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "accounttype_function",
                columns: table => new
                {
                    type = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", comment: "帐号类型名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    function_name = table.Column<string>(type: "varchar(20480)", nullable: true, defaultValueSql: "''", comment: "功能列表", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    companyid = table.Column<int>(type: "int", nullable: true),
                    account_type = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'2'", comment: "创建的帐号类型默认为普通帐号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.type);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    CompanyID = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    logoMenu = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    logoSetting = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Folder = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    money = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    version = table.Column<uint>(type: "int unsigned", nullable: true, defaultValueSql: "'0'"),
                    CreateTime = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    address = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    telphone = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    mobile = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    contacts = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    email = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    WebName = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    WebLogImage = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    WebTitleImage = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    UserNumber = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    EmbedWeb = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    DesignerNumber = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, defaultValueSql: "'''0'''", comment: "设计师帐号数量", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    NormalNumber = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, defaultValueSql: "'''0'''", comment: "普通用户帐号数量", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    BrandName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, defaultValueSql: "''", comment: "牌品名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    BrandImage = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "品牌图片", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.CompanyID);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "company_authority",
                columns: table => new
                {
                    companyid = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false, comment: "公司id", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    webaddr = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "公司开通的网址", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    begindate = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, defaultValueSql: "''", comment: "开通日期", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    enddate = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, defaultValueSql: "''", comment: "终止日期", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    days = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, defaultValueSql: "''", comment: "开通时长", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    state = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true, defaultValueSql: "'0'", comment: "通开状态：0没开通 1：开通 ", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    createdate = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, defaultValueSql: "''", comment: "创建日期", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "design_file",
                columns: table => new
                {
                    FileID = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrentVersion = table.Column<int>(type: "int", nullable: false),
                    ResourceType = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResourceName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeviceType = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserID = table.Column<uint>(type: "int unsigned", nullable: true),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    FileContent = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remarks = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Thumbnail1 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Thumbnail2 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.FileID);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "function_list",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_account = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    company_id = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    function_name = table.Column<string>(type: "varchar(20480)", nullable: true, defaultValueSql: "''", comment: "可以使用的功能,以逗号分隔", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "function_module",
                columns: table => new
                {
                    company_id = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true, defaultValueSql: "''", comment: "公司id", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    function = table.Column<string>(type: "varchar(20480)", nullable: true, defaultValueSql: "''", comment: "指定系统功能模块是否可用", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "housetype",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "唯一id,这个值为保存方案目录", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    thumbnail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "缩略图", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    scenename = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "场景名", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "楼盘/小区名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    housetype = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "型户", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    area = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "积面", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    createtime = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    province = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "份省", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    city = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "市城", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    address = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "体具地址", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    designer = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "计设师", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    designerid = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "计设师帐号", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    companyid = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "司公id", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    fuzzysearch = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "于用模糊查找", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    selldate = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "盘开时间", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    type = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "类型（公寓还是酒店）", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    reserver1 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "留保", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    reserver2 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "留保", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    reserver3 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "留保", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    version = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "ip_list",
                columns: table => new
                {
                    ip = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    city = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    page = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    time = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    date = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "keyword",
                columns: table => new
                {
                    classname = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    classid = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "logininfo",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "text", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    UserIP = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    LoginTime = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "materialcx",
                columns: table => new
                {
                    class1 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    class2 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    file = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    name = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    size = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    type = table.Column<int>(type: "int", nullable: true, comment: "0:普通 1：高光  2：平光  3：哑光"),
                    filesize = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    mode = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'", comment: "0 3D相关 1 vrscene相关 2 缩略图"),
                    uuid = table.Column<string>(type: "text", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    price = table.Column<float>(type: "float", nullable: true, defaultValueSql: "'0'"),
                    accountType = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    companyID = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'2'"),
                    UserID = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    attribute = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    materialname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, defaultValueSql: "''", comment: "贴图中文名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    puttype = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true, defaultValueSql: "''", comment: "放置类型 通用:0  地面：1  墙面：2  顶面：3    参数使用模型使用的贴图：10", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "materialcx_backup",
                columns: table => new
                {
                    class1 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    class2 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    file = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    name = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    size = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    type = table.Column<int>(type: "int", nullable: true, comment: "0:普通 1：高光  2：平光  3：哑光"),
                    filesize = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    mode = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'", comment: "0 3D相关 1 vrscene相关 2 缩略图"),
                    uuid = table.Column<string>(type: "text", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    price = table.Column<float>(type: "float", nullable: true, defaultValueSql: "'0'"),
                    accountType = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    companyID = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'2'"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    attribute = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    materialname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, defaultValueSql: "''", comment: "贴图中文名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    puttype = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true, defaultValueSql: "''", comment: "放置类型 通用:0  地面：1  墙面：2  顶面：3    参数使用模型使用的贴图：10", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "materialcx_buy",
                columns: table => new
                {
                    class1 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    class2 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    file = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    name = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    size = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    type = table.Column<int>(type: "int", nullable: true, comment: "0 面地 1 墙面 2 顶面"),
                    filesize = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    mode = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'", comment: "0 3D相关 1 vrscene相关 2 缩略图"),
                    uuid = table.Column<string>(type: "text", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    price = table.Column<float>(type: "float", nullable: true, defaultValueSql: "'0'"),
                    accountType = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    companyID = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'2'"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    attribute = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    materialname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, defaultValueSql: "''", comment: "贴图中文名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    puttype = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true, defaultValueSql: "''", comment: "放置类型 通用:0  地面：1  墙面：2  顶面：3", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "modelcx",
                columns: table => new
                {
                    class1 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    class2 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    class3 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    name = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    size = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    file = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    type = table.Column<int>(type: "int", nullable: true, comment: "0 面地 1 墙面 2 顶面  10 可编辑资源中模型数据"),
                    mode = table.Column<int>(type: "int", nullable: true, comment: "0 3D相关 1 vrscene相关 2 缩略图"),
                    filesize = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    uuid = table.Column<string>(type: "text", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    price = table.Column<float>(type: "float", nullable: true, defaultValueSql: "'0'"),
                    accountType = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    companyID = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'2'"),
                    UserID = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    attribute = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    modelformat = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValueSql: "''", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    modelname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, defaultValueSql: "''", comment: "模型中文名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    materialreplace = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true, defaultValueSql: "'0'", comment: "0:不可替换材质  1：可以替换材质", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    style = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, defaultValueSql: "'-1'", comment: "模型样式\r\n门 100：单开门  101：双开门 10 2：2扇推拉门  103.3扇推拉门  104. 4扇推拉门   105. 门洞", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    extend = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, defaultValueSql: "'{}'", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "modelcx_backup",
                columns: table => new
                {
                    class1 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    class2 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    class3 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    name = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    size = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    file = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    type = table.Column<int>(type: "int", nullable: true, comment: "0 面地 1 墙面 2 顶面"),
                    mode = table.Column<int>(type: "int", nullable: true, comment: "0 3D相关 1 vrscene相关 2 缩略图"),
                    filesize = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    uuid = table.Column<string>(type: "text", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    price = table.Column<float>(type: "float", nullable: true, defaultValueSql: "'0'"),
                    accountType = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    companyID = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'2'"),
                    UserID = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    attribute = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    modelformat = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValueSql: "''", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    modelname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, defaultValueSql: "''", comment: "模型中文名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    materialreplace = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true, defaultValueSql: "'0'", comment: "0:不可替换材质  1：可以替换材质", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    style = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, defaultValueSql: "'-1'", comment: "模型样式\r\n门 100：单开门  101：双开门 10 2：2扇推拉门  103.3扇推拉门  104. 4扇推拉门   105. 门洞", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    extend = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, defaultValueSql: "'{}'", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "modelcx_buy",
                columns: table => new
                {
                    class1 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    class2 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    class3 = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    name = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    size = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    file = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    type = table.Column<int>(type: "int", nullable: true, comment: "0 面地 1 墙面 2 顶面"),
                    mode = table.Column<int>(type: "int", nullable: true, comment: "0 3D相关 1 vrscene相关 2 缩略图"),
                    filesize = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    uuid = table.Column<string>(type: "text", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    price = table.Column<float>(type: "float", nullable: true, defaultValueSql: "'0'"),
                    accountType = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    companyID = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'2'"),
                    UserID = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    attribute = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    modelformat = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValueSql: "''", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    modelname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, defaultValueSql: "''", comment: "模型中文名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    materialreplace = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true, defaultValueSql: "'0'", comment: "0:不可替换材质  1：可以替换材质", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    style = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, defaultValueSql: "'-1'", comment: "模型样式\r\n门 100：单开门  101：双开门 10 2：2扇推拉门  103.3扇推拉门  104. 4扇推拉门   105. 门洞", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    extend = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, defaultValueSql: "'{}'", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "mymaterialcx",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true, defaultValueSql: "''", comment: "户用帐号", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    companyid = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true, defaultValueSql: "''", comment: "公司id", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    name = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true, defaultValueSql: "''", comment: "片图名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    file = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "文件相对路径", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    size = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true, defaultValueSql: "''", comment: "片图尺寸", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    class1 = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", comment: "类分1", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    class2 = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", comment: "分类2", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    date = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", comment: "上传日期", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "outdoor_picture",
                columns: table => new
                {
                    companyid = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    file = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValueSql: "'0'", comment: "0:室内 1：室外白天  2:室外夜晚", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    user = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "renderanimation",
                columns: table => new
                {
                    FileID = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    CreateTime = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    FileName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    FilePath = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    VideoWidth = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    VideoHeight = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    VideoFps = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.FileID);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "renderdata",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    UserAccount = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "''", comment: "渲染用户帐号", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ImageType = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "''", comment: "渲染生成的图片类型：0 效果图  1：全景", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ImageName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "''", comment: "渲染生成的图片名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Thumbnail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "''", comment: "生成的缩略图名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ImagePath = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "''", comment: "渲染生成的图片路径", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ImageSize = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "''", comment: "渲染生成图片尺寸", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    RenderTime = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "''", comment: "渲染时间", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    WebName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "''", comment: "渲染全景时生成的网页名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "renderimage",
                columns: table => new
                {
                    FileID = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ImageIndex = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    PathFileName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                },
                comment: "保存渲染时回传图片")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "renderqueue",
                columns: table => new
                {
                    FileID = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ImageFile = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    UvFile = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'", comment: "0：ready  1:rendering  2:finish"),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserID = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Progress = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    UserSchemePath = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Thumbnail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    RenderVersion = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValueSql: "'3.0'", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    RenderingPictrue = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    TotalPicture = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "scheme_exhibition",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    user_account = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    project_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    project_thumbnail = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    project_data = table.Column<string>(type: "varchar(20480)", nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    create_time = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "scheme_failed",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", comment: "方案id", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    company_name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    render_failed = table.Column<string>(type: "longtext", nullable: true, comment: "渲染失败原因", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    send_failed = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, defaultValueSql: "''", comment: "发送失败原因", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    other_failed = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    create_time = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "sharescene",
                columns: table => new
                {
                    scenename = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    folder = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    username = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    modelcount = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, defaultValueSql: "'0'", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    thumbnail1 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    thumbnail2 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    thumbnail3 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    state = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true, defaultValueSql: "'0'", comment: "0:下架 1：上架  2：删除", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    companyid = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "'2'", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    @class = table.Column<string>(name: "class", type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "'其它'", comment: "分类名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "sharescene_buy",
                columns: table => new
                {
                    scenename = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    folder = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    username = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    modelcount = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, defaultValueSql: "'0'", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    thumbnail1 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    thumbnail2 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    thumbnail3 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    state = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true, defaultValueSql: "'0'", comment: "0:下架 1：上传  2：删除", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    companyid = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    @class = table.Column<string>(name: "class", type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "'其它'", comment: "分类名称", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserID = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Password = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    QQ = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Tel = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    EnableTime = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    CompanyID = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Authcode = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Permissions = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'"),
                    textdesc = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    lasttime = table.Column<DateTime>(type: "datetime", nullable: true),
                    administrator = table.Column<string>(type: "text", nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    accounttype = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'", comment: "0 企业  1：设计师  2：普通用户 11:超级用户"),
                    creater = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    createrid = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    accountname = table.Column<string>(type: "text", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    refine_authorization = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true, defaultValueSql: "'0'", comment: "设置精装方案:1 充许  0：禁止", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    master_authorization = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true, defaultValueSql: "'0'", comment: "设置大师方案： 1 充许  0：禁止", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    housetype_authorization = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true, defaultValueSql: "'0'", comment: "户型上传：1 充许  0：禁止", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    scheme_check_authorization = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true, defaultValueSql: "'0'", comment: "方案审核：1 充许  0：禁止", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    housetype_check_authorization = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true, defaultValueSql: "'0'", comment: "户型审核：1 充许  0：禁止", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    createtime = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailVerificationCode = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    AvatarIcon = table.Column<string>(type: "text", nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    MailAddress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Zip = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.UserID);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "wxshare",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, defaultValueSql: "''", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    address = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "需要分享的地址", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    thumbnail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "分享显示的缩略图", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    title = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "标题", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    description = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, defaultValueSql: "''", comment: "分享描述", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    reserve = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, defaultValueSql: "''", comment: "预留字段", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_type");

            migrationBuilder.DropTable(
                name: "accounttype_function");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "company_authority");

            migrationBuilder.DropTable(
                name: "design_file");

            migrationBuilder.DropTable(
                name: "function_list");

            migrationBuilder.DropTable(
                name: "function_module");

            migrationBuilder.DropTable(
                name: "housetype");

            migrationBuilder.DropTable(
                name: "ip_list");

            migrationBuilder.DropTable(
                name: "keyword");

            migrationBuilder.DropTable(
                name: "logininfo");

            migrationBuilder.DropTable(
                name: "materialcx");

            migrationBuilder.DropTable(
                name: "materialcx_backup");

            migrationBuilder.DropTable(
                name: "materialcx_buy");

            migrationBuilder.DropTable(
                name: "modelcx");

            migrationBuilder.DropTable(
                name: "modelcx_backup");

            migrationBuilder.DropTable(
                name: "modelcx_buy");

            migrationBuilder.DropTable(
                name: "mymaterialcx");

            migrationBuilder.DropTable(
                name: "outdoor_picture");

            migrationBuilder.DropTable(
                name: "renderanimation");

            migrationBuilder.DropTable(
                name: "renderdata");

            migrationBuilder.DropTable(
                name: "renderimage");

            migrationBuilder.DropTable(
                name: "renderqueue");

            migrationBuilder.DropTable(
                name: "scheme_exhibition");

            migrationBuilder.DropTable(
                name: "scheme_failed");

            migrationBuilder.DropTable(
                name: "sharescene");

            migrationBuilder.DropTable(
                name: "sharescene_buy");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "wxshare");
        }
    }
}
