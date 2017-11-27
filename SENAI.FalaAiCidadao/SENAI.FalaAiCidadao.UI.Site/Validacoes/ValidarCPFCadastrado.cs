using SENAI.FalaAiCidadao.Dominio.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.Validacoes
{
    public class ValidarCPFCadastradoPolitico : ValidationAttribute
    {
        PoliticoServico politicoServico = new PoliticoServico();
        public ValidarCPFCadastradoPolitico()
        {
            ErrorMessage = "CPF já cadastrado.";
        }
        public override bool IsValid(object value)
        {
            string cpf = value as string;
            return politicoServico.VerificarCPFCadastrado(cpf);
        }
    }
}