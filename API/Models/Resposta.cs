using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Find.Models
{
    public class Resposta
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Respostas { get; set;}
        [ForeignKey("idPergunta")]
        public int idPergunta { get; set; }
        public Pergunta Pergunta { get; set; }

        [ForeignKey("idUsuario")]
        public int idUsuario { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("idProfessor")]
        public int idProfessor { get; set; }
        public Professor Professor { get; set; }
    }
}