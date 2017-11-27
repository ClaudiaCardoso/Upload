using System.Web;
using System.Web.Mvc;

namespace SENAI.FalaAiCidadao.UI.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
