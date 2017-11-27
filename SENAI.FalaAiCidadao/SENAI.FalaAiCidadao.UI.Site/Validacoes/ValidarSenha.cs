using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.Validacoes
{
    public class ValidarSenha : ValidationAttribute
    {
        public ValidarSenha()
        {
            ErrorMessage = "A senha deve conter 6 caracteres com letras e números";
        }

        public override bool IsValid(object value)
        {
            string senha = value as string;
            if (senha.Length != 6)
                return false;

            if ((senha.Where(c => char.IsLetter(c)).Count() > 0) && (senha.Where(c => char.IsNumber(c)).Count() > 0))
                return true;
            else return false;
        }
    }
}