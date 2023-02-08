using System.ComponentModel.DataAnnotations;
using FreshFarmMarket_210705C.ViewModels;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;

namespace FreshFarmMarket_210705C.ViewModels
{
    public class Register
    {
        [Required][RegularExpression(@"^[a-zA-Z0-9]{1,}$", ErrorMessage = "No special character allowed for full name")]
        public string FullName { get; set; } = string.Empty;

        [Required][RegularExpression(@"[0-9]{12,16}$", ErrorMessage = "No special character and letter allowed for credit card, please enter 12-16 numbers for credit card number")]
        public string CreditCardNumber { get; set; }


        [Required]
        public string Gender { get; set; }


        [Required][RegularExpression(@"[0-9]{8}$", ErrorMessage = "No special character and letter allowed for mobile number, please enter 8 numbers for phone number")]
        public string MobileNumber { get; set; }

        [Required][RegularExpression(@"^[a-zA-Z0-9]{1,}$", ErrorMessage = "No special character allowed for delivery address")]
        public string DeliveryAddress { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }


        [Required][DataType(DataType.Password)]
        public string Password { get; set; }


        [Required][DataType(DataType.Password)][Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

        public string ImageURL { get; set; } = string.Empty;

        public string AboutMe { get; set; } = string.Empty;
    }


}
