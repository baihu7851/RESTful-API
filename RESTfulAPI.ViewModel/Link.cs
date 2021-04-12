namespace RESTfulAPI.ViewModel
{
    public class Link
    {
        public string Href { get; set; }

        public Link(string href)
        {
            Href = href;
        }
    }
}