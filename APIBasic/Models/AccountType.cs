namespace APIBasic.Models;

/// <summary>
/// 帐号类型
/// 
/// </summary>
public partial class AccountType
{
    /// <summary>
    /// 帐号类型
    /// </summary>
    public uint Id { get; set; }

    /// <summary>
    /// 号帐类型名称(0:普通帐号 1：企业帐号 2：设计师帐号 11：管理员帐号)
    /// </summary>
    public string? Name { get; set; }
}
