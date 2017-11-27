using SENAI.FalaAiCidadao.Dominio.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.Validacoes
{
    public class ValidarEmailExistePolitico : ValidationAttribute
    {
        PoliticoServico politicoServico = new PoliticoServico();

        public ValidarEmailExistePolitico()
        {
            ErrorMessage = "Email já cadastrado.";
        }

        public override bool IsValid(object value)
        {
            string email = value as string;
            return politicoServico.VerificarEmailCadastrado(email);
        }
    }
}