using System.Collections.Generic;
using System.Text.RegularExpressions;
using SAIG.PS.ConfigGen.Interfaces;
using log4net;

namespace SAIG.PS.ConfigGen
{
    public class TokenIdentifier : ITokenIdentifier
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof (TokenIdentifier));

        public List<string> TokensFound { get; private set; }


        public void SearchForTokens(string input)
        {
            string pattern = @"\[%.*?%\]";

            var result = new List<string>();

            foreach (var m in Regex.Matches(input, pattern))
            {
                string match = m.ToString();

                if (!result.Contains(match))
                {
                    result.Add(match);
                    _logger.DebugFormat("Found '{0}' token in template file", match);
                }

            }
            TokensFound = result;
        }


        

        public string StripTokens(string toStrip)
        {
            return toStrip.Substring(2, toStrip.Length - 4);
        }

        public string AppendTokens(string toAppend)
        {
            return string.Format("[%{0}%]", toAppend);
        }
    }
}
