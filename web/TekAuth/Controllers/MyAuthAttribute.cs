using System.Web.Http;
using System.Web.Http.Controllers;

namespace TekAuth.Controllers
{
    public enum AuthenticationRequirement
    {
        AllowAnoymous,
        RequireAuthentication
    }
    public class MyAuthAttribute : AuthorizeAttribute
    {
        private readonly AuthenticationRequirement _requirement;

        public MyAuthAttribute(AuthenticationRequirement requirement)
        {
            _requirement = requirement;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext != null && actionContext.Request != null 
                && actionContext.Request.Headers != null 
                && actionContext.Request.Headers.Authorization != null 
                && !string.IsNullOrWhiteSpace(actionContext.Request.Headers.Authorization.ToString()))
            {
                // Passed an auth header
                return base.IsAuthorized(actionContext);

            }
            else
            {
                // No auth header
                return _requirement != AuthenticationRequirement.RequireAuthentication;
            }
        }
    }
}