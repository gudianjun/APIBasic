using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class OutdoorPicture
{
    public string? Companyid { get; set; }

    public string? File { get; set; }

    /// <summary>
    /// 0:室内 1：室外白天  2:室外夜晚
    /// </summary>
    public string? Type { get; set; }

    public string? User { get; set; }
}
