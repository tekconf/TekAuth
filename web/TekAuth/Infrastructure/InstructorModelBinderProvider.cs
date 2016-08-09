namespace TekAuth.Infrastructure
{
    using System;
    using System.Web.Mvc;
    using App_Start;
    using Tekconf.Data.Entities;

    public class InstructorModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            return typeof(Instructor).IsAssignableFrom(modelType) ? StructuremapMvc.ParentScope.GetInstance<InstructorModelBinder>() : null;
        }
    }
}