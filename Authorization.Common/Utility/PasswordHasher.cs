using BCryptNet = BCrypt.Net.BCrypt;

namespace Authorization.Common.Utility
{
    public static class PasswordHasher
    {
        public static string HashPassword(this string password)
        {
            string salt = BCryptNet.GenerateSalt(12);
            string hashedPassword = BCryptNet.HashPassword(password, salt);

            return hashedPassword;
        }

        public static bool VerifyPassword(this string password, string hashedPassword)
        {
            return BCryptNet.Verify(password, hashedPassword);
        }
    }
}
