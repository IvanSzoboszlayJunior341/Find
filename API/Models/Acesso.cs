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
        [ForeignKey("idUsuario")]
        public int idUsuario { get; set; }
        public Usuario Usuario { get; set; }
        [ForeignKey("idCurso")]
        public int idCurso { get; set;}
        public Curso Curso { get; set; }
    }
}