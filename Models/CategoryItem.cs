namespace PasswordAPI {
    public class CategoryItem {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryItem(int Id, string Name) {
            this.Id = Id;
            this.Name = Name;
        }
    }
}