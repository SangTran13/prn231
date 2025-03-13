using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class Member : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        public virtual ICollection<Order> Orders { get; set; } = [];
    }
}
