using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Find.Models
{
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O curso deve conter um nome")]
        public string nomeCurso { get; set; }
        
        [StringLength(255)]
        public string Requisitos { get; set; }
        [Required]
        public ICollection<CursoProfessor> CursoProfessor { get; set; }

        [Required]
        public ICollection<Aula> Aula { get; set; }
        public int Progresso { get; set; }
        public string Categoria { get; set; }
        public ICollection<Acesso> Acesso { get; set; }
    }
}