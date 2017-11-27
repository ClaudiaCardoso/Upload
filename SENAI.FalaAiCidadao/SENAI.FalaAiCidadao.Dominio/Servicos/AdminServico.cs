using SENAI.FalaAiCidadao.Modelos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Dominio.Servicos
{
    public class AdminServico : ServicoBase<Admin>
    {
        public Admin Login(string email, string senha)
        {
            return GetAll().Where(a => a.Email == email && a.Senha == senha).FirstOrDefault();
        }
    }
}
