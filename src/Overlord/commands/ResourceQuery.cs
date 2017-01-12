using System.Threading.Tasks;
using Overlord.Model;
using Overlord.Parser;

namespace Overlord {
    
    class ResourceQuery : Command<Resources> {
        
        Kingdom kingdom;
        
        public ResourceQuery(UrlBuilder builder, Requester requester, Kingdom kingdom) : base(builder, requester) {
            this.kingdom = kingdom;
        }
        
        override public async Task<Resources> run() {
            
            var kingdomResources = new Resources();
            
            foreach(var planet in kingdom.Planets) {
                var planetOverviewPage = await requester.Get(planet.Link, "Resources");
                var planetResources = new ResourceReader().Parse(planetOverviewPage);
                
                kingdomResources.Metal += planetResources.Metal;
                kingdomResources.Crystal += planetResources.Crystal;
                kingdomResources.Deuter += planetResources.Deuter;
            }
            
            return kingdomResources;
        }
    }
}