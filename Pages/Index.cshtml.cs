using FreshFarmMarket_210705C.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace FreshFarmMarket_210705C.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<IndexModel> _logger;



        public IndexModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [BindProperty]
        public ApplicationUser user { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {

            var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
            var protector = dataProtectionProvider.CreateProtector("MySecretKey");
            user = await userManager.GetUserAsync(User);
            // Decrypt the creditcard
            user.CreditCardNumber = protector.Unprotect(user.CreditCardNumber);
            return Page();

        }
    }
}