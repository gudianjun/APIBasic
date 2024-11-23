using APIBasic.Models;

namespace APIBasic.DTOs
{
    public class DesignBaseInfo
    {
        public DesignBaseInfo(DesignFile designFile)
        {
            FileId = designFile.FileId;
            CurrentVersion = designFile.CurrentVersion;
            ResourceType = designFile.ResourceType;
            ResourceName = designFile.ResourceName;
            DeviceType = designFile.DeviceType;
            UserId = designFile.UserId;
            LastUpdatedTime = designFile.LastUpdatedTime;
            CreatedTime = designFile.CreatedTime;
        }
        public string FileId { get; set; } = null!;

        public int CurrentVersion { get; set; }

        public string ResourceType { get; set; } = null!;

        public string ResourceName { get; set; } = null!;

        public string DeviceType { get; set; } = null!;

        public uint? UserId { get; set; }

        public DateTime? LastUpdatedTime { get; set; }

        public DateTime CreatedTime { get; set; }
    }
    public class GetFilesResponse
    {
        public List<DesignBaseInfo> DesignFiles { get; set; } = new List<DesignBaseInfo>();
    }
}
