using System.Collections.Generic;
using System.Net.Http;

using Overlord.Model;

namespace Overlord {
    class ChooseCargoParameters: FormEncodable {
        Coordinates coordinates;
        int smallTransporters;
        
        public ChooseCargoParameters(Coordinates coordinates, int smallTransporters) {
            this.coordinates = coordinates;
            this.smallTransporters = smallTransporters;
        }
        
        public FormUrlEncodedContent Encoded() {
            return new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("type", "1"),
                new KeyValuePair<string, string>("mission", "0"),
                new KeyValuePair<string, string>("union", "0"),
                new KeyValuePair<string, string>("am202", smallTransporters.ToString()),
                new KeyValuePair<string, string>("galaxy", coordinates.Galaxy.ToString()),
                new KeyValuePair<string, string>("system", coordinates.System.ToString()),
                new KeyValuePair<string, string>("position", coordinates.Position.ToString()),
                new KeyValuePair<string, string>("acsValues", "-"),
                new KeyValuePair<string, string>("speed", "10"),
            });
        }
        
    }

}