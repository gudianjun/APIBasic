namespace APIBasic.Models;

/// <summary>
/// 保存渲染时回传图片
/// </summary>
public partial class Renderimage
{
    public string FileId { get; set; } = null!;

    public string ImageIndex { get; set; } = null!;

    public string? PathFileName { get; set; }
}
