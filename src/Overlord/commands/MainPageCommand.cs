using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Overlord {
    
    class MainPageCommand : Command<string> {
        
        public MainPageCommand(UrlBuilder builder, Requester requester) : base(builder, requester) {}
        
        override public Task<string> run() {
            logger.LogInformation("Running Main Page Command");
            return requester.Get(builder.Create("/"), "main");
        }
    }
}