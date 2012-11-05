using System.Collections.Generic;

namespace SAIG.PS.ConfigGen.Interfaces
{
    public interface ITokenReplacer
    {
        string Replace(string templateText, Dictionary<string, string> replacementValues);
    }
}