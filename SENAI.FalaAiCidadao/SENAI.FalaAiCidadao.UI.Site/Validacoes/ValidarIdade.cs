using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.Validacoes
{
    public class ValidarIdade : ValidationAttribute
    {
        public ValidarIdade()
        {
            ErrorMessage = "O eleitor ser maior de 18 anos.";
        }

        public override bool IsValid(object value)
        {
            DateTime data = Convert.ToDateTime(value);
            int idade;
            int anoAtual = DateTime.Now.Year;
            int mesAtual = DateTime.Now.Month;
            int anoNiver = data.Year;
            int mesNiver = data.Month;
            int difAno = anoAtual - anoNiver;
            int difMes = mesAtual - mesNiver;
            if (difMes < 0)
                idade = difAno - 1;
            else
                idade = difAno;

            return idade > 18;
        }
    }
}