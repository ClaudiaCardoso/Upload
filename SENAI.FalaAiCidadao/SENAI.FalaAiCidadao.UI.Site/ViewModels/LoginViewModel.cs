using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Insira seu E-mail,")]
        [Display(Name = "E-mail:")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Insira sua senha.")]
        [Display(Name = "Senha:")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}