using APIBasic.Models;

namespace APIBasic.DTOs
{
    public class GetFilesResponse
    {
        public List<DesignFile> DesignFiles { get; set; } = new List<DesignFile>();
    }
}
