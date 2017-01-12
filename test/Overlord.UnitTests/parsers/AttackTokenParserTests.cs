using System;
using Xunit;
using Overlord.Parser;

namespace Overlord.Tests {
    public class AttackTokenParserTests {
        
        private AttackTokenParser tokenParser = new AttackTokenParser();
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void ReturnFalseGivenValuesLessThan2(int value)
        {
            Assert.False(false, String.Format("{0} should not be prime", value));
        }
        
        [Fact]
        public void EmptyPageContentShouldReturnEmptyToken() {
            var token = tokenParser.Parse("");
            Assert.Equal("", token);
        }
        
        [Fact]
        public void PageWithTokenShouldReturnProperToken() {
            var testToken = "TEST_TOKEN";
            var pageContent = CreateTestHtml(testToken);
            var token = tokenParser.Parse(pageContent);
            
            Assert.Equal(testToken, token);
        }
        
        [Fact]
        public void PageWithEmptyTokenShouldReturnEmptyToken() {
            var testToken = "";
            var pageContent = CreateTestHtml(testToken);
            var token = tokenParser.Parse(pageContent);
            
            Assert.Equal(testToken, token);
        }
        
        private string CreateTestHtml(string token) {
            return "<html><input type='hidden' name='token' value='" + token + "'/></html>";
        }
    }
}