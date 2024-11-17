using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class Renderqueue
{
    public string? FileId { get; set; }

    public string? ImageFile { get; set; }

    public string? UvFile { get; set; }

    /// <summary>
    /// 0：ready  1:rendering  2:finish
    /// </summary>
    public int? Status { get; set; }

    public DateTime? DateTime { get; set; }

    public string? UserId { get; set; }

    public string? Progress { get; set; }

    public string? UserSchemePath { get; set; }

    public string? Thumbnail { get; set; }

    public string? RenderVersion { get; set; }

    public string? RenderingPictrue { get; set; }

    public string? TotalPicture { get; set; }
}
