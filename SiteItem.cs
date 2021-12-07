namespace PasswordAPI {
    public class SiteItem {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string CreationDate { get; set; }
        public string Description { get; set; }
        public CategoryItem Category { get; set; }
    }
}