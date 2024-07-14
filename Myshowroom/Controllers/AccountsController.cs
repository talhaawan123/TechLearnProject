
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Myshowroom.Models.NewFolder;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TechLearn.Business_logic.Concrete;

namespace Myshowroom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Signup model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                { 
                    UserName = model.Email,
                    Email = model.Email, 
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedAt = DateTime.UtcNow };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new { message = "Registration successful" });
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("registrations-last-week")]
        public async Task<IActionResult> GetRegistrationsLastWeek()
        {
            var lastWeek = DateTime.UtcNow.AddDays(-7);
            var recentRegistrations = await _userManager.Users
                .Where(u => u.CreatedAt >= lastWeek)
                .GroupBy(u => u.CreatedAt.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return Ok(recentRegistrations);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        return BadRequest("User not found.");
                    }

                    var token = GenerateJwtToken(user);
                    return Ok(new { token });
                }
                return BadRequest("Invalid login attempt.");
            }
            return BadRequest(ModelState);
        }
       

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }


        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var frontendBaseUrl = _configuration["Frontend:BaseUrl"];
                var resetPasswordLink = $"{frontendBaseUrl}/reset-password?token={WebUtility.UrlEncode(token)}&email={WebUtility.UrlEncode(user.Email)}";

                // Compose email HTML content
                string emailSubject = "Reset Your Password";
                string emailContent = $@"
            <html>
            <head>
                <style>
                    /* Add your CSS styles here */
                    body {{
                        font-family: Arial, sans-serif;
                        line-height: 1.6;
                        background-color: #f4f4f4;
                        padding: 20px;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        background: #fff;
                        padding: 30px;
                        border-radius: 8px;
                        box-shadow: 0 0 10px rgba(0,0,0,0.1);
                    }}
                    h2 {{
                        color: #4caf50;
                        margin-bottom: 20px;
                    }}
                    p {{
                        margin-bottom: 15px;
                    }}
                    .button {{
                        display: inline-block;
                        background-color: #4caf50;
                        color: white;
                        text-decoration: none;
                        padding: 10px 20px;
                        border-radius: 5px;
                        margin-top: 15px;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>{emailSubject}</h2>
                    <p>Hello {user.FirstName} {user.LastName},</p>
                    <p>We received a request to reset your password. Please click the link below to reset it:</p>
                    <p><a class='button' href='{resetPasswordLink}'>Reset Password</a></p>
                    <p>If you did not request a password reset, please ignore this email.</p>
                    <p>Thank you,</p>
                    <p>Your Tech-Learn Team</p>
                </div>
            </body>
            </html>
        ";

                await _emailSender.SendEmailAsync(model.Email, emailSubject, emailContent);
                return Ok(new { message = "Password reset email sent." });
            }
            return BadRequest(ModelState);
        }



        [HttpGet ("reset-password")]
        public async Task<IActionResult> ResetPassword(string token , string email)
        {
          var model = new ResetPassword { Token = token , Email =email };
            return Ok(new { model });

        }


        [AllowAnonymous]
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword( ResetPassword resetPassword)
        {
          var user = await _userManager.FindByEmailAsync (resetPassword.Email);
          var resetpassword = await _userManager.ResetPasswordAsync(user, resetPassword.Token , resetPassword.Password);
            return Ok(new { message = "completed" });
            
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users.Select(u => new
            {

                FirstName= u.FirstName,
                LastName=u.LastName,
                Email = u.Email,
                Resume = u.ResumeFilePath,
            }).ToList();

            return Ok(users);
        }
        [HttpGet("totalUsers")]
        public async Task<IActionResult> GetTotalUsersCount()
        {
            var totalUsersCount = await _userManager.Users.CountAsync();
            return Ok(new { totalUsersCount });
        }
        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userProfile = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                ResumeFilePath = user.ResumeFilePath
            };

            return Ok(userProfile);
        }

        [HttpGet("download-resume")]
        [Authorize]
        public async Task<IActionResult> DownloadResume()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || string.IsNullOrEmpty(user.ResumeFilePath))
            {
                return NotFound("Resume not found.");
            }

            var filePath = user.ResumeFilePath;
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, contentType, Path.GetFileName(filePath));
        }

        [HttpPost("upload-resume")]
        [Authorize]
        public async Task<IActionResult> UploadResume(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, $"{user.Id}_{Path.GetFileName(file.FileName)}");

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            user.ResumeFilePath = filePath;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Resume uploaded successfully." });
        }

    }
}
