using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Find.Models
{
    public class Pergunta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }
        [Required]
        public string Perguntas { get; set; }
        [Required]
        [ForeignKey("Aula")]
        public int idAula { get; set; }
        public Aula Aula { get; set; }
    }
}