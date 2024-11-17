namespace APIBasic.DTOs
{
    abstract public class PagedRequest
    {
        /// <summary>
        /// 页码（从1开始）
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string? SortField { get; set; }

        /// <summary>
        /// 排序方向（asc 或 desc）
        /// </summary>
        public string? SortDirection { get; set; }
    }
}
