using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using TekAuth.App_Start;

namespace TekAuth.Infrastructure.Validation
{
    public class StructureMapValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return StructuremapMvc.ParentScope.CurrentNestedContainer.TryGetInstance(validatorType) as IValidator;
        }
    }
}