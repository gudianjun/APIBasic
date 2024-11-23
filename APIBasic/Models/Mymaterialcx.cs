namespace APIBasic.Models;

public partial class Mymaterialcx
{
    public int Id { get; set; }

    /// <summary>
    /// 户用帐号
    /// </summary>
    public string? User { get; set; }

    /// <summary>
    /// 公司id
    /// </summary>
    public string? Companyid { get; set; }

    /// <summary>
    /// 片图名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 文件相对路径
    /// </summary>
    public string? File { get; set; }

    /// <summary>
    /// 片图尺寸
    /// </summary>
    public string? Size { get; set; }

    /// <summary>
    /// 类分1
    /// </summary>
    public string? Class1 { get; set; }

    /// <summary>
    /// 分类2
    /// </summary>
    public string? Class2 { get; set; }

    /// <summary>
    /// 上传日期
    /// </summary>
    public string? Date { get; set; }
}
