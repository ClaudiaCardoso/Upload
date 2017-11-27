using SENAI.FalaAiCidadao.Modelos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Dominio.Servicos
{
    public class PoliticoServico : ServicoBase<Politico>
    {
        public bool VerificarEmailCadastrado(string email)
        {
            return GetAll().Where(p => p.Email.Equals(email)).FirstOrDefault() != null;
        }
        public bool VerificarCPFCadastrado(string cpf)
        {
            return GetAll().Where(p => p.CPF.Equals(cpf)).FirstOrDefault() != null;
        }

        public Politico Login(string email, string senha)
        {
            return GetAll().Where(p => p.Email == email && p.Senha == senha).FirstOrDefault();
        }
    }
}
