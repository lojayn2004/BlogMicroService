namespace BlogService.Specifications
{
    public class BaseSpeification
    {
        protected Func<object, bool> Criteria { get; private set; }

        protected List<Func<object, object>> Includes { get; } = new List<Func<object, object>>();
    }
}
