using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Modelos.Models
{
    [Table("Politicos")]
    public class Politico
    {
        public Politico()
        {
            PoliticoId = Guid.NewGuid();
            Permissao = "POLITICO";
        }

        [Key]
        public Guid PoliticoId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Nome { get; set; }

        [StringLength(11, MinimumLength = 11)]
        [Required]
        public string CPF { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Partido { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DataNascimento { get; set; }

        [StringLength(50, MinimumLength = 6)]
        [Required]
        public string Email { get; set; }

        [StringLength(50)] // ira receber uma senha criptografada
        [Required]
        public string Senha { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string Permissao { get; set; }

        [Required]
        [StringLength(150)]// ira receber o caminho da foto
        public string Foto { get; set; }

        public bool Ativo { get; set; }

        public virtual List<Comentario> Comentarios { get; set; }
    }
}
