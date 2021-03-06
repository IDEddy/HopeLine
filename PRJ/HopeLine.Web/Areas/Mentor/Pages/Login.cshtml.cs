using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HopeLine.Web.Areas.Mentor.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<HopeLineUser> _signInManager;
        private readonly UserManager<HopeLineUser> _userManager;
        private readonly IMessage _messageService;

        public LoginModel(SignInManager<HopeLineUser> signInManager, UserManager<HopeLineUser> userManager, IMessage messageService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _messageService = messageService;
        }

        [BindProperty]
        public LoginViewModel LoginInput { get; set; }
        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var url = Url.Content("~/");
                return Redirect(url);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl = returnUrl ?? Url.Content("~/mentor/index");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(LoginInput.Username);
                if (user != null)
                {
                    if (user.AccountType == Account.Mentor)
                    {
                        var res = await _signInManager.PasswordSignInAsync(LoginInput.Username, LoginInput.Password, true, false);
                        if (res.Succeeded)
                        {

                            System.Console.WriteLine("User has logged in.");
                            return LocalRedirect(returnUrl);
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login...");
            }
            return Page();
        }
    }
}