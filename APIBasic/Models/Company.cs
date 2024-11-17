using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class Company
{
    public uint CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public string? LogoMenu { get; set; }

    public string? LogoSetting { get; set; }

    public string? Folder { get; set; }

    public int? Money { get; set; }

    public uint? Version { get; set; }

    public string? CreateTime { get; set; }

    public string? Address { get; set; }

    public string? Telphone { get; set; }

    public string? Mobile { get; set; }

    public string? Contacts { get; set; }

    public string? Email { get; set; }

    public string? WebName { get; set; }

    public string? WebLogImage { get; set; }

    public string? WebTitleImage { get; set; }

    public string? UserNumber { get; set; }

    public string? EmbedWeb { get; set; }

    /// <summary>
    /// 设计师帐号数量
    /// </summary>
    public string? DesignerNumber { get; set; }

    /// <summary>
    /// 普通用户帐号数量
    /// </summary>
    public string? NormalNumber { get; set; }

    /// <summary>
    /// 牌品名称
    /// </summary>
    public string? BrandName { get; set; }

    /// <summary>
    /// 品牌图片
    /// </summary>
    public string? BrandImage { get; set; }
}
