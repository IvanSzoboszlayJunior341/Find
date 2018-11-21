using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Find.Models
{
    public class Pagamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime dataPagamento { get; set;}
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Você deve informar o status do pagamento")]
        public string statusPagamento { get; set; }        
        [Required]
        public string codTransacao { get; set; }
        [Required]
        public double valorPagamento { get; set; }
        // Vamos criar um campo para guardar a opção de pagamento do cliente, sendo elas PayPal ou PagSeguro
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Você deve selecionar uma forma de pagamento!")]
        public string formaPagamento { get; set; }
        [ForeignKey("IdUsuario")]
        public int IdUsuario{get;set;}
        [Required]
        public Usuario Usuario { get; set; }
    }
}