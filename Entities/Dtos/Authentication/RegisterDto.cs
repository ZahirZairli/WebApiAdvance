﻿namespace WebApiAdvance.Entities.Dtos.Authentication;

public class RegisterDto
{
    public string Username{ get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
