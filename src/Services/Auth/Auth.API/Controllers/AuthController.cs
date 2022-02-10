using System.Security.Claims;
using Auth.API.Auth.ViewModels;
using Auth.Core.Models;
using Auth.Core.Services;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AuthenticationOptions = Auth.API.Auth.Configuration.AuthenticationOptions;

namespace Auth.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILoginService<POCUser> _loginService;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly ILogger<AuthController> _logger;
        private readonly AuthenticationOptions _authenticationOptions;

        public AuthController(ILoginService<POCUser> loginService, IIdentityServerInteractionService interactionService, ILogger<AuthController> logger, IOptions<AuthenticationOptions> authenticationOptions)
        {
            _loginService = loginService;
            _interactionService = interactionService;
            _logger = logger;
            _authenticationOptions = authenticationOptions.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var context = await _interactionService.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null)
            {
                throw new NotImplementedException("External login is not implemented!");
            }

            return View(new LoginViewModel(returnUrl));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _loginService.FindByUsernameAsync(viewModel.Username);
                if (user is not null && await _loginService.ValidateCredentialsAsync(user, viewModel.Password))
                {
                    var authenticationProperties = new AuthenticationProperties()
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(_authenticationOptions.TokenLifetime),
                        AllowRefresh = _authenticationOptions.AllowRefresh,
                        RedirectUri = viewModel.ReturnUrl,
                        IsPersistent = viewModel.RememberMe
                    };

                    var identityUser = new IdentityServerUser(user.Id)
                    {
                        DisplayName = user.Username,
                        AuthenticationTime = DateTime.UtcNow
                    };

                    await HttpContext.SignInAsync(identityUser, authenticationProperties);

                    if (_interactionService.IsValidReturnUrl(viewModel.ReturnUrl))
                        return Redirect(viewModel.ReturnUrl);
                    return Content("Success");
                }
                else
                    _logger.LogInformation($"User {viewModel.Username} failed to login");

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View(viewModel.NullifyPassword());
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await HttpContext.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            _logger.LogInformation($"Logout for id {logoutId}");

            if (string.IsNullOrWhiteSpace(logoutRequest.PostLogoutRedirectUri))
                return Content("Signed out");

            return SignOut(new AuthenticationProperties()
            {
                RedirectUri = logoutRequest.PostLogoutRedirectUri
            });
        }
    }
}
