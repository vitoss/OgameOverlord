using System;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Overlord.Parser {
    class HtmlDocumentLoader {
        public XDocument loadDocument(string pageContent) {
            var sanitized = stripUnusedTags(pageContent);
            
            sanitized = fixUnclosedTags(sanitized);
            
            sanitized = Regex.Replace(sanitized, "&(?!amp;)", "&amp;");
            
            return tryParse(sanitized);
        }
        
        private string stripUnusedTags(string pageContent) {
            Regex removeImages = new Regex("<img[^>]*>");
            Regex removeScriptTags = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            
            var sanitized = removeImages.Replace(pageContent, "");
            return removeScriptTags
                    .Replace(sanitized, "")
                    .Replace("<br />", "")
                    .Replace("class=\"transport_ecke\"", "")
                    .Replace("<tbody>", "")
                    .Replace("</tbody>", "");
        }
        
        private XDocument tryParse(string rawContent) {
            try {
                return XDocument.Parse(rawContent);
            } catch(Exception) {
                File.WriteAllText("pages/invalid_page.html", rawContent);
                throw;
            }
        }
        
        private string fixUnclosedTags(string pageContent) {
            var removeEmptyQueueTableBug = new Regex(@"</table>[\n\r\s]+</table>[\n\r\s]+</div>");
            return removeEmptyQueueTableBug.Replace(pageContent, "</table></div>")
                .Replace("komputerowa\">", "komputerowa\"/>")
                // .Replace("=\"Kopalnia metalu\">", "=\"Kopalnia metalu\"/>")
                .Replace("alt=\"Opancerzenie\">", "alt=\"Opancerzenie\"/>");
        }
    }
}