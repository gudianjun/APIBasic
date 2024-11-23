namespace APIBasic.Models;

public partial class MaterialcxBackup
{
    public string? Class1 { get; set; }

    public string? Class2 { get; set; }

    public string? File { get; set; }

    public string? Name { get; set; }

    public string? Size { get; set; }

    /// <summary>
    /// 0:普通 1：高光  2：平光  3：哑光
    /// </summary>
    public int? Type { get; set; }

    public string? Filesize { get; set; }

    /// <summary>
    /// 0 3D相关 1 vrscene相关 2 缩略图
    /// </summary>
    public int? Mode { get; set; }

    public string Uuid { get; set; } = null!;

    public float? Price { get; set; }

    public int? AccountType { get; set; }

    public int? CompanyId { get; set; }

    public int? UserId { get; set; }

    public string? Attribute { get; set; }

    /// <summary>
    /// 贴图中文名称
    /// </summary>
    public string? Materialname { get; set; }

    /// <summary>
    /// 放置类型 通用:0  地面：1  墙面：2  顶面：3    参数使用模型使用的贴图：10
    /// </summary>
    public string? Puttype { get; set; }
}
