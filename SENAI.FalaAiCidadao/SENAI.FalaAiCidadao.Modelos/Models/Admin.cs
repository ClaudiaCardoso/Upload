using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.FalaAiCidadao.Modelos.Models
{
    [Table("Admins")]
    public class Admin
    {
        public Admin()
        {
            AdminId = Guid.NewGuid();
            Permissao = "ADMIN";
        }

        [Key]
        public Guid AdminId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)] // irá receber uma senha criptografada
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string Permissao { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }
    }
}
