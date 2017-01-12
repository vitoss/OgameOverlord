using System.Collections.Generic;
using System.Net.Http;

using Overlord.Model;

namespace Overlord {
    class ConfirmMovementParameters: FormEncodable {
        Coordinates coordinates;
        int smallTransporters;
        string token;
        
        public ConfirmMovementParameters(Coordinates coordinates, int smallTransporters, string token) {
            this.coordinates = coordinates;
            this.smallTransporters = smallTransporters;
            this.token = token;
        }
        
        public FormUrlEncodedContent Encoded() {
            return new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("holdingtime", "1"),
                new KeyValuePair<string, string>("expeditiontime", "1"),
                new KeyValuePair<string, string>("token", token),
                new KeyValuePair<string, string>("galaxy", coordinates.Galaxy.ToString()),
                new KeyValuePair<string, string>("system", coordinates.System.ToString()),
                new KeyValuePair<string, string>("position", coordinates.Position.ToString()),
                new KeyValuePair<string, string>("type", "1"),
                new KeyValuePair<string, string>("mission", "1"),
                new KeyValuePair<string, string>("union2", "0"),
                new KeyValuePair<string, string>("holdingOrExpTime", "0"),
                new KeyValuePair<string, string>("speed", "10"),
                new KeyValuePair<string, string>("acsValues", "-"),
                new KeyValuePair<string, string>("prioMetal", "1"),
                new KeyValuePair<string, string>("prioCrystal", "2"),
                new KeyValuePair<string, string>("prioDeuterium", "3"),
                new KeyValuePair<string, string>("am202", smallTransporters.ToString()),
                new KeyValuePair<string, string>("metal", "0"),
                new KeyValuePair<string, string>("crystal", "0"),
                new KeyValuePair<string, string>("deuterium", "0"),
            });
        }
        
    }
}