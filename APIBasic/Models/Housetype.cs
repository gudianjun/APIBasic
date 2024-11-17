using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class Housetype
{
    /// <summary>
    /// 唯一id,这个值为保存方案目录
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// 缩略图
    /// </summary>
    public string? Thumbnail { get; set; }

    /// <summary>
    /// 场景名
    /// </summary>
    public string? Scenename { get; set; }

    /// <summary>
    /// 楼盘/小区名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 型户
    /// </summary>
    public string? Housetype1 { get; set; }

    /// <summary>
    /// 积面
    /// </summary>
    public string? Area { get; set; }

    public string? Createtime { get; set; }

    /// <summary>
    /// 份省
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// 市城
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 体具地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 计设师
    /// </summary>
    public string? Designer { get; set; }

    /// <summary>
    /// 计设师帐号
    /// </summary>
    public string? Designerid { get; set; }

    /// <summary>
    /// 司公id
    /// </summary>
    public string? Companyid { get; set; }

    /// <summary>
    /// 于用模糊查找
    /// </summary>
    public string? Fuzzysearch { get; set; }

    /// <summary>
    /// 盘开时间
    /// </summary>
    public string? Selldate { get; set; }

    /// <summary>
    /// 类型（公寓还是酒店）
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 留保
    /// </summary>
    public string? Reserver1 { get; set; }

    /// <summary>
    /// 留保
    /// </summary>
    public string? Reserver2 { get; set; }

    /// <summary>
    /// 留保
    /// </summary>
    public string? Reserver3 { get; set; }

    public string? Version { get; set; }
}
