using Xunit;

namespace Overlord.Tests {
    public class RoundArgsParserTests {
        
        private RoundArgsParser parser = new RoundArgsParser();
        
        [Fact]
        public void ShouldParseEmptyArrayAsFirst() {
            var input = new string[0];
            Assert.Equal(0, parser.Parse(input));            
        }
        
        [Theory,
        InlineData(new string[] {"0"}, 0),
        InlineData(new string[] {"1"}, 1),
        InlineData(new string[] {"2"}, 2),
        InlineData(new string[] {"0", "2"}, 0)]
        public void ShouldParseArgsProperly(string[] args, int expected) {
            Assert.Equal(expected, parser.Parse(args));
        }
    }
}