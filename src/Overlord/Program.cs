using System;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Overlord.Parser;
// using Microsoft.Framework.Logging;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.FileProviders;
// using Microsoft.Extensions.PlatformAbstractions;
// using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging;
using System.Linq;

using Overlord.Model;

namespace Overlord
{
    public class Program
    {      
        static ILogger Logger { get; } = ApplicationLogging.CreateLogger<Program>();
        public static void Main(string[] args)
        {
            Logger.LogInformation("Overlord started.");
            
            // TestLoadingDocument("pages/FleetThree");
            // ExtractToken("pages/FleetThree");
            // return;
            
            var requester = new Requester(true);
            
            new InitCommand(requester).run().Wait();
            
            Logger.LogInformation("Initialization successful");
            
            var urlBuilder = new UrlBuilder("universe-name-to-substitute.ogame.gameforge.com", true);
            var kingdom = new KingdomQuery(urlBuilder, requester).run().Result;
            
            foreach(var planet in kingdom.Planets) {
                Logger.LogDebug($"Detected planet: {planet.Name}");
            }
            
            var roundParser = new RoundArgsParser();
            var roundNumber = roundParser.Parse(args);
            
            Logger.LogDebug("Sending round number: " + roundNumber);
            
            var coordinatesRepository = new CoordinatesRepository();
            var allTargets = coordinatesRepository.GetAll();
            var maxSimultaneousFleets = 16;
            var fleetLimit = 14;
            var elector = new TargetElector(allTargets, maxSimultaneousFleets);
            var round = elector.SelectRound(roundNumber).Take(fleetLimit).ToArray();
            
            Logger.LogDebug($"Selected round with {round.Length} targets.");
            
           var roundResult = new AttackRoundCommand(urlBuilder, requester, kingdom, round).run().Result;
            
            // var resources = new ResourceQuery(urlBuilder, requester, kingdom).run().Result;
            // Logger.LogDebug($"Resources {resources}");          
            
            Logger.LogInformation("Overlord ended.");
        }
        
        private static void TestFunc() {
            var reader = new ResourceReader();
            
            var resources = reader.Parse(File.ReadAllText("pages/entry.html"));
            
            Logger.LogDebug($"Metal: {resources.Metal}");
        }
        
        private static void TestLoadingDocument(string file) {
            Logger.LogDebug("Testing kingdom parser");
            var content = File.ReadAllText(file);
            var loader = new HtmlDocumentLoader();
            var document = loader.loadDocument(content);
            var parser = new KingdomParser();
            var kingdom = parser.Parse(content);
            
            Logger.LogDebug($"Found {kingdom.Planets.Count} planets.");
        }
        
        private static void ExtractToken(string fileName) {
            var content = File.ReadAllText(fileName);
            var parser = new AttackTokenParser();
            
            var token = parser.Parse(content);
            
            Logger.LogDebug($"Token: {token}");
        }
    }
}
