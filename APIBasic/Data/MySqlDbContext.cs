using System;
using System.Collections.Generic;
using APIBasic.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace APIBasic.Data;

public partial class MySqlDbContext : DbContext
{
    public MySqlDbContext()
    {
    }

    public MySqlDbContext(DbContextOptions<MySqlDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<AccounttypeFunction> AccounttypeFunctions { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyAuthority> CompanyAuthorities { get; set; }

    public virtual DbSet<FileManagement> FileManagements { get; set; }

    public virtual DbSet<FunctionList> FunctionLists { get; set; }

    public virtual DbSet<FunctionModule> FunctionModules { get; set; }

    public virtual DbSet<Housetype> Housetypes { get; set; }

    public virtual DbSet<IpList> IpLists { get; set; }

    public virtual DbSet<Keyword> Keywords { get; set; }

    public virtual DbSet<Logininfo> Logininfos { get; set; }

    public virtual DbSet<Materialcx> Materialcxes { get; set; }

    public virtual DbSet<MaterialcxBackup> MaterialcxBackups { get; set; }

    public virtual DbSet<MaterialcxBuy> MaterialcxBuys { get; set; }

    public virtual DbSet<Modelcx> Modelcxes { get; set; }

    public virtual DbSet<ModelcxBackup> ModelcxBackups { get; set; }

    public virtual DbSet<ModelcxBuy> ModelcxBuys { get; set; }

    public virtual DbSet<Mymaterialcx> Mymaterialcxes { get; set; }

    public virtual DbSet<OutdoorPicture> OutdoorPictures { get; set; }

    public virtual DbSet<Renderanimation> Renderanimations { get; set; }

    public virtual DbSet<Renderdatum> Renderdata { get; set; }

    public virtual DbSet<Renderimage> Renderimages { get; set; }

    public virtual DbSet<Renderqueue> Renderqueues { get; set; }

    public virtual DbSet<SchemeExhibition> SchemeExhibitions { get; set; }

    public virtual DbSet<SchemeFailed> SchemeFaileds { get; set; }

    public virtual DbSet<Sharescene> Sharescenes { get; set; }

    public virtual DbSet<SharesceneBuy> SharesceneBuys { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wxshare> Wxshares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=140.83.84.36;port=3306;database=mydatabase;user=root;password=111111;allowpublickeyretrieval=True;connect timeout=130", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.1.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("account_type", tb => tb.HasComment("帐号类型\r\n"))
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id)
                .HasComment("帐号类型")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasComment("号帐类型名称(0:普通帐号 1：企业帐号 2：设计师帐号 11：管理员帐号)")
                .HasColumnName("name");
        });

