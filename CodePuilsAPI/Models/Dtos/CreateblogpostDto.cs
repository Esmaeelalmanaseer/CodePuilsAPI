namespace CodePuilsAPI.Models.Dtos
{
    public class CreateblogpostDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandler { get; set; }
        public DateTime pulishedDate { get; set; }
        public string Author { get; set; }
        public bool Isvisable { get; set; }
        public Guid[] Categores { get; set; }
    }
}
