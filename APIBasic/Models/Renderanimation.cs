using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class Renderanimation
{
    public string FileId { get; set; } = null!;

    public string? CreateTime { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public string? VideoWidth { get; set; }

    public string? VideoHeight { get; set; }

    public string? VideoFps { get; set; }
}
