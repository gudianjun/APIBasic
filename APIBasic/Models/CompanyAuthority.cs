using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class CompanyAuthority
{
    /// <summary>
    /// 公司id
    /// </summary>
    public string Companyid { get; set; } = null!;

    /// <summary>
    /// 公司开通的网址
    /// </summary>
    public string? Webaddr { get; set; }

    /// <summary>
    /// 开通日期
    /// </summary>
    public string? Begindate { get; set; }

    /// <summary>
    /// 终止日期
    /// </summary>
    public string? Enddate { get; set; }

    /// <summary>
    /// 开通时长
    /// </summary>
    public string? Days { get; set; }

    /// <summary>
    /// 通开状态：0没开通 1：开通 
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// 创建日期
    /// </summary>
    public string? Createdate { get; set; }
}
