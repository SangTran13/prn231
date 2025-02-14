using System.Text.Json.Serialization;

namespace eBookStoreWebAPI.ViewModels
{
    public class UserVM
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [JsonIgnore]
        public string Source { get; set; } = string.Empty;

        [JsonIgnore]
        public int RoleId { get; set; }

        [JsonIgnore]
        public int PublisherId { get; set; }

        [JsonIgnore]
        public DateTime HireDate { get; set; }
    }
}
