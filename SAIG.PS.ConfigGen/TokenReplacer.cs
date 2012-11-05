using System.Collections.Generic;
using System.Text;
using SAIG.PS.ConfigGen.Interfaces;
using log4net;

namespace SAIG.PS.ConfigGen
{
    public class TokenReplacer : ITokenReplacer
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof (TokenReplacer));

        private readonly ITokenIdentifier _identifier;

        public TokenReplacer(ITokenIdentifier identifier)
        {
            _identifier = identifier;
        }

        public string Replace(string templateText, Dictionary<string, string> replacementValues)
        {
            _logger.DebugFormat("Replacing template tokens with replacement values");

            var builder = new StringBuilder(templateText);

            foreach(var replacement in replacementValues)
            {
                var tokenizedReplacement = _identifier.AppendTokens(replacement.Key);

                builder.Replace(tokenizedReplacement, replacement.Value);
                _logger.DebugFormat("Replacing token: {0} with value: {1}", tokenizedReplacement, replacement.Value);
            }

            return builder.ToString();

        }
    }
}
