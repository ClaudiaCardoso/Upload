using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Modelos.Models
{
    [Table("Comentarios")]
    public class Comentario
    {
        public Comentario()
        {
            ComentarioId = Guid.NewGuid();
        }
        [Key]
        [Required]
        public Guid ComentarioId { get; set; }

        [StringLength(1000, MinimumLength = 5)]
        [Required]
        public string TextoComentario { get; set; }

        [Range(0,5)]
        public int NumeroAvaliacao { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }
        [Required]

        public bool Excluido { get; set; }
        
        [Required]
        public Guid PostagemId { get; set; }

        public virtual Postagem Postagem { get; set; }

        [Required]
        public Guid PoliticoId { get; set; }

        public virtual Politico Politico { get; set; }
    }
}
