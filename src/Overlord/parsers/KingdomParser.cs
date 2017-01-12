using Overlord.Model;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Overlord.Parser { 
    class KingdomParser: Parser<Kingdom> {
        
        XDocument document;
        
        public Kingdom Parse(string pageContent) {
            var loader = new HtmlDocumentLoader();
            document = loader.loadDocument(pageContent);
            
            var kingdom = new Kingdom();
            var planets = from planetDiv in document.Descendants("div")
                            let anchor = planetDiv.Element("a")
                            let nameSpan = anchor?.Element("span")
                            let id = planetDiv.Attribute("id")?.Value.Replace("planet-", "") ?? "UNKNOWN"
                            let planetName = nameSpan?.Value.Trim() ?? "CannotParse"
                            where planetDiv.Attribute("class")?.Value.Contains("smallplanet") == true
                            select new Planet() { Name = planetName, ID = id };
            
            kingdom.Planets = planets.ToList();
            
            return kingdom;
        }
    }
}