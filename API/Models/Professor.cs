using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Find.Models
{
    public class Professor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Você deve inserir um nome de 2 até 100 caracteres")]
        public string nomeProfessor { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Você deve inserir um e-mail")]
        public string Email { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Você deve inserir uma senha")]
        public string Senha { get; set; }
        [Required]
        public ICollection<CursoProfessor> CursoProfessor { get; set; }
        public ICollection<Resposta> Resposta { get; set; }
    }
}