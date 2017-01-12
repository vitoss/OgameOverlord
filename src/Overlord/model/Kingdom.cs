using System;
using System.Collections.Generic;

namespace Overlord.Model {

    class Kingdom {
        public List<Planet> Planets { get; set; }
        
        public Planet Aceris {
            get {
                return Planets.Find((planet) => planet.Name.Equals("Aceris", StringComparison.OrdinalIgnoreCase));
            }   
        }
    }

}