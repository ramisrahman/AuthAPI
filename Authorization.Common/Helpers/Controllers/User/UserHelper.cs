using Authorization.Common.Database.Entities;
using Authorization.Common.Database.Persistence;
using Authorization.Common.Models.Request;
using Microsoft.AspNetCore.Identity;
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

        public async Task RegisterUser(UserRequest request)
        {
            if(request != null)
            {
                var userExists = await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress == request.EmailAddress);
                if (userExists != null)
                {
                    throw new Exception("User already exists.");
                }

                var entity = new UserEntity
                {
                    CreatedAt = DateTime.UtcNow,
                    EmailAddress = request.EmailAddress,
                    PasswordHash = request.Password,
                    UpdatedAt = DateTime.UtcNow,
                    UserName = request.UserName,
                };

                await _context.Users.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
