using APIBasic.Models;
using APIBasic.Services.Interfaces;

namespace APIBasic.Services.Implementations
{
    public class SampleService : ISampleService
    {
        public SampleService() 
        { 
        }

        public Logininfo GetLogininfo(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
