using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Overlord.Model;
using Overlord.Parser;

namespace Overlord {
    
    class AttackCommand : Command<bool> {
        Coordinates coordinates;
        
        public AttackCommand(UrlBuilder builder, Requester requester, Coordinates coordinates) : base(builder, requester) {
            this.coordinates = coordinates;
        }
        
        override public async Task<bool> run() {
            logger.LogInformation("Running Attack Command");
            
            var smallTransporters = 30;
            
            logger.LogInformation("Opening first fleet screen");
            await requester.Get(builder.Create("/game/index.php?page=fleet1"), "FleetOne");
            
            logger.LogInformation("Choosing fleet");
            var chooseFleetPayload = new ChooseFleetParameters(coordinates, smallTransporters).Encoded();
            var fleet2Content = await requester.PostForm(builder.Create("/game/index.php?page=fleet2"), chooseFleetPayload, "FleetTwo");
            
            logger.LogInformation("Choosing cargo");
            var chooseCargoPayload = new ChooseCargoParameters(coordinates, smallTransporters).Encoded();
            var fleet3Content = await requester.PostForm(builder.Create("/game/index.php?page=fleet3"), chooseCargoPayload, "FleetThree");
            
            var tokenParser = new AttackTokenParser();
            var token = tokenParser.Parse(fleet3Content);
            
            var confirmMovementPayload = new ConfirmMovementParameters(coordinates, smallTransporters, token).Encoded();
            var confirmMovementContent = await requester.PostForm(builder.Create("/game/index.php?page=movement"), confirmMovementPayload, "ConfirmMovement");
            
            return true;
        }
    }
}