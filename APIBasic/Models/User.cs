using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class User
{
    public uint UserId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Qq { get; set; }

    public string? Tel { get; set; }

    public string? EnableTime { get; set; }

    public string? CompanyId { get; set; }

    public string? Authcode { get; set; }

    public int? Permissions { get; set; }

    public string? Textdesc { get; set; }

    public DateTime? Lasttime { get; set; }

    public string Administrator { get; set; } = null!;

    /// <summary>
    /// 0 企业  1：设计师  2：普通用户 11:超级用户
    /// </summary>
    public int? Accounttype { get; set; }

    public string? Creater { get; set; }

    public string? Createrid { get; set; }

    public string? Accountname { get; set; }

    /// <summary>
    /// 设置精装方案:1 充许  0：禁止
    /// </summary>
    public string? RefineAuthorization { get; set; }

    /// <summary>
    /// 设置大师方案： 1 充许  0：禁止
    /// </summary>
    public string? MasterAuthorization { get; set; }

    /// <summary>
    /// 户型上传：1 充许  0：禁止
    /// </summary>
    public string? HousetypeAuthorization { get; set; }

    /// <summary>
    /// 方案审核：1 充许  0：禁止
    /// </summary>
    public string? SchemeCheckAuthorization { get; set; }

    /// <summary>
    /// 户型审核：1 充许  0：禁止
    /// </summary>
    public string? HousetypeCheckAuthorization { get; set; }

    public string? Createtime { get; set; }

    public string? Address { get; set; }

    public string? Name { get; set; }

    public string? CompanyName { get; set; }

    public string? EmailVerificationCode { get; set; }

    public string? AvatarIcon { get; set; }

    public string? MailAddress { get; set; }
}
