using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.Web.Pages
{
    public class InstantChatModel : PageModel
    {
        private readonly ICommonResource _commonResource;
        private readonly ICommunication _communication;
        private readonly SignInManager<HopeLineUser> _signInManager;

        public InstantChatModel(ICommunication communication, ICommonResource commonResource, SignInManager<HopeLineUser> signInManager)
        {
            _commonResource = commonResource;
            _communication = communication;
            _signInManager = signInManager;
        }

        [BindProperty]
        public string PIN { get; set; }

        [BindProperty]
        public IList<TopicViewModel> Topics { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public int IsUser { get; set; }
        public string ReturnUrl { get; set; }

        public IActionResult OnGet()
        {
            /*
             if user is authenticated, check if user is a mentor
                if user is a mentor, redirect user to mentor profile page
                if user is not a mentor, user can access Instant Chat
            if user is not authenticated
                user can access Instant Chat
             */
            var claim = User.Claims.FirstOrDefault(u => u.Type == "Account");
            var url = Url.Page("/Index", new { area = "Mentor" });
            if (User.Identity.IsAuthenticated)
            {
                if (claim.Value == "Mentor")
                {
                    return Redirect(url);

                }
                return Page();
            }
            else
            {


                Topics = _commonResource.GetTopics().Select(t => new TopicViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                }).ToList();

                UserName = HttpContext.Session.GetString("_guest");
                if (UserName != null)
                {
                    HttpContext.Session.SetString("_guest", UserName);
                }
                else
                {
                    var name = "Guest" + Guid.NewGuid().ToString("N").Substring(0, 12);
                    UserName = name;
                    HttpContext.Session.SetString("_guest", UserName);
                }
                // Topics = _commonResource.GetTopics().Select(t => t.Name).ToList();
                return Page();
            }
            //else
            //{
            //    return LocalRedirect(url);

            //}
        }

        public IActionResult OnPost(string pin = null, string user = null)
        {

            if (!ModelState.IsValid || _signInManager.IsSignedIn(User))
            {
                return Redirect("~/");
            }

            UserName = HttpContext.Session.GetString("_guest");

            if (UserName != null)
            {
                HttpContext.Session.SetString("_guest", UserName);
            }
            else
            {
                var name = "Guest" + Guid.NewGuid().ToString("N").Substring(0, 12);
                UserName = name;
                HttpContext.Session.SetString("_guest", UserName);
            }

            return Page();

        }

    }
}