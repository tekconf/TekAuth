namespace TekAuth.Infrastructure
{
    using System.Web.Mvc;
    using DAL;

    public class InstructorModelBinder : IModelBinder
    {
        private readonly ConferenceContext _dbContext;

        public InstructorModelBinder(ConferenceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var attemptedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;

            return string.IsNullOrWhiteSpace(attemptedValue) ? null : _dbContext.Instructors.Find(int.Parse(attemptedValue));
        }
    }
}