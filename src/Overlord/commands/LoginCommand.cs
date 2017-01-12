using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net.Http;

namespace Overlord {
    
    class LoginCommand : Command<string> {
        
        public LoginCommand(UrlBuilder builder, Requester requester) : base(builder, requester) {}
        
        override public Task<string> run() {
            logger.LogInformation("Running Login Command");
            
            var loginPayload = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("kid", ""),
                new KeyValuePair<string, string>("uni", "universe-name-to-substitute.ogame.gameforge.com"),
                new KeyValuePair<string, string>("login", "username-to-substitute"),
                new KeyValuePair<string, string>("pass", "password-to-substitute")
            });
                
            var redirectContent = requester.PostForm(builder.Create("/main/login"), loginPayload, "login").Result; 
            var loginRedirectedContent = requester.Get(GetRedirectUrl(redirectContent), "login-redirect").Result;
            var entry = requester.Get(GetRedirectUrl(loginRedirectedContent), "entry");
            
            return entry;
        }
        
        private string GetRedirectUrl(string content) {
            Match match = Regex.Match(content, @"http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?", RegexOptions.IgnoreCase);
            
            if(match.Success) {
                logger.LogDebug($"Found redirected url {match.Value}");
                return match.Value;
            }
            
            return "";
        }
    }
}