using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Find.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Você deve cadastrar o nome entre 2 até 100 caracteres")]
        public string nomeUsuario { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 11, ErrorMessage = "Você deve informar um cpf válido")]
        public string CPF { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Seu e-mail deve conter de 5 até 100 caracteres")]
        public string Email { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }

        public string Foto { get; set; }
        [Required]
        [ForeignKey("Pagamento")]
        public int idPagamento { get; set;}
        public ICollection<Pagamento> Pagamento { get; set; }

        [Required]
        public ICollection<Acesso> Acesso { get; set;}
        public ICollection<Pergunta> Pergunta { get; set; }
    }
}