using APIBasic.DTOs;
using APIBasic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Org.BouncyCastle.Asn1.Ocsp;
using System.ComponentModel.DataAnnotations;

namespace APIBasic.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class DesignFilesController : ControllerBase
    {
        private readonly ITopWindowService _topWindowService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DesignFilesController> _logger;
        private readonly IMemoryCache _memoryCache;
        public DesignFilesController(IConfiguration configuration, ITopWindowService topWindowService
            , ILogger<DesignFilesController> logger, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _topWindowService = topWindowService;
            _configuration = configuration;
        }
        /// <summary>
        /// 分类检索户型设计信息。
        /// 通过传递的Request参数，来区分检索类型，
        /// 1，户型图
        /// 2，3D设计图
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<GetFilesResponse>> GetFiles()
        {
            var response = await _topWindowService.GetFilesAsync();
            return response;
        }

        /// <summary>
        /// 下载文件内容
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>文件内容</returns>
        [HttpGet("{fileId}")]
        [Authorize]
        public async Task<ActionResult<DownloadFileResponse>> DownloadFileContent([Required]string fileId)
        {
            var response = await _topWindowService.DownloadFileAsync(fileId);
            return response;
        }

        /// <summary>
        /// 新规一个文件
        /// </summary>
        /// <param name="request">新文件请求</param>
        /// <returns>创建结果</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateFileResponse>> CreateFile([FromBody] CreateFileRequest request)
        {
            var response = await _topWindowService.CreateFileAsync(request);
            return response;
        }

        /// <summary>
        /// 删除一个文件
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>删除结果</returns>
        [HttpDelete("{fileId}")]
        [Authorize]
        public async Task<ActionResult<DeleteFileResponse>> DeleteFile([Required] string fileId)
        {
            var response = await _topWindowService.DeleteFileAsync(fileId);
            return response;
        }

        /// <summary>
        /// 更新文件内容
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <param name="request">更新文件请求</param>
        /// <returns>更新结果</returns>
        [HttpPut("{fileId}")]
        [Authorize]
        public async Task<ActionResult<UpdateFileResponse>> UpdateFile([Required]string fileId, [FromBody] UpdateFileRequest request)
        {
            var response = await _topWindowService.UpdateFileAsync(fileId, request);
            return response;
        }
    }
}
