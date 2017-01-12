using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Overlord {
    
    class InitCommand : Command<bool> {
        
        public InitCommand(Requester requester) : base(new UrlBuilder("pl.ogame.gameforge.com", true), requester) {}
        
        async override public Task<bool> run() {
            logger.LogInformation("Running Init Command");
            await new MainPageCommand(builder, requester).run();
            
            Task.Delay(1000).Wait();
            
            await new LoginCommand(builder, requester).run();
            
            return true;
        }
    }
}