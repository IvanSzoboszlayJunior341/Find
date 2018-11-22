using System.Linq;
using Find.Models;

namespace Find.Data
{
    public class InicializarBanco
    {
        public static void Iniciar(APIContexto contexto)
        {
            contexto.Database.EnsureCreated();
            if(contexto.Usuario.Any())
            {
                return;
            }
            var usuario = new Usuario()
            {
                nomeUsuario = "Paulo Ricardo", 
                CPF = "111.222.333-45", 
                Email = "paulo@find.com.br",
                Senha = "123"
            };
            contexto.Usuario.Add(usuario);

            var professor = new Professor()
            {
                nomeProfessor = "Ivan Szobozslay",
                Email = "ivan@find.com.br",
                Senha = "1234"
            };
            contexto.Professor.Add(professor);
            contexto.SaveChanges();
        }
    }
}