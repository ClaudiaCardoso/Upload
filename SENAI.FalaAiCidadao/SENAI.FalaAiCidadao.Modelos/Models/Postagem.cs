using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SENAI.FalaAiCidadao.Modelos.Models
{
    [Table("Postagens")]
    public class Postagem
    {
        public Postagem()
        {
            PostagemId = Guid.NewGuid();
        }

        [Key]
        public Guid PostagemId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string TituloPost { get; set; }

        [StringLength(1000, MinimumLength = 5)]
        [Required]
        public string TextoPost { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Required]
        public Guid EleitorId { get; set; }

        [Required]
        public Guid RegiaoId { get; set; }

        [Required]
        public Guid TipoId { get; set; }
        
        [Required]
        public bool Excluido { get; set; }

        public int NumAvaliacao { get; set; }

        public virtual string Comentario { get; set; }

        public virtual Eleitor Eleitor { get; set; }

        public virtual Tipo Tipo { get; set; }

        public virtual Regiao Regiao { get; set; }

        public virtual List<ImagemPost> Imagens { get; set; }

        public virtual List<Comentario> Comentaios { get; set; }
    }
}
