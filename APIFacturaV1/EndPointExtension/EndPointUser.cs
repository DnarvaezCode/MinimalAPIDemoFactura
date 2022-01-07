using APIFacturaV1.DTOs;
using APIFacturaV1.Models;
using APIFacturaV1.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIFacturaV1.EndPointExtension
{
    public static class EndPointUser
    {
        public static void AddEndPointUser(this WebApplication app)
        {
            app.MapPost("api/user/register", async (UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, UserDTO userDTO) =>
            {
                try
                {
                    var user = new IdentityUser { UserName = userDTO.Email, Email = userDTO.Email };

                    var result = await userManager.CreateAsync(user, userDTO.Password);
                    if (!result.Succeeded) return Results.BadRequest(result.Errors);

                    if (!await roleManager.RoleExistsAsync("Admin"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    if (!await roleManager.RoleExistsAsync("Customer"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Customer"));
                    }

                    await userManager.AddToRoleAsync(user, "Customer");

                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).WithName(EndPointNames.Register);

            app.MapPost("api/user/login", async (UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, UserDTO userDTO, IConfiguration configuration) =>
            {
                try
                {
                    var result = await signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, isPersistent: false, lockoutOnFailure: false);
                    if (!result.Succeeded) return Results.BadRequest(new Errors { ErrorMessage = "Nombre de usuario/contraseña no valido" });


                    var user = await userManager.FindByEmailAsync(userDTO.Email);
                    var roles = await userManager.GetRolesAsync(user);
                    var token = CreateToken(user, roles, configuration);
                    return Results.Ok(new UserTokenDTO
                    {
                        Token = token,
                        UserName = user.UserName,
                        Roles = roles
                    });
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).WithName(EndPointNames.Login);
        }

        private static string CreateToken(IdentityUser user, IList<string> roles, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var rolName in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rolName));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
