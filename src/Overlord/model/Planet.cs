
namespace Overlord.Model {

    class Planet {
        public string ID { get; set; }
        public string Name { get; set; }
        
        public string Link {
            get { 
                return "https://universe-name-to-substitute.ogame.gameforge.com/game/index.php?page=overview&cp=" + ID;
            }
        }
    }

}