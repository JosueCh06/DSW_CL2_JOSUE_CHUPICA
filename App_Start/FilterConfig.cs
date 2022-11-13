using System.Web;
using System.Web.Mvc;

namespace DSW_CL2_JOSUE_CHUPICA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
