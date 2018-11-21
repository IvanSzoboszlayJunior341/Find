using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Find.Models
{
    public class Pergunta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("idUsuario")]
        public int idUsuario { get; set; }
        public Usuario Usuario { get; set; }
        [ForeignKey("idAula")]
        public int idAula { get; set; }
        public Aula Aula { get; set; }
        public ICollection<Resposta> Resposta { get; set; }
    }
}