using CandidateApplicationManager.Api.Core;
using CandidateApplicationManager.Core;
using CandidateApplicationManager.Entities;
using CandidateApplicationManager.Models.Api;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CandidateApplicationManager.Services
{
    public class JwtService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public JwtService(IUserRepository userRepository, IConfiguration configuration1) 
        { 
            _userRepository = userRepository;
            _configuration = configuration1;
        }

        public async Task<LoginResponseModel?> Authenticate(LoginRequestModel loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.UserName) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return null;
            }

            GeneralUser generalUser = await _userRepository.GetUserByIdAsync(loginRequest.UserName);

            if (generalUser != null) 
            { 

            }

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var toeknValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokeExpiryTimeStamp = DateTime.UtcNow.AddMinutes(toeknValidityMins);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, loginRequest.UserName)
                }),
                Expires = tokeExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byte[100000,1000])),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponseModel
            {
                AccessToken = accessToken,
                UserName = loginRequest.UserName,
                ExpiresIn = (int)tokeExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }
    }
}
