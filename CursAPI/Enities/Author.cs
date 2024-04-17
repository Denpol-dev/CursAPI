namespace CursAPI.Enities
{
    public class Author
    {
        public Author()
        {
            Books = [];
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Book> Books { get; set; }
    }
}
