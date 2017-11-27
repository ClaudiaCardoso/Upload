using SENAI.FalaAiCidadao.Modelos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SENAI.FalaAiCidadao.Dominio.Servicos
{
    public class ImagemPostServico : ServicoBase<ImagemPost>
    {
        public bool ValidarImagem(HttpPostedFileBase file)
        {
            return (file.FileName.Contains("jpg") || file.FileName.Contains("jpeg") || file.FileName.Contains("png"));
        }
    }
}
