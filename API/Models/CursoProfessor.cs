using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Find.Models
{
    public class CursoProfessor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Professor")]
        public int idProfessor { get; set; }
        [Required]
        [ForeignKey("Curso")]
        public int idCurso { get; set;}
    }
}