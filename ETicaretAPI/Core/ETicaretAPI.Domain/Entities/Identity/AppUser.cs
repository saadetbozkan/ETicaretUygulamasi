using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Domain.Entities.Identity
{
    public class AppUser  : IdentityUser<string>
    {
        public string NameSurname { get; set; }
        public int MyProperty { get; set; }
        public bool IsAdmin { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public ICollection<Basket> Baskets { get; set; }
    }
}
