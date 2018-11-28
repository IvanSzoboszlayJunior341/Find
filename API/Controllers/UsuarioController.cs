using System.Collections.Generic;
using System.Linq;
using Find.Data;
using Find.Models;
using Microsoft.AspNetCore.Mvc;

namespace Find.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        Usuario user = new Usuario();
        readonly APIContexto contexto;

        public UsuarioController(APIContexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public IEnumerable<Usuario> Listar()
        {
            return contexto.Usuario.ToList();
        }

        [HttpGet("{id}")]
        public Usuario Listar(int id)
        {
            return contexto.Usuario.Where(us => us.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult Cadastro([FromBody] Usuario usuario)
        {
            if(!ModelState.IsValid)
                return BadRequest("Não foi possível enviar os dados para o cadastro, pois os dados não seguem o padrão de cadastro");
            
            contexto.Usuario.Add(usuario);
            int rs = contexto.SaveChanges();

            if(rs < 1)
                return BadRequest("Houve uma falha interna e não foi possível cadastrar");
            else
                return Ok(usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] Usuario usuario, int id)
        {
            if(!ModelState.IsValid)
                return BadRequest("Não foi possível atualizar os dados, pois eles não seguem o padrão de atualização!");
            
            var user = contexto.Usuario.Where(us => us.Id == id).FirstOrDefault();

            user.nomeUsuario = usuario.nomeUsuario;
            user.CPF = usuario.CPF;
            user.Foto = usuario.Foto;
            user.Email  = usuario.Email;
            user.Senha = usuario.Senha;
            contexto.Usuario.Update(user);
            int rs = contexto.SaveChanges();

            if(rs < 1)
                return BadRequest("Houve uma falha interna e não foi possível atualizar");
            else
                return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Apagar([FromBody] Usuario usuario, int id)
        {
            var user = contexto.Usuario.Where(us => us.Id == id).FirstOrDefault();
            if(user == null)
                return BadRequest("Usuário não existe!");

            contexto.Usuario.Remove(user);
            int rs = contexto.SaveChanges();

            if(rs > 0)
                return Ok();
            else
                return BadRequest();
        }
    }
}