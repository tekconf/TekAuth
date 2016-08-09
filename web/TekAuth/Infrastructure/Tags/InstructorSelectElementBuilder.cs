namespace TekAuth.Infrastructure.Tags
{
    using Tekconf.Data.Entities;

    public class InstructorSelectElementBuilder : EntitySelectElementBuilder<Instructor>
    {
        protected override int GetValue(Instructor instance)
        {
            return instance.ID;
        }

        protected override string GetDisplayValue(Instructor instance)
        {
            return instance.FullName;
        }
    }
}