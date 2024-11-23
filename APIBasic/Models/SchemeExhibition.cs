namespace APIBasic.Models;

public partial class SchemeExhibition
{
    public string Id { get; set; } = null!;

    public string UserAccount { get; set; } = null!;

    public string? ProjectName { get; set; }

    public string? ProjectThumbnail { get; set; }

    public string? ProjectData { get; set; }

    public string? CreateTime { get; set; }
}
