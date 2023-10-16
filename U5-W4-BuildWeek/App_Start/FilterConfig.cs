using System.Web;
using System.Web.Mvc;

namespace U5_W4_BuildWeek
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
