﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Configurations;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twilio.Jwt.AccessToken;

namespace HopeLine.Web.Pages
{
    public class VideoChatModel : PageModel
    {


        //// Substitute your Twilio AccountSid and ApiKey details
        ////private var AccountSid = "accountSid";
        ////private var ApiKeySid = "apiKeySid;
        ////private var ApiKeySecret = "apiKeySecret";
        ////private var identity = "example-user";

            
        private readonly ICommunication _communicationService;

        [BindProperty]
        public string Token { get; set; }



        public VideoChatModel(ICommunication communicationService)
        {
            _communicationService = communicationService;
            var grant = new VideoGrant();
            grant.Room = "cool room";
            var grants = new HashSet<IGrant> { grant };
            var identity = "example-user";

            // Create an Access Token generator
            var token = new Token(APIConstant.TwilioAccountSID, APIConstant.TwilioApiSID, APIConstant.TwilioSecret, identity: identity, grants: grants);
            Token = token.ToJwt();
            Console.WriteLine("Here is the token: " + token.ToJwt());
            // Serialize the token as a JWT
            Console.WriteLine(token.ToJwt());
        }

        [BindProperty]
        public string RoomId { get; set; }
        
        public IActionResult OnGet(string roomId)
        {
            RoomId = roomId;
            return Page();
            //RoomId = _communicationService.GenerateConnectionId();
        }
        
    }
}