using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.ViewModels
{
    public class PoliticoViewModel
    {
        public Guid PoliticoId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]

        public string Nome { get; set; }

        [StringLength(11, MinimumLength = 11)]
        [Required]
        public string CPF { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Partido { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataNascimento { get; set; }

        [StringLength(50, MinimumLength = 6)]
        [Required]
        public string Email { get; set; }

        [StringLength(6, MinimumLength = 6)] 
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare("Senha")]
        public string SenhaConfirm { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public HttpPostedFileBase Foto { get; set; }
    }
}