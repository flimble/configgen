using System.IO;
using System.Text;
using SAIG.PS.ConfigGen.Interfaces;
using log4net;

namespace SAIG.PS.ConfigGen
{
    public class TemplateReader : ITemplateReader
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(TemplateReader));

        public string Read(string templatePath)
        {
            _logger.DebugFormat("Reading template data from: {0}", templatePath);

            return File.ReadAllText(templatePath, Encoding.Default);
        }
    }
}
