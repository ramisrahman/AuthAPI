using Authorization.Common.Models.Response;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.Common.Services.Authentication
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly AuthenticationOptions _authOptions;

        public AuthenticationServices(IOptions<AuthenticationOptions> authOptions)
        {
            _authOptions = authOptions.Value;
        }

        public UserResponse GenerateTokens(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecretKey!));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_authOptions.ExpirationMinutes);
            var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_authOptions.ExpirationMinutes * 2);

            var accessToken = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                claims: claims,
                expires: accessTokenExpiration,
                signingCredentials: signInCredentials
            );

            var refreshToken = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                expires: refreshTokenExpiration,
                signingCredentials: signInCredentials
            );

            var jwtHandler = new JwtSecurityTokenHandler();

            return new UserResponse()
            {
                AccessToken = jwtHandler.WriteToken(accessToken),
                RefreshToken = jwtHandler.WriteToken(refreshToken)
            };
        }

    }
}
