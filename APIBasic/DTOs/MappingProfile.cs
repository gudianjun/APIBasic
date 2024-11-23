using APIBasic.Models;
using APIBasic.Utilities;
using AutoMapper;

namespace APIBasic.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, User>()
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Password, opt => opt.MapFrom(src => StringHelper.HashPassword(src.Password)))
             .ForMember(dest => dest.Qq, opt => opt.MapFrom(src => (string?)null)) // 如果有 QQ 字段，请根据需要设置
             .ForMember(dest => dest.Tel, opt => opt.MapFrom(src => (string?)null)) // 如果有电话字段，请根据需要设置
             .ForMember(dest => dest.EnableTime, opt => opt.MapFrom(src => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")))
             .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => (string?)null)) // 如果有公司 ID 字段，请根据需要设置
             .ForMember(dest => dest.Authcode, opt => opt.MapFrom(src => (string?)null)) // 如果有授权码字段，请根据需要设置
             .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => (int?)null)) // 如果有权限字段，请根据需要设置
             .ForMember(dest => dest.Textdesc, opt => opt.MapFrom(src => (string?)null)) // 如果有描述字段，请根据需要设置
             .ForMember(dest => dest.Lasttime, opt => opt.MapFrom(src => DateTime.Now))
             .ForMember(dest => dest.Administrator, opt => opt.MapFrom(src => "0"))
             .ForMember(dest => dest.Accounttype, opt => opt.MapFrom(src => 2))
             .ForMember(dest => dest.Creater, opt => opt.MapFrom(src => (string?)null)) // 如果有创建者字段，请根据需要设置
             .ForMember(dest => dest.Createrid, opt => opt.MapFrom(src => (string?)null)) // 如果有创建者 ID 字段，请根据需要设置
             .ForMember(dest => dest.Accountname, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.RefineAuthorization, opt => opt.MapFrom(src => "0"))
             .ForMember(dest => dest.MasterAuthorization, opt => opt.MapFrom(src => "0"))
             .ForMember(dest => dest.HousetypeAuthorization, opt => opt.MapFrom(src => "0"))
             .ForMember(dest => dest.SchemeCheckAuthorization, opt => opt.MapFrom(src => "0"))
             .ForMember(dest => dest.HousetypeCheckAuthorization, opt => opt.MapFrom(src => "0"))
             .ForMember(dest => dest.Createtime, opt => opt.MapFrom(src => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")))
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
             .ForMember(dest => dest.EmailVerificationCode, opt => opt.MapFrom(src => (string?)null)) // 如果有邮箱验证代码字段，请根据需要设置
             .ForMember(dest => dest.AvatarIcon, opt => opt.MapFrom(src => (string?)null)) // 如果有头像字段，请根据需要设置
             .ForMember(dest => dest.MailAddress, opt => opt.MapFrom(src => src.MailAddress))
             .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Zip));

            // 转换User，到 GetUserInfoResponse，除了密码以外，原样转换
            CreateMap<User, GetUserInfoResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Qq, opt => opt.MapFrom(src => src.Qq))
            .ForMember(dest => dest.Tel, opt => opt.MapFrom(src => src.Tel))
            .ForMember(dest => dest.EnableTime, opt => opt.MapFrom(src => src.EnableTime))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.Authcode, opt => opt.MapFrom(src => src.Authcode))
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions))
            .ForMember(dest => dest.Textdesc, opt => opt.MapFrom(src => src.Textdesc))
            .ForMember(dest => dest.Lasttime, opt => opt.MapFrom(src => src.Lasttime))
            .ForMember(dest => dest.Administrator, opt => opt.MapFrom(src => src.Administrator))
            .ForMember(dest => dest.Accounttype, opt => opt.MapFrom(src => src.Accounttype))
            .ForMember(dest => dest.Creater, opt => opt.MapFrom(src => src.Creater))
            .ForMember(dest => dest.Createrid, opt => opt.MapFrom(src => src.Createrid))
            .ForMember(dest => dest.Accountname, opt => opt.MapFrom(src => src.Accountname))
            .ForMember(dest => dest.RefineAuthorization, opt => opt.MapFrom(src => src.RefineAuthorization))
            .ForMember(dest => dest.MasterAuthorization, opt => opt.MapFrom(src => src.MasterAuthorization))
            .ForMember(dest => dest.HousetypeAuthorization, opt => opt.MapFrom(src => src.HousetypeAuthorization))
            .ForMember(dest => dest.SchemeCheckAuthorization, opt => opt.MapFrom(src => src.SchemeCheckAuthorization))
            .ForMember(dest => dest.HousetypeCheckAuthorization, opt => opt.MapFrom(src => src.HousetypeCheckAuthorization))
            .ForMember(dest => dest.Createtime, opt => opt.MapFrom(src => src.Createtime))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
            .ForMember(dest => dest.EmailVerificationCode, opt => opt.MapFrom(src => src.EmailVerificationCode))
            .ForMember(dest => dest.AvatarIcon, opt => opt.MapFrom(src => src.AvatarIcon))
            .ForMember(dest => dest.MailAddress, opt => opt.MapFrom(src => src.MailAddress))
            .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Zip));

            CreateMap<CreateFileRequest, DesignFile>()
                .ForMember(dest => dest.FileId, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.CurrentVersion, opt => opt.MapFrom(src => 1)) // 默认版本号为1
                .ForMember(dest => dest.ResourceType, opt => opt.MapFrom(src => src.ResourceType))
                .ForMember(dest => dest.ResourceName, opt => opt.MapFrom(src => src.ResourceName))
                .ForMember(dest => dest.DeviceType, opt => opt.MapFrom(src => "")) // 设备类型信息
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => 0))    // 用户ID
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => DateTime.Now))   // 最后更新时间
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => DateTime.Now))      // 创建时间
                .ForMember(dest => dest.FileContent, opt => opt.MapFrom(src => src.FileContent))
                .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks))
                .ForMember(dest => dest.Thumbnail1, opt => opt.MapFrom(src => src.Thumbnail1))
                .ForMember(dest => dest.Thumbnail2, opt => opt.MapFrom(src => src.Thumbnail2));
        }
    }
}
