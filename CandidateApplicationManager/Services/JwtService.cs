using CandidateApplicationManager.Api.Core;
using CandidateApplicationManager.Models.Api;

namespace CandidateApplicationManager.Services
{
    public class JwtService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IConfiguration _configuration;
        public JwtService(IApplicationRepository applicationRepository, IConfiguration configuration1) 
        { 
            _applicationRepository = applicationRepository;
            _configuration = configuration1;
        }

        public LoginResponseModel Authenticate(LoginRequestModel loginRequestModel)
        {
            return new LoginResponseModel();
        }
    }
}
