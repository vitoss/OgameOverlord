using Xunit;

using Overlord.Model;

namespace Overlord.Tests {
    public class TargetElectorTests {
        
        [Fact]
        public void EmptyCoordinatesShouldReturnEmptyElection() {
            var coordinates = new Coordinates[0];
            var elector = new TargetElector(coordinates, 10);
            var targets = elector.SelectRound(1);
            Assert.Equal(0, targets.Length);
        }
        
        [Fact]
        public void ZeroRoundShouldBeFromBegining() {
            var coordinates = MockCoordinates(20);
            var elector = new TargetElector(coordinates, 10);
            var targets = elector.SelectRound(0);
            
            Assert.Equal(coordinates[0], targets[0]);
        }
        
        [Fact]
        public void FirstRoundShouldStartWithProperElement() {
            var coordinates = MockCoordinates(20);
            var elector = new TargetElector(coordinates, 10);
            var targets = elector.SelectRound(1);
            
            Assert.Equal(coordinates[10], targets[0]);
        }
        
        [Fact]
        public void RoundShouldHaveProperLength() {
            var roundSize = 11;
            var coordinates = MockCoordinates(20);
            var elector = new TargetElector(coordinates, roundSize);
            var targets = elector.SelectRound(0);
            
            Assert.Equal(roundSize, targets.Length);
        }
        
        [Fact]
        public void LastRoundShouldHaveProperLength() {
            var roundSize = 11;
            var coordinates = MockCoordinates(20);
            var elector = new TargetElector(coordinates, roundSize);
            var targets = elector.SelectRound(1);
            
            Assert.Equal(9, targets.Length);
        }
        
        private Coordinates[] MockCoordinates(int amount) {
            var mocks = new Coordinates[amount];
            
            for(var i=0; i<amount; i++) {
                mocks[i] = new Coordinates(1, 1, 1);
            }
            
            return mocks;
        }
    }
}