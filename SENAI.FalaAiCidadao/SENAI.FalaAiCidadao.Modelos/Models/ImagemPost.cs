using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Modelos.Models
{
    [Table("ImagemPost")]
    public class ImagemPost
    {
        public ImagemPost()
        {
            ImagemPostId = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid ImagemPostId { get; set; }

        [Required]
        public Guid PostagemId { get; set; }

        [MaxLength(150)]
        [Required]
        public string NomeImagem { get; set; }

        public virtual Postagem Postagem { get; set; }
    }
}
