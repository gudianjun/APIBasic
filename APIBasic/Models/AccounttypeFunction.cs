using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class AccounttypeFunction
{
    public uint Type { get; set; }

    /// <summary>
    /// 帐号类型名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 功能列表
    /// </summary>
    public string? FunctionName { get; set; }

    public int? Companyid { get; set; }

    /// <summary>
    /// 创建的帐号类型默认为普通帐号
    /// </summary>
    public int? AccountType { get; set; }
}
