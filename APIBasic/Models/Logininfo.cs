using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class Logininfo
{
    public string UserName { get; set; } = null!;

    public int? CompanyId { get; set; }

    public string? UserIp { get; set; }

    public string? LoginTime { get; set; }
}
