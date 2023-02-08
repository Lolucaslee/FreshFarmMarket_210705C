using FreshFarmMarket_210705C.Model;
using FreshFarmMarket_210705C.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreshFarmMarket_210705C.Pages
{
    public class RegisterModel : PageModel
    {

        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment environment;

        [BindProperty]
        public IFormFile? Upload { get; set; }

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, 
        RoleManager<IdentityRole> roleManager, 
        IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.environment = environment;
        }



        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");
                if (Upload != null)
                {
                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var file = Path.Combine(environment.ContentRootPath, "wwwroot\\uploads", imageFile);
                    using var fileStream = new FileStream(file, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    RModel.ImageURL = "/uploads/" + imageFile;
                }
                var user = new ApplicationUser()
                {
                    FullName = RModel.FullName,
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    CreditCardNumber = protector.Protect(RModel.CreditCardNumber),
                    MobileNumber = RModel.MobileNumber,
                    Gender = RModel.Gender,
                    DeliveryAddress = RModel.DeliveryAddress,
                    ImageURL = RModel.ImageURL,
                    AboutMe = RModel.AboutMe,
                };
                //Create the Admin role if NOT exist
                IdentityRole role = await roleManager.FindByIdAsync("Admin");
                if (role == null)
                {
                    IdentityResult result2 = await roleManager.CreateAsync(new IdentityRole("Admin"));
                    if (!result2.Succeeded)
                    {
                        ModelState.AddModelError("", "Create role admin failed");
                    }
                }

                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    //Add users to Admin Role
                    result = await userManager.AddToRoleAsync(user, "Admin");


                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}


