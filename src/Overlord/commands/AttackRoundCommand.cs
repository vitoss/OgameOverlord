using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Overlord.Model;
using Overlord.Parser;

namespace Overlord {
    class AttackRoundCommand: Command<bool> {
        Kingdom kingdom;
        Coordinates[] targets;
        
        public AttackRoundCommand(UrlBuilder builder, Requester requester, Kingdom kingdom, Coordinates[] targets) : base(builder, requester) {
            this.kingdom = kingdom;
            this.targets = targets;
        }
        
        override public async Task<bool> run() {
            logger.LogInformation("Running Attack Round Command");
            
            logger.LogInformation("Navigating to Aceris");
            await requester.Get(kingdom.Aceris.Link, "AcerisAttack");
            
            var overallResult = true;
            
            foreach(var coordinates in targets) {
                logger.LogDebug($"Attack on {coordinates}");
                var attackCommand = new AttackCommand(builder, requester, coordinates);
                var attackResult = await attackCommand.run();
                overallResult = overallResult && attackResult;
            }
            
            logger.LogInformation("Navigating to fleet main page to finalize attack round");
            await requester.Get(builder.Create("/game/index.php?page=fleet1"), "FleetOne");
            
            return overallResult;
        }
    }
}