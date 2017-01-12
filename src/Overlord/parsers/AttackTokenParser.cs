using System;
using System.Text.RegularExpressions;

namespace Overlord.Parser
{
    class AttackTokenParser: Parser<String> {
        public String Parse(string pageContent) {
            Match match = Regex.Match(pageContent, @"<input type='hidden' name='token' value='([\S]+)'", RegexOptions.IgnoreCase);
            
            if(match.Success) {
                return match.Groups[1].Value;
            }
            
            return "";
        }
    }
}