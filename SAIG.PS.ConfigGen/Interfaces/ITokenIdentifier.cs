using System.Collections.Generic;

namespace SAIG.PS.ConfigGen.Interfaces
{
    public interface ITokenIdentifier
    {
        List<string> TokensFound { get; }
        void SearchForTokens(string input);
        string StripTokens(string toStrip);
        string AppendTokens(string toAppend);
    }
}