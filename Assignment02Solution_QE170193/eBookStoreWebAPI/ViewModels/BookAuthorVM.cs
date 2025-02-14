namespace eBookStoreWebAPI.ViewModels
{
    public class BookAuthorVM
    {

        public int AuthorId { get; set; }

        public int BookId { get; set; }
        public string AuthorOther { get; set; } = string.Empty;
        public double RoyaltyPercentage { get; set; }

        public virtual AuthorVM Author { get; set; } = null!;

    }
}
