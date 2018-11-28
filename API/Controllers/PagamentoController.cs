using System.Collections.Generic;
using System.Linq;
using Find.Data;
using Find.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Find.Controllers
{
    [Route("api/[controller]")]
    public class PagamentoController : Controller
    {
        Pagamento pag = new Pagamento();

        readonly APIContexto contexto;

        public PagamentoController(APIContexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet("{id}")]
        public IEnumerable<object> Listar(int id)
        {              
            
            var pagamentos = contexto.Usuario
                             .Join(contexto.Pagamento,
                             us => us.Id,
                             pgt => pgt.IdUsuario,
                             (us, pgt) => new 
                             {
                                Id = us.Id,
                                Nome = us.nomeUsuario,                                
                                DataPagamento = pgt.dataPagamento,
                                Valor = pgt.valorPagamento
                             }).Where(pg => pg.Id == id);

            var rs = pagamentos.ToList();
            
            return rs;
        }
    }
}