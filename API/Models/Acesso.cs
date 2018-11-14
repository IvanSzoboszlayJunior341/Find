using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Find.Models
{
    public class Acesso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Usuario")]
        public int idUser { get; set; }
        public Usuario Usuario { get; set; }
        [Required]
        [ForeignKey("Curso")]
        public int idCurso { get; set;}
        public ICollection<Curso> Curso { get; set; }
    }
}