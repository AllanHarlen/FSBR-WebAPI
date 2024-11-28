using Domain.Services;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApis.Models;
using WebApis.Token;
using WebAPIs.Token;

namespace WebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Método para adicionar um novo usuário
        [AllowAnonymous]
        [HttpPost("AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] UsuarioRegister login)
        {
            if (string.IsNullOrWhiteSpace(login.Login) || string.IsNullOrWhiteSpace(login.Senha))
                return BadRequest("Falta alguns dados");

            var user = new Usuario
            {
                UserName = login.Login
            };

            var resultado = await _userManager.CreateAsync(user, login.Senha);
            if (resultado.Succeeded)
                return Ok(await _userManager.FindByNameAsync(login.Login));
            else            
                return BadRequest(resultado.Errors);
            
        }

        // Método para autenticar e gerar um token JWT
        [AllowAnonymous]
        [HttpPost("AutorizarUsuario")]
        public async Task<IActionResult> AutorizarUsuario([FromBody] UsuarioLogin login)
        {
            if (string.IsNullOrWhiteSpace(login.Login) || string.IsNullOrEmpty(login.Senha))
            {
                return Unauthorized();
            }

            var resultado = await _signInManager.PasswordSignInAsync(login.Login, login.Senha, false, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                var userCurrent = await _userManager.FindByNameAsync(login.Login);
                var userId = userCurrent.Id;
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("3doNYBdUvmRYB3W3zXsZuF9kHqk8E2UuZnOKcFueo0c=\r\n"))
                    .AddSubject("MeuRelatorio")
                    .AddIssuer("MeuRelatorio.Security")
                    .AddAudience("MeuRelatorio.Security")
                    .AddClaim("userId", userId)
                    .AddExpiry(5)
                    .Builder();

                var usuarioResponse = new UsuarioResponse
                {
                    Auth = token.value,
                    Usuario = new UsuarioInfo
                    {
                        Id = userId,
                        Login = userCurrent.UserName
                    }                   
                };

                return Ok(usuarioResponse);
            }
            else
            {
                return Unauthorized();
            }
        }
        
        // Método para obter um usuário pelo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterUsuario(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // Método para atualizar informações de um usuário
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarUsuario(string id, [FromBody] UsuarioRegister model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            user.UserName = model.Login;
            var resultado = await _userManager.UpdateAsync(user);

            if (resultado.Succeeded)
                return Ok("Usuário atualizado com sucesso");
            else
                return BadRequest(resultado.Errors);
        }

        // Método para deletar um usuário pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarUsuario(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var resultado = await _userManager.DeleteAsync(user);

            if (resultado.Succeeded)
                return Ok("Usuário deletado com sucesso");
            else
                return BadRequest(resultado.Errors);
        }

        // Método para listar todos os usuários
        [HttpGet("ListarUsuarios")]
        public IActionResult ListarUsuarios()
        {
            var usuarios = _userManager.Users.ToList();
            return Ok(usuarios);
        }

    }
}
