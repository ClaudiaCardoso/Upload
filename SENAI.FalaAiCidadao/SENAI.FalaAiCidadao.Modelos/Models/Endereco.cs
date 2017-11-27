using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Modelos.Models
{
    [Table("Enderecos")]
    public class Endereco
    {
        public Endereco()
        {
            EnderecoId = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid EnderecoId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Estado { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Cidade { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Bairro { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Logradouro { get; set; }

        [StringLength(5, MinimumLength = 1)]
        [Required]
        public string Numero { get; set; }

        [StringLength(8, MinimumLength = 1)]
        [Required]
        public string Cep { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Complemento { get; set; }

        [Required]
        public Guid EleitorId { get; set; }

        public virtual Eleitor Eleitor { get; set; }

    }
}
