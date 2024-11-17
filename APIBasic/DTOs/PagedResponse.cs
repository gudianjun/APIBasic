namespace APIBasic.DTOs
{
    public class PagedResponse<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public long TotalRecords { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

        /// <summary>
        /// 当前页的数据
        /// </summary>
        public List<T> Data { get; set; }

        public PagedResponse()
        {
            Data = new List<T>();
        }

        public PagedResponse(List<T> data, int pageNumber, int pageSize, long totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }
    }
}
