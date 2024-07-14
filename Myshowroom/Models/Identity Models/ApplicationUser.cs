using Microsoft.AspNetCore.Identity;
using Myshowroom.Migrations;

namespace Myshowroom.Models.NewFolder
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ResumeFilePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}



public class SmtpSettings
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
}

public class ForgotPassword
{
    public string Email { get; set; }
}

public class ResetPassword
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}