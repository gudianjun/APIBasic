namespace APIBasic.Models;

public partial class SharesceneBuy
{
    public string? Scenename { get; set; }

    public string? Folder { get; set; }

    public string? Username { get; set; }

    public string? Modelcount { get; set; }

    public string? Thumbnail1 { get; set; }

    public string? Thumbnail2 { get; set; }

    public string? Thumbnail3 { get; set; }

    /// <summary>
    /// 0:下架 1：上传  2：删除
    /// </summary>
    public string? State { get; set; }

    public string? Companyid { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string? Class { get; set; }
}
