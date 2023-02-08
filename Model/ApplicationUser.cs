using Microsoft.AspNetCore.Identity;

namespace FreshFarmMarket_210705C.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public string CreditCardNumber { get; set; }

        public string Gender { get; set; }

        public string MobileNumber { get; set; }

        public string DeliveryAddress { get; set; }

        public string Email { get; set; }

        public string ImageURL { get; set; } = string.Empty;

        public string AboutMe { get; set; } = string.Empty;
    }
}
