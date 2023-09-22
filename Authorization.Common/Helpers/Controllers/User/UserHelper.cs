using Authorization.Common.Database.Entities;
using Authorization.Common.Database.Persistence;
using Authorization.Common.Extensions;
using Authorization.Common.Models.Request;
using Authorization.Common.Models.Response;
using Authorization.Common.Services.Authentication;
using Authorization.Common.Utility;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Authorization.Common.Helpers.Controllers.User
{
    public class UserHelper : IUserHelper
    {
        private readonly AppDbContext _context;
        private readonly IAuthenticationServices _authenticationServices;

        public UserHelper(AppDbContext context,
            IAuthenticationServices authenticationServices)
        {
            _context = context;
            _authenticationServices = authenticationServices;
        }

        public async Task<UserResponse> RegisterUserAsync(UserRequest request)
        {
            if(request != null)
            {
                if (!string.IsNullOrEmpty(request.EmailAddress))
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress == request.EmailAddress);
                    if (user != null)
                    {
                        throw new Exception("User already exists.");
                    }

                    var entity = new UserEntity
                    {
                        CreatedAt = DateTime.UtcNow,
                        EmailAddress = request.EmailAddress,
                        Id = Guid.NewGuid(),
                        PasswordHash = request.Password!.HashPassword(),
                        UpdatedAt = DateTime.UtcNow,
                        UserName = request.UserName,
                    };

                    var response = entity.ToResponse();
                    var tokenResponse = GenerateTokens(Convert.ToString(entity.Id), entity.EmailAddress);

                    response.AccessToken = tokenResponse.AccessToken;
                    response.RefreshToken = tokenResponse.RefreshToken;

                    await _context.Users.AddAsync(entity);
                    await _context.SaveChangesAsync();

                    return response;
                }
                throw new Exception("Email address is required");
            }
            throw new Exception("User credentials are mandatory");
        }

        public async Task<LoginResponse> UserLoginAsync(LoginRequest request)
        {
            if(request != null)
            {
                if(!string.IsNullOrEmpty(request.EmailAddress))
                {
                    if (!string.IsNullOrEmpty(request.Password))
                    {
                        var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress == request.EmailAddress);
                        if(userEntity != null)
                        {
                            if (request.Password.VerifyPassword(userEntity.PasswordHash!))
                            {
                                return new LoginResponse
                                {
                                    EmailAddress = userEntity.EmailAddress,
                                    Message = "Login Success",
                                    Status = "Success",
                                    UserId = userEntity.Id
                                };
                            }
                            throw new Exception("Email address and password does not matches");
                        }
                        throw new Exception("User does not exists");
                    }
                    throw new Exception("Password is required");
                }
                throw new Exception("Email address is required");
            }
            throw new Exception("User credentials are mandatory");
        }

        private UserResponse GenerateTokens(string? userId, string emailAddress)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId!),
                new Claim(ClaimTypes.Name, emailAddress),
                new Claim(ClaimTypes.Role, "user"),
            };

            return _authenticationServices.GenerateTokens(claims);
        }

    }
}
