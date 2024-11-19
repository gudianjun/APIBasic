using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace APIBasic.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class DesignsController : ControllerBase
    {
        private readonly ITopWindowService _topWindowService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DesignsController> _logger;
        private readonly IMemoryCache _memoryCache;
        public DesignsController(IConfiguration configuration, ITopWindowService topWindowService
            , ILogger<DesignsController> logger, IMemoryCache memoryCache)
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
        [HttpGet("type/{type}")]
        public async Task<ActionResult<GetTypeDesignsResponse>> GetTypeDesigns(string type, [FromBody] GetTypeDesignsRequest request)
        {
            return (new ApiResponse<GetTypeDesignsResponse>(null)).Result();
        }
        /// <summary>
        /// 检索设计信息。
        /// 传入查询参数，包括户型名称等
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("Designs")]
        public async Task<ActionResult<GetDesignsResponse>> GetDesigns([FromBody] GetDesignsRequest request)
        {
            return (new ApiResponse<GetDesignsResponse>(null)).Result();
        }

        /// <summary>
        /// 得到指定设计ID的，设计详细信息。
        /// </summary>
        /// <param name="designId"></param>
        /// <returns></returns>
        [HttpGet("details/{designId}")]
        [Authorize]
        public async Task<ActionResult<GetDesignDetailsResponse>> GetDesignDetails(int designId)
        {
            return (new ApiResponse<GetDesignDetailsResponse>(null)).Result();
        }
    }
}
