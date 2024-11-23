namespace APIBasic.Models;

public partial class FunctionModule
{
    /// <summary>
    /// 公司id
    /// </summary>
    public string? CompanyId { get; set; }

    /// <summary>
    /// 指定系统功能模块是否可用
    /// </summary>
    public string? Function { get; set; }
}
