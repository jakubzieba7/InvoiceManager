using InvoiceManager.App_Start;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomExceptionFilter());
        }
    }
}
