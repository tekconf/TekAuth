namespace TekAuth
{
    public class MyHeaderAttribute : FromHeaderAttribute
    {
        public MyHeaderAttribute()
            : base("MyHeaderName")
        {
        }
    }
}