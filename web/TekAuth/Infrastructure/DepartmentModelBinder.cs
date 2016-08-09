namespace TekAuth.Infrastructure
{
    using System.Web.Mvc;
    using DAL;

    public class DepartmentModelBinder : IModelBinder
    {
        private readonly ConferenceContext _dbContext;

        public DepartmentModelBinder(ConferenceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var attemptedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;

            return string.IsNullOrWhiteSpace(attemptedValue) ? null : _dbContext.Departments.Find(int.Parse(attemptedValue));
        }
    }
}