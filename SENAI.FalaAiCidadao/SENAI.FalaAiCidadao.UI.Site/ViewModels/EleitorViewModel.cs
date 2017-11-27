using SENAI.FalaAiCidadao.Modelos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.ViewModels
{
    public class EleitorViewModel
    {
        [Key]
        public Guid EleitorId { get; set; }

      //  [Required(ErrorMessage = "O nome deve ser preeenchido.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome deve conter entre 2 e 20 letras.")]
        [Display(Name = "Nome:")]
        public string Nome { get; set; }

       // [Required(ErrorMessage = "O sobrenome deve ser preeenchido.")]
        [StringLength(50, MinimumLength = 2)]
        public string Sobrenome { get; set; }

       // [Required(ErrorMessage = "O cpf deve ser preeenchido.")]
        [StringLength(11, MinimumLength = 11)]
        public string CPF { get; set; }

       // [Required(ErrorMessage = "A data de Nacimento deve ser preeenchida.")]
        [DisplayName("Data de Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataNascimento { get; set; }


      //  [Required(ErrorMessage = "Selecione uma foto.")]
        [ScaffoldColumn(false)]
        public HttpPostedFileBase Foto { get; set; }

        //[Required(ErrorMessage = "O nome deve ser preeenchido.")]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "A senha atual deve ser preeenchida.")]
        [StringLength(50)] //ira receber uma senha criptografada
        [DataType(DataType.Password)]
        [DisplayName("Senha Atual")]
        public string SenhaAntiga { get; set; }

        [StringLength(50)] //ira receber uma senha criptografada
        [DataType(DataType.Password)]
        public string SenhaNova { get; set; }
        
        [StringLength(50)] //ira receber uma senha criptografada
        [DataType(DataType.Password)]
        [Compare("SenhaNova")]
        [DisplayName("Confirmação de Senha")]
        public string SenhaConfima { get; set; }
        
        [StringLength(12, MinimumLength = 12)]
        [DisplayName("Título de Eleitor")]
        public string TituloEleitor { get; set; }
        
        public string Permissao { get; set; }
        //Endereco

        [StringLength(8, MinimumLength = 8)]
        [Required]
        public string Cep { get; set; }

        [StringLength(10, MinimumLength = 1)]
        [Required]
        public string Numero { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Complemento { get; set; }

    }
}