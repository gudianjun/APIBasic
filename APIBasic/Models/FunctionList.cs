using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class FunctionList
{
    public int Id { get; set; }

    public string? UserAccount { get; set; }

    public string? CompanyId { get; set; }

    /// <summary>
    /// 可以使用的功能,以逗号分隔
    /// </summary>
    public string? FunctionName { get; set; }
}
