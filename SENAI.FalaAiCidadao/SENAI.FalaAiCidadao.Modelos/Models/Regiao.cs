using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Modelos.Models
{
    [Table("Regioes")]
    public class Regiao
    {
        public Regiao()
        {
            RegiaoId = Guid.NewGuid();
        }

        [Key]
        public Guid RegiaoId { get; set; }

        [Required]
        [StringLength(20)]
        public string Nome { get; set; }


        public virtual List<Postagem> Postagem { get; set; }

    }
}
