namespace APIBasic.Models;

public partial class Wxshare
{
    public string Id { get; set; } = null!;

    /// <summary>
    /// 需要分享的地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 分享显示的缩略图
    /// </summary>
    public string? Thumbnail { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 分享描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 预留字段
    /// </summary>
    public string? Reserve { get; set; }
}
