using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaginaRecetas.App_Start
{
    public class SessionTimeOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = HttpContext.Current;
            if (context.Session != null)
            {
                if (context.Session["Usuario"] == null)                
                    context.Response.Redirect("~/Recetas/Index");                
            }
            base.OnActionExecuting(filterContext);
        }
    }
}