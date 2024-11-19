using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class FileManagement
{
    public string FileId { get; set; } = null!;

    public int CurrentVersion { get; set; }

    public string ResourceType { get; set; } = null!;

    public string ResourceName { get; set; } = null!;

    public string DeviceType { get; set; } = null!;

    public uint? UserId { get; set; }

    public long? FileSize { get; set; }

    public string? FileFormat { get; set; }

    public DateTime? LastUpdatedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public string FileContent { get; set; } = null!;

    public string? Remarks { get; set; }
}
