using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class Renderdatum
{
    public string Id { get; set; } = null!;

    /// <summary>
    /// 渲染用户帐号
    /// </summary>
    public string? UserAccount { get; set; }

    /// <summary>
    /// 渲染生成的图片类型：0 效果图  1：全景
    /// </summary>
    public string? ImageType { get; set; }

    /// <summary>
    /// 渲染生成的图片名称
    /// </summary>
    public string? ImageName { get; set; }

    /// <summary>
    /// 生成的缩略图名称
    /// </summary>
    public string? Thumbnail { get; set; }

    /// <summary>
    /// 渲染生成的图片路径
    /// </summary>
    public string? ImagePath { get; set; }

    /// <summary>
    /// 渲染生成图片尺寸
    /// </summary>
    public string? ImageSize { get; set; }

    /// <summary>
    /// 渲染时间
    /// </summary>
    public string? RenderTime { get; set; }

    /// <summary>
    /// 渲染全景时生成的网页名称
    /// </summary>
    public string? WebName { get; set; }
}