        modelBuilder.Entity<AccounttypeFunction>(entity =>
        {
            entity.HasKey(e => e.Type).HasName("PRIMARY");

            entity
                .ToTable("accounttype_function")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.AccountType)
                .HasDefaultValueSql("'2'")
                .HasComment("创建的帐号类型默认为普通帐号")
                .HasColumnName("account_type");
            entity.Property(e => e.Companyid).HasColumnName("companyid");
            entity.Property(e => e.FunctionName)
                .HasDefaultValueSql("''")
                .HasComment("功能列表")
                .HasColumnType("varchar(20480)")
                .HasColumnName("function_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasComment("帐号类型名称")
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PRIMARY");

            entity
                .ToTable("company")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.BrandImage)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("品牌图片");
            entity.Property(e => e.BrandName)
                .HasMaxLength(128)
                .HasDefaultValueSql("''")
                .HasComment("牌品名称");
            entity.Property(e => e.CompanyName).HasColumnType("text");
            entity.Property(e => e.Contacts)
                .HasColumnType("text")
                .HasColumnName("contacts");
            entity.Property(e => e.CreateTime).HasColumnType("text");
            entity.Property(e => e.DesignerNumber)
                .HasMaxLength(5)
                .HasDefaultValueSql("'''0'''")
                .HasComment("设计师帐号数量");
            entity.Property(e => e.Email)
                .HasColumnType("text")
                .HasColumnName("email");
            entity.Property(e => e.EmbedWeb).HasColumnType("text");
            entity.Property(e => e.Folder)
                .HasColumnType("text")
                .UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");
            entity.Property(e => e.LogoMenu)
                .HasColumnType("text")
                .HasColumnName("logoMenu")
                .UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");
            entity.Property(e => e.LogoSetting)
                .HasColumnType("text")
                .HasColumnName("logoSetting")
                .UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");
            entity.Property(e => e.Mobile)
                .HasColumnType("text")
                .HasColumnName("mobile");
            entity.Property(e => e.Money)
                .HasDefaultValueSql("'0'")
                .HasColumnName("money");
            entity.Property(e => e.NormalNumber)
                .HasMaxLength(5)
                .HasDefaultValueSql("'''0'''")
                .HasComment("普通用户帐号数量");
            entity.Property(e => e.Telphone)
                .HasColumnType("text")
                .HasColumnName("telphone");
            entity.Property(e => e.UserNumber).HasColumnType("text");
            entity.Property(e => e.Version)
                .HasDefaultValueSql("'0'")
                .HasColumnName("version");
            entity.Property(e => e.WebLogImage).HasColumnType("text");
            entity.Property(e => e.WebName).HasColumnType("text");
            entity.Property(e => e.WebTitleImage).HasColumnType("text");
        });

        modelBuilder.Entity<CompanyAuthority>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("company_authority")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Begindate)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasComment("开通日期")
                .HasColumnName("begindate")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Companyid)
                .HasMaxLength(5)
                .HasComment("公司id")
                .HasColumnName("companyid")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Createdate)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasComment("创建日期")
                .HasColumnName("createdate")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Days)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasComment("开通时长")
                .HasColumnName("days")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Enddate)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasComment("终止日期")
                .HasColumnName("enddate")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .HasDefaultValueSql("'0'")
                .HasComment("通开状态：0没开通 1：开通 ")
                .HasColumnName("state");
            entity.Property(e => e.Webaddr)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("公司开通的网址")
                .HasColumnName("webaddr")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<FileManagement>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PRIMARY");

            entity.ToTable("FileManagement");

            entity.Property(e => e.FileId).HasColumnName("FileID");
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.DeviceType).HasMaxLength(255);
            entity.Property(e => e.FileFormat).HasMaxLength(255);
            entity.Property(e => e.LastUpdatedTime).HasColumnType("datetime");
            entity.Property(e => e.Remarks).HasColumnType("text");
            entity.Property(e => e.ResourceName).HasMaxLength(255);
            entity.Property(e => e.ResourceType).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<FunctionList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("function_list")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .HasColumnName("company_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.FunctionName)
                .HasDefaultValueSql("''")
                .HasComment("可以使用的功能,以逗号分隔")
                .HasColumnType("varchar(20480)")
                .HasColumnName("function_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserAccount)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasColumnName("user_account")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<FunctionModule>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("function_module")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.CompanyId)
                .HasMaxLength(16)
                .HasDefaultValueSql("''")
                .HasComment("公司id")
                .HasColumnName("company_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Function)
                .HasDefaultValueSql("''")
                .HasComment("指定系统功能模块是否可用")
                .HasColumnType("varchar(20480)")
                .HasColumnName("function")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Housetype>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("housetype")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Address)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("体具地址")
                .HasColumnName("address")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Area)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("积面")
                .HasColumnName("area")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.City)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("市城")
                .HasColumnName("city")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Companyid)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("司公id")
                .HasColumnName("companyid")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Createtime)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasColumnName("createtime")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Designer)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("计设师")
                .HasColumnName("designer")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Designerid)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("计设师帐号")
                .HasColumnName("designerid")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Fuzzysearch)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("于用模糊查找")
                .HasColumnName("fuzzysearch")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Housetype1)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("型户")
                .HasColumnName("housetype")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Id)
                .HasMaxLength(256)
                .HasComment("唯一id,这个值为保存方案目录")
                .HasColumnName("id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("楼盘/小区名称")
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Province)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("份省")
                .HasColumnName("province")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Reserver1)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("留保")
                .HasColumnName("reserver1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Reserver2)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("留保")
                .HasColumnName("reserver2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Reserver3)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("留保")
                .HasColumnName("reserver3")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Scenename)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("场景名")
                .HasColumnName("scenename")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Selldate)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("盘开时间")
                .HasColumnName("selldate")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("缩略图")
                .HasColumnName("thumbnail")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("类型（公寓还是酒店）")
                .HasColumnName("type")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Version)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .HasColumnName("version")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<IpList>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ip_list")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.City)
                .HasMaxLength(32)
                .HasColumnName("city")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Date)
                .HasMaxLength(32)
                .HasColumnName("date");
            entity.Property(e => e.Ip)
                .HasMaxLength(32)
                .HasColumnName("ip");
            entity.Property(e => e.Page)
                .HasMaxLength(32)
                .HasColumnName("page");
            entity.Property(e => e.Time)
                .HasMaxLength(32)
                .HasColumnName("time");
        });

        modelBuilder.Entity<Keyword>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("keyword")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Classid)
                .HasColumnType("text")
                .HasColumnName("classid")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Classname)
                .HasColumnType("text")
                .HasColumnName("classname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Logininfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("logininfo")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.LoginTime)
                .HasColumnType("text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserIp)
                .HasColumnType("text")
                .HasColumnName("UserIP")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserName)
                .HasColumnType("text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Materialcx>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("materialcx")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.AccountType)
                .HasDefaultValueSql("'0'")
                .HasColumnName("accountType");
            entity.Property(e => e.Attribute)
                .HasColumnType("text")
                .HasColumnName("attribute")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class1)
                .HasColumnType("text")
                .HasColumnName("class1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class2)
                .HasColumnType("text")
                .HasColumnName("class2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CompanyId)
                .HasDefaultValueSql("'2'")
                .HasColumnName("companyID");
            entity.Property(e => e.File)
                .HasColumnType("text")
                .HasColumnName("file")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Filesize)
                .HasColumnType("text")
                .HasColumnName("filesize");
            entity.Property(e => e.Materialname)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasComment("贴图中文名称")
                .HasColumnName("materialname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Mode)
                .HasDefaultValueSql("'0'")
                .HasComment("0 3D相关 1 vrscene相关 2 缩略图")
                .HasColumnName("mode");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'0'")
                .HasColumnName("price");
            entity.Property(e => e.Puttype)
                .HasMaxLength(2)
                .HasDefaultValueSql("''")
                .HasComment("放置类型 通用:0  地面：1  墙面：2  顶面：3    参数使用模型使用的贴图：10")
                .HasColumnName("puttype")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Size)
                .HasColumnType("text")
                .HasColumnName("size")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasComment("0:普通 1：高光  2：平光  3：哑光")
                .HasColumnName("type");
            entity.Property(e => e.UserId)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasColumnName("UserID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Uuid)
                .HasColumnType("text")
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<MaterialcxBackup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("materialcx_backup")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.AccountType)
                .HasDefaultValueSql("'0'")
                .HasColumnName("accountType");
            entity.Property(e => e.Attribute)
                .HasColumnType("text")
                .HasColumnName("attribute")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class1)
                .HasColumnType("text")
                .HasColumnName("class1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class2)
                .HasColumnType("text")
                .HasColumnName("class2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CompanyId)
                .HasDefaultValueSql("'2'")
                .HasColumnName("companyID");
            entity.Property(e => e.File)
                .HasColumnType("text")
                .HasColumnName("file")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Filesize)
                .HasColumnType("text")
                .HasColumnName("filesize");
            entity.Property(e => e.Materialname)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasComment("贴图中文名称")
                .HasColumnName("materialname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Mode)
                .HasDefaultValueSql("'0'")
                .HasComment("0 3D相关 1 vrscene相关 2 缩略图")
                .HasColumnName("mode");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'0'")
                .HasColumnName("price");
            entity.Property(e => e.Puttype)
                .HasMaxLength(2)
                .HasDefaultValueSql("''")
                .HasComment("放置类型 通用:0  地面：1  墙面：2  顶面：3    参数使用模型使用的贴图：10")
                .HasColumnName("puttype")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Size)
                .HasColumnType("text")
                .HasColumnName("size")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasComment("0:普通 1：高光  2：平光  3：哑光")
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Uuid)
                .HasColumnType("text")
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<MaterialcxBuy>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("materialcx_buy")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.AccountType)
                .HasDefaultValueSql("'0'")
                .HasColumnName("accountType");
            entity.Property(e => e.Attribute)
                .HasColumnType("text")
                .HasColumnName("attribute")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class1)
                .HasColumnType("text")
                .HasColumnName("class1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class2)
                .HasColumnType("text")
                .HasColumnName("class2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CompanyId)
                .HasDefaultValueSql("'2'")
                .HasColumnName("companyID");
            entity.Property(e => e.File)
                .HasColumnType("text")
                .HasColumnName("file")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Filesize)
                .HasColumnType("text")
                .HasColumnName("filesize");
            entity.Property(e => e.Materialname)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasComment("贴图中文名称")
                .HasColumnName("materialname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Mode)
                .HasDefaultValueSql("'0'")
                .HasComment("0 3D相关 1 vrscene相关 2 缩略图")
                .HasColumnName("mode");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'0'")
                .HasColumnName("price");
            entity.Property(e => e.Puttype)
                .HasMaxLength(1)
                .HasDefaultValueSql("''")
                .HasComment("放置类型 通用:0  地面：1  墙面：2  顶面：3")
                .HasColumnName("puttype")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Size)
                .HasColumnType("text")
                .HasColumnName("size")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasComment("0 面地 1 墙面 2 顶面")
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Uuid)
                .HasColumnType("text")
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<Modelcx>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("modelcx")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.AccountType)
                .HasDefaultValueSql("'0'")
                .HasColumnName("accountType");
            entity.Property(e => e.Attribute)
                .HasColumnType("text")
                .HasColumnName("attribute")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class1)
                .HasColumnType("text")
                .HasColumnName("class1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class2)
                .HasColumnType("text")
                .HasColumnName("class2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class3)
                .HasColumnType("text")
                .HasColumnName("class3")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CompanyId)
                .HasDefaultValueSql("'2'")
                .HasColumnName("companyID");
            entity.Property(e => e.Extend)
                .HasMaxLength(1024)
                .HasDefaultValueSql("'{}'")
                .HasColumnName("extend")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.File)
                .HasColumnType("text")
                .HasColumnName("file")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Filesize)
                .HasColumnType("text")
                .HasColumnName("filesize");
            entity.Property(e => e.Materialreplace)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'")
                .HasComment("0:不可替换材质  1：可以替换材质")
                .HasColumnName("materialreplace")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Mode)
                .HasComment("0 3D相关 1 vrscene相关 2 缩略图")
                .HasColumnName("mode");
            entity.Property(e => e.Modelformat)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .HasColumnName("modelformat");
            entity.Property(e => e.Modelname)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasComment("模型中文名称")
                .HasColumnName("modelname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'0'")
                .HasColumnName("price");
            entity.Property(e => e.Size)
                .HasColumnType("text")
                .HasColumnName("size")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Style)
                .HasMaxLength(5)
                .HasDefaultValueSql("'-1'")
                .HasComment("模型样式\r\n门 100：单开门  101：双开门 10 2：2扇推拉门  103.3扇推拉门  104. 4扇推拉门   105. 门洞")
                .HasColumnName("style")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasComment("0 面地 1 墙面 2 顶面  10 可编辑资源中模型数据")
                .HasColumnName("type");
            entity.Property(e => e.UserId)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasColumnName("UserID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Uuid)
                .HasColumnType("text")
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<ModelcxBackup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("modelcx_backup")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.AccountType)
                .HasDefaultValueSql("'0'")
                .HasColumnName("accountType");
            entity.Property(e => e.Attribute)
                .HasColumnType("text")
                .HasColumnName("attribute")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class1)
                .HasColumnType("text")
                .HasColumnName("class1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class2)
                .HasColumnType("text")
                .HasColumnName("class2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class3)
                .HasColumnType("text")
                .HasColumnName("class3")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CompanyId)
                .HasDefaultValueSql("'2'")
                .HasColumnName("companyID");
            entity.Property(e => e.Extend)
                .HasMaxLength(1024)
                .HasDefaultValueSql("'{}'")
                .HasColumnName("extend")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.File)
                .HasColumnType("text")
                .HasColumnName("file")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Filesize)
                .HasColumnType("text")
                .HasColumnName("filesize");
            entity.Property(e => e.Materialreplace)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'")
                .HasComment("0:不可替换材质  1：可以替换材质")
                .HasColumnName("materialreplace")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Mode)
                .HasComment("0 3D相关 1 vrscene相关 2 缩略图")
                .HasColumnName("mode");
            entity.Property(e => e.Modelformat)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .HasColumnName("modelformat");
            entity.Property(e => e.Modelname)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasComment("模型中文名称")
                .HasColumnName("modelname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'0'")
                .HasColumnName("price");
            entity.Property(e => e.Size)
                .HasColumnType("text")
                .HasColumnName("size")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Style)
                .HasMaxLength(5)
                .HasDefaultValueSql("'-1'")
                .HasComment("模型样式\r\n门 100：单开门  101：双开门 10 2：2扇推拉门  103.3扇推拉门  104. 4扇推拉门   105. 门洞")
                .HasColumnName("style")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasComment("0 面地 1 墙面 2 顶面")
                .HasColumnName("type");
            entity.Property(e => e.UserId)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasColumnName("UserID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Uuid)
                .HasColumnType("text")
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<ModelcxBuy>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("modelcx_buy")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.AccountType)
                .HasDefaultValueSql("'0'")
                .HasColumnName("accountType");
            entity.Property(e => e.Attribute)
                .HasColumnType("text")
                .HasColumnName("attribute")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class1)
                .HasColumnType("text")
                .HasColumnName("class1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class2)
                .HasColumnType("text")
                .HasColumnName("class2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class3)
                .HasColumnType("text")
                .HasColumnName("class3")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CompanyId)
                .HasDefaultValueSql("'2'")
                .HasColumnName("companyID");
            entity.Property(e => e.Extend)
                .HasMaxLength(1024)
                .HasDefaultValueSql("'{}'")
                .HasColumnName("extend")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.File)
                .HasColumnType("text")
                .HasColumnName("file")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Filesize)
                .HasColumnType("text")
                .HasColumnName("filesize");
            entity.Property(e => e.Materialreplace)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'")
                .HasComment("0:不可替换材质  1：可以替换材质")
                .HasColumnName("materialreplace")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Mode)
                .HasComment("0 3D相关 1 vrscene相关 2 缩略图")
                .HasColumnName("mode");
            entity.Property(e => e.Modelformat)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .HasColumnName("modelformat");
            entity.Property(e => e.Modelname)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasComment("模型中文名称")
                .HasColumnName("modelname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'0'")
                .HasColumnName("price");
            entity.Property(e => e.Size)
                .HasColumnType("text")
                .HasColumnName("size")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Style)
                .HasMaxLength(5)
                .HasDefaultValueSql("'-1'")
                .HasComment("模型样式\r\n门 100：单开门  101：双开门 10 2：2扇推拉门  103.3扇推拉门  104. 4扇推拉门   105. 门洞")
                .HasColumnName("style")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasComment("0 面地 1 墙面 2 顶面")
                .HasColumnName("type");
            entity.Property(e => e.UserId)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasColumnName("UserID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Uuid)
                .HasColumnType("text")
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<Mymaterialcx>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("mymaterialcx")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Class1)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasComment("类分1")
                .HasColumnName("class1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Class2)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasComment("分类2")
                .HasColumnName("class2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Companyid)
                .HasMaxLength(16)
                .HasDefaultValueSql("''")
                .HasComment("公司id")
                .HasColumnName("companyid")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Date)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasComment("上传日期")
                .HasColumnName("date")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.File)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("文件相对路径")
                .HasColumnName("file")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasMaxLength(16)
                .HasDefaultValueSql("''")
                .HasComment("片图名称")
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Size)
                .HasMaxLength(16)
                .HasDefaultValueSql("''")
                .HasComment("片图尺寸")
                .HasColumnName("size")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.User)
                .HasMaxLength(16)
                .HasDefaultValueSql("''")
                .HasComment("户用帐号")
                .HasColumnName("user")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<OutdoorPicture>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("outdoor_picture")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Companyid)
                .HasColumnType("text")
                .HasColumnName("companyid")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.File)
                .HasColumnType("text")
                .HasColumnName("file")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasDefaultValueSql("'0'")
                .HasComment("0:室内 1：室外白天  2:室外夜晚")
                .HasColumnName("type")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.User)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasColumnName("user")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Renderanimation>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PRIMARY");

            entity
                .ToTable("renderanimation")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.FileId)
                .HasMaxLength(50)
                .HasColumnName("FileID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CreateTime)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.FileName)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.FilePath)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.VideoFps)
                .HasMaxLength(5)
                .HasDefaultValueSql("''")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.VideoHeight)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.VideoWidth)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Renderdatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("renderdata")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ImageName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasComment("渲染生成的图片名称")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasComment("渲染生成的图片路径")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ImageSize)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasComment("渲染生成图片尺寸")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ImageType)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasComment("渲染生成的图片类型：0 效果图  1：全景")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RenderTime)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasComment("渲染时间")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasComment("生成的缩略图名称")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserAccount)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasComment("渲染用户帐号")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.WebName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasComment("渲染全景时生成的网页名称")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Renderimage>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("renderimage", tb => tb.HasComment("保存渲染时回传图片"))
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.FileId)
                .HasMaxLength(64)
                .HasColumnName("FileID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ImageIndex)
                .HasMaxLength(5)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PathFileName)
                .HasMaxLength(128)
                .HasDefaultValueSql("''")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Renderqueue>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("renderqueue")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.FileId)
                .HasColumnType("text")
                .HasColumnName("FileID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ImageFile)
                .HasColumnType("text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Progress)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RenderVersion)
                .HasMaxLength(10)
                .HasDefaultValueSql("'3.0'")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RenderingPictrue)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'0'")
                .HasComment("0：ready  1:rendering  2:finish");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.TotalPicture)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserId)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasColumnName("UserID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserSchemePath)
                .HasColumnType("text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UvFile)
                .HasColumnType("text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<SchemeExhibition>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("scheme_exhibition")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.CreateTime)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasColumnName("create_time")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Id)
                .HasMaxLength(128)
                .HasColumnName("id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProjectData)
                .HasDefaultValueSql("''")
                .HasColumnType("varchar(20480)")
                .HasColumnName("project_data")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasColumnName("project_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProjectThumbnail)
                .HasMaxLength(512)
                .HasDefaultValueSql("''")
                .HasColumnName("project_thumbnail")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserAccount)
                .HasMaxLength(64)
                .HasColumnName("user_account")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<SchemeFailed>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("scheme_failed")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.CompanyName)
                .HasMaxLength(128)
                .HasDefaultValueSql("''")
                .HasColumnName("company_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CreateTime)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasColumnName("create_time")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Id)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasComment("方案id")
                .HasColumnName("id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OtherFailed)
                .HasColumnName("other_failed")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RenderFailed)
                .HasComment("渲染失败原因")
                .HasColumnName("render_failed")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SendFailed)
                .HasMaxLength(128)
                .HasDefaultValueSql("''")
                .HasComment("发送失败原因")
                .HasColumnName("send_failed")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Sharescene>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sharescene")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Class)
                .HasMaxLength(256)
                .HasDefaultValueSql("'其它'")
                .HasComment("分类名称")
                .HasColumnName("class");
            entity.Property(e => e.Companyid)
                .HasMaxLength(64)
                .HasDefaultValueSql("'2'")
                .HasColumnName("companyid");
            entity.Property(e => e.Folder)
                .HasColumnType("text")
                .HasColumnName("folder")
                .UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");
            entity.Property(e => e.Modelcount)
                .HasMaxLength(5)
                .HasDefaultValueSql("'0'")
                .HasColumnName("modelcount");
            entity.Property(e => e.Scenename)
                .HasColumnType("text")
                .HasColumnName("scenename");
            entity.Property(e => e.State)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'")
                .HasComment("0:下架 1：上架  2：删除")
                .HasColumnName("state");
            entity.Property(e => e.Thumbnail1)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasColumnName("thumbnail1");
            entity.Property(e => e.Thumbnail2)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasColumnName("thumbnail2");
            entity.Property(e => e.Thumbnail3)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasColumnName("thumbnail3");
            entity.Property(e => e.Username)
                .HasColumnType("text")
                .HasColumnName("username")
                .UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");
        });

        modelBuilder.Entity<SharesceneBuy>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sharescene_buy")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Class)
                .HasMaxLength(256)
                .HasDefaultValueSql("'其它'")
                .HasComment("分类名称")
                .HasColumnName("class");
            entity.Property(e => e.Companyid)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasColumnName("companyid");
            entity.Property(e => e.Folder)
                .HasColumnType("text")
                .HasColumnName("folder")
                .UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");
            entity.Property(e => e.Modelcount)
                .HasMaxLength(5)
                .HasDefaultValueSql("'0'")
                .HasColumnName("modelcount");
            entity.Property(e => e.Scenename)
                .HasColumnType("text")
                .HasColumnName("scenename");
            entity.Property(e => e.State)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'")
                .HasComment("0:下架 1：上传  2：删除")
                .HasColumnName("state");
            entity.Property(e => e.Thumbnail1)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasColumnName("thumbnail1");
            entity.Property(e => e.Thumbnail2)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasColumnName("thumbnail2");
            entity.Property(e => e.Thumbnail3)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasColumnName("thumbnail3");
            entity.Property(e => e.Username)
                .HasColumnType("text")
                .HasColumnName("username")
                .UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity
                .ToTable("user")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Accountname)
                .HasColumnType("text")
                .HasColumnName("accountname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Accounttype)
                .HasDefaultValueSql("'0'")
                .HasComment("0 企业  1：设计师  2：普通用户 11:超级用户")
                .HasColumnName("accounttype");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Administrator)
                .HasColumnType("text")
                .HasColumnName("administrator");
            entity.Property(e => e.Authcode)
                .HasColumnType("text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AvatarIcon).HasColumnType("text");
            entity.Property(e => e.CompanyId)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasColumnName("CompanyID")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Creater)
                .HasColumnType("text")
                .HasColumnName("creater")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Createrid)
                .HasColumnType("text")
                .HasColumnName("createrid")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Createtime)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasColumnName("createtime")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.EmailVerificationCode).HasMaxLength(255);
            entity.Property(e => e.EnableTime)
                .HasColumnType("text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.HousetypeAuthorization)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'")
                .HasComment("户型上传：1 充许  0：禁止")
                .HasColumnName("housetype_authorization")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.HousetypeCheckAuthorization)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'")
                .HasComment("户型审核：1 充许  0：禁止")
                .HasColumnName("housetype_check_authorization")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Lasttime)
                .HasColumnType("datetime")
                .HasColumnName("lasttime");
            entity.Property(e => e.MailAddress).HasMaxLength(255);
            entity.Property(e => e.MasterAuthorization)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'")
                .HasComment("设置大师方案： 1 充许  0：禁止")
                .HasColumnName("master_authorization")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Password)
                .HasColumnType("text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Permissions).HasDefaultValueSql("'0'");
            entity.Property(e => e.Qq)
                .HasColumnType("text")
                .HasColumnName("QQ")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RefineAuthorization)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'")
                .HasComment("设置精装方案:1 充许  0：禁止")
                .HasColumnName("refine_authorization")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SchemeCheckAuthorization)
                .HasMaxLength(1)
                .HasDefaultValueSql("'0'")
                .HasComment("方案审核：1 充许  0：禁止")
                .HasColumnName("scheme_check_authorization")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Tel)
                .HasColumnType("text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Textdesc)
                .HasColumnType("text")
                .HasColumnName("textdesc")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UserName)
                .HasColumnType("text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Wxshare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("wxshare")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasColumnName("id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Address)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("需要分享的地址")
                .HasColumnName("address")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasDefaultValueSql("''")
                .HasComment("分享描述")
                .HasColumnName("description")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Reserve)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("预留字段")
                .HasColumnName("reserve")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("分享显示的缩略图")
                .HasColumnName("thumbnail")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Title)
                .HasMaxLength(256)
                .HasDefaultValueSql("''")
                .HasComment("标题")
                .HasColumnName("title")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
