using AutoMapper;
using ECommerceApp.Core.DTO;
using ECommerceApp.Core.Interface;
using ECommerceApp.Core.Utilities;
using ECommerceApp.Domain.Enum;
using ECommerceApp.Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ECommerceApp.Core.Services
{
    public class AuthService : IAuthService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IRoleService _service;
        private User _user;

        public AuthService(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration config, IRoleService service)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _config = config;
            _service = service;
        }
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtConfig = _config.GetSection("Jwt");
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(jwtConfig.GetSection("lifetime").Value));
            var token = new JwtSecurityToken(
                issuer: jwtConfig.GetSection("Issuer").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );
            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, _user.Email),
                 new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()),
            };
            var roles = await _service.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            //var key = Environment.GetEnvironmentVariable("LOCK");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:token")));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<ResponseDTO<string>> RegisterAsync(RegistrationDTO userDetails)
        {
            var result = new ResponseDTO<string>();
            try
            {
                var checkEmail = await _unitOfWork.UserRepository.GetAsync(user => user.Email.Equals(userDetails.Email.ToLower()));
                if (checkEmail != null)
                {
                    result.Error = new List<ErrorItem>
                    {
                        new ErrorItem { Description = "" }
                    };
                    result.StatusCode = (int)HttpStatusCode.BadRequest;
                    return result;
                }

                var userModel = _mapper.Map<User>(userDetails);
                userModel.PasswordSalt = SaltHashAlgorithm.GenerateSalt();
                userModel.PasswordHash = SaltHashAlgorithm.GenerateHash(userDetails.Password, userModel.PasswordSalt);
                if(_unitOfWork.UserRepository.InsertAsync(userModel).IsCompletedSuccessfully)
                {
                    await _service.AddToRoleAsync(userModel, UserRoles.Customer.ToString());
                }               
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _unitOfWork.Save();
            }

            result.Status = true;
            result.Data = string.Empty;

            return result;
        }

        public async Task<bool> LoginAsync(LoginDTO details)
        {
            _user = await _unitOfWork.UserRepository.GetAsync(user => user.Email.Equals(details.Email.ToLower()));
            return _user != null & SaltHashAlgorithm.CompareHash(details.Password, _user.PasswordHash, _user.PasswordSalt);

        }
    }
}
