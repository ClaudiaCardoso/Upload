using SENAI.FalaAiCidadao.Dominio.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.Validacoes
{
    public class ValidarEmailJaExisteEleitor : ValidationAttribute
    {
        EleitorServico eleitorServico = new EleitorServico();

        public ValidarEmailJaExisteEleitor()
        {
            ErrorMessage = "Email já cadastrado.";
        }

        public override bool IsValid(object value)
        {
            string email = value as string;
            return eleitorServico.VerificarEmailExiste(email);
        }
    }
}