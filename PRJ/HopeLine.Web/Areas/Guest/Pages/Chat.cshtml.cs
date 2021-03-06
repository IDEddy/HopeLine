using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.CoreServices;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace HopeLine.Web.Areas.Guest.Pages
{
    public class ChatModel : PageModel
    {
        private readonly ICommonResource _commonResource;
        private readonly ICommunication _communication;
        public ChatModel(ICommunication communication, ICommonResource commonResource)
        {
            _commonResource = commonResource;
            _communication = communication;
        }

        [BindProperty]
        public string PIN { get; set; }
        [BindProperty]
        public List<string> Topics { get; set; }
        [BindProperty]
        public string UserName { get; set; }
        public string ReturnUrl { get; set; }

        public IActionResult OnGet(string pin = null, string user = null)
        {
            UserName = HttpContext.Session.GetString("_guest");
            System.Console.WriteLine("User = " + UserName);

            if (UserName != null)
            {
                if (pin == null)
                    PIN = _communication.GenerateConnectionId();

                else
                    PIN = pin;

                Topics = _commonResource.GetTopics().Select(t => t.Name).ToList();
                return Page();
            }
            else
            {
                string url = Url.Page("/Login", new { area = "Guest", returnUrl = "chat" });
                return LocalRedirect(url);
            }
        }
    }
}