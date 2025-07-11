﻿namespace IdentityService.Dtos
{
    public class RegisterDto
    {
        public string UserName { get; set; }  
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string? ProfilImage { get; set; }
    }
}
