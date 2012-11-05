using System.IO;
using log4net;

namespace SAIG.PS.ConfigGen
{
    public class ConfigWriter
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof (ConfigWriter));

        public void Write(string path, string configName, string fileText)
        {
            if (!Directory.Exists(path))
            {
                _logger.DebugFormat("Folder: {0} does not exist. Creating...", path);   
                Directory.CreateDirectory(path);
            }

            string configPath = Path.Combine(path, configName);

            _logger.DebugFormat("Writing config file: {0}", configPath);
            File.WriteAllText(configPath, fileText);
        }

    }
}
