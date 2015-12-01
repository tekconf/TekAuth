using System.Web.Http;
using System.Web.Http.Controllers;

namespace TekAuth.Controllers
{
    public class MyAuthAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext != null && actionContext.Request != null && actionContext.Request.Headers != null &&
                actionContext.Request.Headers.Authorization != null &&
                !string.IsNullOrWhiteSpace(actionContext.Request.Headers.Authorization.ToString()))
            {
                return base.IsAuthorized(actionContext);

            }
            else
            {
                return true;
            }
        }
    }
}