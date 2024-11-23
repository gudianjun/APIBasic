namespace APIBasic.Models;

public partial class Modelcx
{
    public string? Class1 { get; set; }

    public string? Class2 { get; set; }

    public string? Class3 { get; set; }

    public string? Name { get; set; }

    public string? Size { get; set; }

    public string? File { get; set; }

    /// <summary>
    /// 0 面地 1 墙面 2 顶面  10 可编辑资源中模型数据
    /// </summary>
    public int? Type { get; set; }

    /// <summary>
    /// 0 3D相关 1 vrscene相关 2 缩略图
    /// </summary>
    public int? Mode { get; set; }

    public string? Filesize { get; set; }

    public string Uuid { get; set; } = null!;

    public float? Price { get; set; }

    public int? AccountType { get; set; }

    public int? CompanyId { get; set; }

    public string? UserId { get; set; }

    public string? Attribute { get; set; }

    public string? Modelformat { get; set; }

    /// <summary>
    /// 模型中文名称
    /// </summary>
    public string? Modelname { get; set; }

    /// <summary>
    /// 0:不可替换材质  1：可以替换材质
    /// </summary>
    public string? Materialreplace { get; set; }

    /// <summary>
    /// 模型样式
    /// 门 100：单开门  101：双开门 10 2：2扇推拉门  103.3扇推拉门  104. 4扇推拉门   105. 门洞
    /// </summary>
    public string? Style { get; set; }

    public string? Extend { get; set; }
}
