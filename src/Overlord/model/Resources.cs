
namespace Overlord.Model {

    class Resources {
        public int Metal;
        public int Crystal;
        public int Deuter;
        
        override public string ToString() {
            return $"M: {Metal} C: {Crystal} D: {Deuter}";
        }
    }

}