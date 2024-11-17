using APIBasic.Models;

namespace APIBasic.Services.Interfaces
{
    public interface ISampleService
    {
        Logininfo GetLogininfo(string username, string password);
    }
}
