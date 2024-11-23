namespace APIBasic.Models;

public partial class SchemeFailed
{
    /// <summary>
    /// 方案id
    /// </summary>
    public string? Id { get; set; }

    public string? CompanyName { get; set; }

    /// <summary>
    /// 渲染失败原因
    /// </summary>
    public string? RenderFailed { get; set; }

    /// <summary>
    /// 发送失败原因
    /// </summary>
    public string? SendFailed { get; set; }

    public string? OtherFailed { get; set; }

    public string? CreateTime { get; set; }
}
