using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Modelos.Models
{
    [Table("Tipos")]
    public class Tipo
    {
        public Tipo()
        {
            TipoId = Guid.NewGuid();
        }
        [Key]
        public Guid TipoId { get; set; }

        [Required]
        [StringLength(30)]
        public string Descicao { get; set; }
        public virtual List<Postagem> Postagem { get; set; }
    }
}
