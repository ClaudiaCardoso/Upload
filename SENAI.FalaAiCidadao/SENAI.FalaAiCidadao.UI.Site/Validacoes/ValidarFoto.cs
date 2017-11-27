using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SENAI.FalaAiCidadao.UI.Site.Validacoes
{
    public class ValidarFoto : ValidationAttribute
    {
        public ValidarFoto()
        {
            ErrorMessage = "A foto deve conter ate 100 kb e dos tipos .jpg, .jpeg ou .png";
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                HttpPostedFileBase file = value as HttpPostedFileBase;
                String[] strName = file.FileName.Split('.');
                String strExt = strName[strName.Count() - 1];
                if (!string.IsNullOrEmpty(strExt) && strExt != "jpg" && strExt != "jpeg" && strExt != "png") // verifica se n eh nulo, ou de tipo que não é imagem
                    return false;
                return file.ContentLength < 100000; // verifica se é maior que 100kb
            }
            return false;
        }
    }
}