using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Modelos.Models
{
    [Table("Eleitores")]
    public class Eleitor
    {
        public Eleitor()
        {
            EleitorId = Guid.NewGuid();
            Permissao = "ELEITOR";
        }

        [Key]
        public Guid EleitorId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Sobrenome { get; set; }

        [StringLength(11, MinimumLength = 11)]
        [Required]
        public string CPF { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)] //ira receber uma senha criptografada
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [StringLength(12, MinimumLength = 12)]
        [Required]
        public string TituloEleitor { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string Permissao { get; set; }

        [Required]
        [StringLength(150)]// ira receber o caminho da foto
        public string Foto { get; set; }

        public bool Ativo { get; set; }

        public bool Excluido { get; set; }

        public virtual List<Endereco> Endereco { get; set; }

        public virtual List<Postagem> Postagens { get; set; }
    }
}
