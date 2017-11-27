using SENAI.FalaAiCidadao.Modelos.Models;
using SENAI.FalaAiCidadao.Util.Correio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Util
{
    public class ValidarCep
    {
        //Site Referencia: http://www.eduardorizo.com.br/2014/12/04/correios-webservice-para-consulta-de-enderecos-a-partir-de-um-cep/
        public static Endereco buscarEndereco(string cep)
        {
            try
            {
                Endereco endereco = new Endereco();
                endereco.Cep = cep;
                AtendeClienteClient ws = new AtendeClienteClient("AtendeClientePort"); //Verificar o nome do endpoint no arquivo Web.config
                var dados = ws.consultaCEP(cep);
                if (dados != null)
                {
                    endereco.Logradouro = dados.end;
                    endereco.Bairro = dados.bairro;
                    endereco.Cidade = dados.cidade;
                    endereco.Estado = dados.uf;
                    return endereco;
                }
                else
                    return null;
            }
            catch(System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
    }
}
