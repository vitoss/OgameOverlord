using Overlord.Model;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Overlord.Parser { 
    class ResourceReader: Parser<Resources> {
        
        XDocument document;
        
        public Resources Parse(string pageContent) {
            var loader = new HtmlDocumentLoader();
            document = loader.loadDocument(pageContent);
            
            var metal = ValueForSpanWithID("resources_metal");
            var crystal = ValueForSpanWithID("resources_crystal");
            var deuterium = ValueForSpanWithID("resources_deuterium");
            
            //test parser for reasources
            var resources = new Resources();
            resources.Metal = ParseResourceValue(metal);
            resources.Crystal = ParseResourceValue(crystal);
            resources.Deuter = ParseResourceValue(deuterium);
            
            return resources;
        }
        
        private string ValueForSpanWithID(string spanID) {
            return (from someSpan in document.Descendants("span")
                                where someSpan.Attribute("id")?.Value.Contains(spanID) == true
                                select someSpan.Value).First();
        }
        
        private int ParseResourceValue(string rawValue) {
            var sanitizedValue = rawValue.Trim().Replace(".", "");
            return int.Parse(sanitizedValue);
        }
    }
}