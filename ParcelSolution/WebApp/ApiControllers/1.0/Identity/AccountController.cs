using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Identity;


namespace WebApp.ApiControllers._1._0.Identity
{
    /// <summary>
    /// Api endpoint for registering new user and user log-in (jwt token generation)
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private IList<AuthenticationScheme>? ExternalLogins { get; set; }
        private readonly IEmailSender _emailSender;
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userManager"></param>
        /// <param name="logger"></param>
        /// <param name="signInManager"></param>
        /// <param name="emailSender"></param>
        /// <param name="bll">bll</param>
        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
            ILogger<AccountController> logger, SignInManager<AppUser> signInManager, IEmailSender emailSender, IAppBLL bll)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _bll = bll;
        }

        /// <summary>
        /// Endpoint for user log-in (jwt generation)
        /// </summary>
        /// <param name="model">login data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {model.Email} not found!");
                return StatusCode(403);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); //get the User analog
                var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"Token generated for user {model.Email}");
                return Ok(new {token = jwt, status = "Logged in", email = model.Email});
            }

            _logger.LogInformation($"Web-Api login. User {model.Email} attempted to log-in with bad password!");
            return StatusCode(403);
        }


        /// <summary>
        /// Endpoint for user registration and immediate log-in (jwt generation) 
        /// </summary>
        /// <param name="model">user data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var appUser = new AppUser
                {
                    UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName,
                    Address = model.Address
                };
                var result = await _userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("New user created.");

                    // create claims based user 
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);

                    // get the Json Web Token
                    var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                        _configuration["JWT:SigningKey"],
                        _configuration["JWT:Issuer"],
                        _configuration.GetValue<int>("JWT:ExpirationInDays")
                    );

                   await _bll.SaveChangesAsync();
                    
                    _logger.LogInformation("Token generated for user");
                    return Ok(new {token = jwt, status = "Account created for " + model.Email, email = model.Email});
                }

                return StatusCode(406); //406 Not Acceptable
            }

            return StatusCode(400); //400 Bad Request
        }
        
        /// <summary>
        /// Change the password.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            Console.WriteLine(user.ToString());
            if (user == null)
            {
                _logger.LogInformation("User not found!");
                return StatusCode(403);
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return StatusCode(406); //406 Not Acceptable
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user); //get the User analog
            var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                _configuration["JWT:SigningKey"],
                _configuration["JWT:Issuer"],
                _configuration.GetValue<int>("JWT:ExpirationInDays")
            );
            _logger.LogInformation("Token generated for user");
            return Ok(new {token = jwt, status = "Password changed", email = model.Email});
        }
        
        /// <summary>
        /// Change the password.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> ChangeEmail([FromBody] ChangeEmailDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            Console.WriteLine(user.ToString());
            if (user == null)
            {
                _logger.LogInformation("User not found!");
                return StatusCode(403);
            }
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user); //get the User analog
            var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                _configuration["JWT:SigningKey"],
                _configuration["JWT:Issuer"],
                _configuration.GetValue<int>("JWT:ExpirationInDays")
            );
            
            var email = await _userManager.GetEmailAsync(user);
            if (model.NewEmail != email)
            {
                
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
                await _userManager.ChangeEmailAsync(user, model.NewEmail, code);
                await _userManager.SetUserNameAsync(user, model.NewEmail);
                
                _logger.LogInformation("Token generated for user");
                return Ok(new {token = jwt, status = "Email changed", email = model.NewEmail});
            }

            return Ok(new {token = jwt, status = "Email was not changed", email = model.Email});
        }
    }
    
    

    /// <summary>
    /// DTO for login validation
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = default!;
    }

    /// <summary>
    /// DTO for register validation
    /// </summary>
    public class RegisterDTO
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = default!;
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; } = default!;
        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; } = default!;
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; } = default!;
    }
    
    /// <summary>
    /// DTO for changing password
    /// </summary>
    public class ChangePasswordDTO
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// Old password
        /// </summary>
        public string OldPassword { get; set; } = default!;
        /// <summary>
        /// Password that we want to change to
        /// </summary>
        public string NewPassword { get; set; } = default!;
    }
    
    /// <summary>
    /// DTO for changing email
    /// </summary>
    public class ChangeEmailDTO
    {
        /// <summary>
        /// Old email
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// New email
        /// </summary>
        public string NewEmail { get; set; } = default!;
    }
}