using SENAI.FalaAiCidadao.Dominio.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.Validacoes
{
    public class ValidarCPFCadastradoELeitor : ValidationAttribute
    {
        EleitorServico eleitorServico = new EleitorServico();
        public ValidarCPFCadastradoELeitor()
        {
            ErrorMessage = "CPF já cadastrado.";
        }
        public override bool IsValid(object value)
        {
            string cpf = value as string;
            return eleitorServico.VericicarCPFCadastrado(cpf);
        }
    }
}