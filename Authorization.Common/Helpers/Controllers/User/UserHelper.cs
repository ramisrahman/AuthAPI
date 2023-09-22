using Authorization.Common.Database.Entities;
using Authorization.Common.Database.Persistence;
using Authorization.Common.Extensions;
using Authorization.Common.Models.Request;
using Authorization.Common.Models.Response;
using Authorization.Common.Utility;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Common.Helpers.Controllers.User
{
    public class UserHelper : IUserHelper
    {
        private readonly AppDbContext _context;

        public UserHelper(AppDbContext context)
        {
            _context = context;
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
                        PasswordHash = request.Password!.HashPassword(),
                        UpdatedAt = DateTime.UtcNow,
                        UserName = request.UserName,
                    };

                    await _context.Users.AddAsync(entity);
                    await _context.SaveChangesAsync();

                    var response = entity.ToResponse();
                    return response;
                }
                throw new Exception("Email address is required");
            }
            throw new Exception("User credentials are mandatory");
        }

        public async Task<string> UserLoginAsync(UserRequest request)
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
                                return "Login successful";
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
    }
}
