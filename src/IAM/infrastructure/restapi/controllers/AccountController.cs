using IAMService.domain;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IAMService.infrastructure.restapi.controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            // Manejar error de OAuth
            return RedirectToAction(nameof(Login));
        }

        // Verificar si el usuario ya existe con este login externo
        var user = await _userRepository.GetByExternalLoginAsync(info.LoginProvider, info.ProviderKey);
        if (user == null)
        {
            // Crear un nuevo usuario en el dominio si no existe
            var newUser = new User
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                IsExternalUser = true,
                ProviderName = info.LoginProvider
            };
            await _userRepository.CreateExternalUserAsync(newUser, info.LoginProvider, info.ProviderKey);
            user = newUser;
        }

        // Autenticar al usuario localmente
        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToLocal(returnUrl);
    }
}