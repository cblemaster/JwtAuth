﻿namespace JwtAuth.Core.DataTransferObjects;

public class LoginUserDTO
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
