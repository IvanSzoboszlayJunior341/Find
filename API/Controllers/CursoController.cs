using System.Collections.Generic;
using System.Linq;
using Find.Data;
using Find.Models;
using Microsoft.AspNetCore.Mvc;

namespace Find.Controllers
{
    [Route("api/[controller]")]
    public class CursoController : Controller
    {
        Curso cur = new Curso();

        readonly APIContexto contexto;

        public CursoController(APIContexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public IEnumerable<Curso> Listar()
        {
            return contexto.Curso.ToList();
        }

        [HttpGet("{id}")]
        public Curso Listar(int id)
        {
            return contexto.Curso.Where(cur => cur.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult Cadastro([FromBody] Curso curso)
        {
            if(!ModelState.IsValid)
                return BadRequest("Não foi possível enviar os dados para o cadastro, pois os dados não seguem o padrão de cadastro");

            contexto.Curso.Add(curso);
            int rs = contexto.SaveChanges();

            if(rs < 1)
                return BadRequest("Houve uma falha interna e não foi possível cadastrar");
            else
                return Ok(curso);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] Curso curso, int id)
        {
            if(!ModelState.IsValid)
                return BadRequest("Não foi possível atualizar os dados, pois eles não seguem o padrão de atualização!");
            
            var cur = contexto.Curso.Where(cr => cr.Id == id).FirstOrDefault();
            
            cur.nomeCurso = curso.nomeCurso;
            cur.Requisitos = curso.Requisitos;
            cur.Progresso = curso.Progresso;
            cur.Categoria = curso.Categoria;
            cur.CursoProfessor = curso.CursoProfessor;
            contexto.Curso.Update(cur);
            int rs = contexto.SaveChanges();

            if(rs < 1)
                return BadRequest("Houve uma falha interna e não foi possível atualizar!");
            else
                return Ok(curso);
        }

        
    }
}