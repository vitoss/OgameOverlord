using System.Threading.Tasks;
using Overlord.Model;
using Overlord.Parser;
using Microsoft.Extensions.Logging;

namespace Overlord {
    
    class KingdomQuery : Command<Kingdom> {
        
        public KingdomQuery(UrlBuilder builder, Requester requester) : base(builder, requester) {}
        
        override public async Task<Kingdom> run() {
            logger.LogDebug("Running Kingdom Query");
            
            var content = await requester.Get(builder.Create("/game/index.php?page=overview"), "Overview");
            var parser = new KingdomParser();
            
            return parser.Parse(content);
        }
    }
}