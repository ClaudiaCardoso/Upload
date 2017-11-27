using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.Validacoes
{
    public class ValidarTitulo : ValidationAttribute
    {
        public ValidarTitulo()
        {
            ErrorMessage = "Título de eleitor inválido.";
        }

        public override bool IsValid(object value)
        {
            string titulo = value as string;

            int dig1; int dig2; int dig3; int dig4; int dig5; int dig6;
            int dig7; int dig8; int dig9; int dig10; int dig11;
            int dig12; int dv1; int dv2; int qDig;

            if (titulo.Length == 0) //Validação do preenchimento
            {
                return false; //Caso não seja informado o Título
            }
            else
            {
                if (titulo.Length < 12)
                {
                    //Completar 12 dígitos             
                    titulo = "000000000000" + titulo;
                    titulo = titulo.Substring(titulo.Length - 12);
                }
                else if (titulo.Length > 12)
                {
                    return false; //Caso tenha mais que 12 dígitos
                }
            }

            qDig = titulo.Length; //Total de caracteres

            //Gravar posição dos caracteres
            dig1 = int.Parse(titulo.Substring(qDig - 12, 1));
            dig2 = int.Parse(titulo.Substring(qDig - 11, 1));
            dig3 = int.Parse(titulo.Substring(qDig - 10, 1));
            dig4 = int.Parse(titulo.Substring(qDig - 9, 1));
            dig5 = int.Parse(titulo.Substring(qDig - 8, 1));
            dig6 = int.Parse(titulo.Substring(qDig - 7, 1));
            dig7 = int.Parse(titulo.Substring(qDig - 6, 1));
            dig8 = int.Parse(titulo.Substring(qDig - 5, 1));
            dig9 = int.Parse(titulo.Substring(qDig - 4, 1));
            dig10 = int.Parse(titulo.Substring(qDig - 3, 1));
            dig11 = int.Parse(titulo.Substring(qDig - 2, 1));
            dig12 = int.Parse(titulo.Substring(qDig - 1, 1));

            //Cálculo para o primeiro dígito validador
            dv1 = (dig1 * 2) + (dig2 * 3) + (dig3 * 4) + (dig4 * 5) + (dig5 * 6) +
                    (dig6 * 7) + (dig7 * 8) + (dig8 * 9);
            dv1 = dv1 % 11;

            if (dv1 == 10)
            {
                dv1 = 0; //Se o resto for igual a 10, dv1 igual a zero
            }

            //Cálculo para o segundo dígito validador
            dv2 = (dig9 * 7) + (dig10 * 8) + (dv1 * 9);
            dv2 = dv2 % 11;

            if (dv2 == 10)
            {
                dv2 = 0; //Se o resto for igual a 10, dv1 igual a zero
            }

            //Validação dos dígitos validadores, após o cálculo realizado
            return (dig11 == dv1 && dig12 == dv2);
        }
    }
}