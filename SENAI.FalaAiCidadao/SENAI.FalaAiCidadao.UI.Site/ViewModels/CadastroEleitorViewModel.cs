using SENAI.FalaAiCidadao.UI.Site.Validacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.ViewModels
{
    public class CadastroEleitorViewModel
    {
        //Eleitor
        public HttpPostedFileBase Foto { get; set; }

        [Required]
        [Display(Name = "Nome Eleitor:")]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Sobrenome Eleitor:")]
        [StringLength(50, MinimumLength = 2)]
        public string Sobrenome { get; set; }

        [Required]
        [Display(Name = "CPF Eleitor:")]
        [StringLength(11, MinimumLength = 11)]
        public string CPF { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento Eleitor:")]
        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Display(Name = "Email do Eleitor:")]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Senha do Eleitor:")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Senha:")]
        [Compare("Senha", ErrorMessage = "As senhas digitadas não são compativeis. ")]
        public string SenhaConfirm { get; set; }

        [StringLength(12, MinimumLength = 12)]
        [Display(Name = "Titulo de Eleitor:")]
        [Required]
        public string TituloEleitor { get; set; }

        //Endereco

        [StringLength(8, MinimumLength = 8)]
        [Required]
        [Display(Name = "CEP Eleitor:")]
        public string Cep { get; set; }

        [StringLength(10, MinimumLength = 1)]
        [Display(Name = "Numero:")]
        [Required]
        public string Numero { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Complemento:")]
        [Required]
        public string Complemento { get; set; }
    }
}