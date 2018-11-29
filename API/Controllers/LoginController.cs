using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Find.Data;
using Find.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Find.Controllers
{
    /*
    O método Post receberá requisições HTTP do tipo POST, 
    tendo sido marcado com o atributo AllowAnonymous para 
    assim possibilitar o acesso de usuários não-autenticados;
    As instâncias dos tipos UsersDAO, SigningConfigurations e
    TokenConfigurations foram marcadas com o atributo FromServices no método Post,
    o que indica que as mesmas serão resolvidas via mecanismo de
    injeção de dependências do ASP.NET Core;
    O parâmetro usuario foi marcado com o atributo FromBody, correspondendo às
    credenciais (ID do usuário + chave de acesso) que serão enviadas no corpo 
    de uma requisição. As informações desta referência (usuario) serão então 
    comparadas com o retorno produzido pela instância do tipo UsersDAO, determinando 
    assim a validade do usuário e da chave de acesso em questão;
    Em se tratando de credenciais de um usuário existente claims serão geradas, 
    o período de expiração calculado e um token criado por meio de uma instância do
    tipo JwtSecurityTokenHandler (namespace System.IdentityModel.Tokens.Jwt). 
    Este último elemento é então transformado em uma string por meio do método 
    WriteToken e, finalmente, devolvido como retorno da Action Post (juntamente 
    com outras informações como horário de geração e expiração do token);
    Se o usuário for inválido um objeto então será devolvido, indicando que a autenticação falhou.
    */

    public class LoginController : Controller
    {
        readonly APIContexto _contexto;
        public LoginController(APIContexto contexto)
        {
            _contexto = contexto;
        }
        [Route("api/loginUsuario")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(
            [FromBody] Usuario usuario,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            Usuario user = _contexto.Usuario.FirstOrDefault(us => us.Email == usuario.Email && us.Senha == usuario.Senha);
            /*
            Se o user for diferente de null, isso significa que 
            o usuário foi encontrado e seguiremos com o processo de 
            autenticação e geração do token
            */

            if (user != null)
            {
                /*
                Abaixo estamos criando a identidade para o Token do usuário que
                autenticou com sucesso.
                O primeiro elemento é passar para string o id do usuário que vem como int e dizer 
                que está logado (aqui estamos usando o termo "Login").
                Segundo construir uma estrutura de apresentação dos dados o usuario, como NomeUsuario e Nivel como string.
                A forma de registro único do usuário no token foi identificada como user.Id incluimos NomeUsuario e a Nivel.            
                */
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Id.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim (JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                        new Claim("Nome", user.nomeUsuario),
                        new Claim(ClaimTypes.Email, user.Email)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                var retorno = new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "Ok"
                };

                return Ok(retorno);
            }
            else
            {
                var retorno = new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
                return BadRequest(retorno);
            }
        }

    [Route("api/loginProfessor")]
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Post(
    [FromBody] Professor professor,
    [FromServices] SigningConfigurations signingConfigurations,
    [FromServices] TokenConfigurations tokenConfigurations)
        {
            Professor prof = _contexto.Professor.FirstOrDefault(pro => pro.Email == professor.Email && pro.Senha == professor.Senha);
            /*
            Se o user for diferente de null, isso significa que 
            o usuário foi encontrado e seguiremos com o processo de 
            autenticação e geração do token
            */

            if (prof != null)
            {
                /*
                Abaixo estamos criando a identidade para o Token do usuário que
                autenticou com sucesso.
                O primeiro elemento é passar para string o id do usuário que vem como int e dizer 
                que está logado (aqui estamos usando o termo "Login").
                Segundo construir uma estrutura de apresentação dos dados o usuario, como NomeUsuario e Nivel como string.
                A forma de registro único do usuário no token foi identificada como user.Id incluimos NomeUsuario e a Nivel.            
                */
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(prof.Id.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim (JwtRegisteredClaimNames.UniqueName, prof.Id.ToString()),
                        new Claim("Nome", prof.nomeProfessor),
                        new Claim(ClaimTypes.Email, prof.Email)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                var retorno = new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "Ok"
                };
                return Ok(retorno);
            }
            else
            {
                var retorno = new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
                return BadRequest(retorno);
            }
        }
    }
}
