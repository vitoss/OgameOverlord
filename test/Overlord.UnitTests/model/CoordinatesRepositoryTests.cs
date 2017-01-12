using Xunit;

using Overlord.Model;

namespace Overlord.Tests {
    public class CoordinatesRepositoryTests {
        
        [Fact]
        public void RepositoryShouldReturnAllCoordinates() {
            var repository = new CoordinatesRepository();
            var allCoordinates = repository.GetAll();
            
            Assert.True(allCoordinates.Length > 0, "There should be at least 1 coordinate in repository");
        }
    }
}