﻿namespace ShopApi2024.DTOs.Account
{
    //login signin
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
