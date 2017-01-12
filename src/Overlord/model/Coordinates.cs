
namespace Overlord.Model {
    
    class Coordinates {
        public int Galaxy { get; private set; }
        public int System { get; private set; }
        public int Position { get; private set; }
        
        public Coordinates(int galaxy, int system, int position) {
            Galaxy = galaxy;
            System = system;
            Position = position;
        }
        
        override public string ToString() {
            return $"{Galaxy}:{System}:{Position}";
        }
    }

}