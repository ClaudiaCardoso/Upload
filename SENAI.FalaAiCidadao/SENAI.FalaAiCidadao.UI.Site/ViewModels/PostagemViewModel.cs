using SENAI.FalaAiCidadao.Modelos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.ViewModels
{
    public class PostagemViewModel
    {
        public Guid PostagemId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string TituloPost { get; set; }

        [StringLength(1000, MinimumLength = 3)]
        [Required]
        public string TextoPost { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [ScaffoldColumn(false)]
        public Guid EleitorId { get; set; }

        [ScaffoldColumn(false)]
        public Guid RegiaoId { get; set; }

        [ScaffoldColumn(false)]
        public Guid TipoId { get; set; }

        [ScaffoldColumn(false)]
        public List<Regiao> Regioes { get; set; }

        [ScaffoldColumn(false)]
        public List<Tipo> Tipos { get; set; }

        [ScaffoldColumn(false)]
        public List<HttpPostedFileBase> Fotos { get; set; }

        [StringLength(1000, MinimumLength = 5)]
        public string Comentario { get; set; }
    }
}