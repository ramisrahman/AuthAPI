﻿
namespace Authorization.Common.Models.Request
{
    public class LoginRequest
    {
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
    }
}
