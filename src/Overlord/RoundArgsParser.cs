
namespace Overlord {
    class RoundArgsParser {
        public int Parse(string[] args) {
            if(args.Length == 0) {
                return 0;
            }
            
            return int.Parse(args[0]);
        }
    }
}