using System.Web.Http;
using System.Web.Http.Controllers;

namespace TekAuth
{
    public abstract class FromHeaderAttribute : ParameterBindingAttribute
    {
        private string name;

        public FromHeaderAttribute(string headerName)
        {
            this.name = headerName;
        }

        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return new FromHeaderBinding(parameter, this.name);
        }
    }
}