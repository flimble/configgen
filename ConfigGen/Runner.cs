using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using NDesk.Options;
using log4net;

namespace SAIG.PS.ConfigGen
{
    public class Runner
    {
        private static ILog _logger;
        private ConfigurationCoordinator _coordinator;
        
        public Runner(ConfigurationCoordinator coordinator)
        {
            _coordinator = coordinator;
        }

        public void Run(string[] args)
        {
            _logger = LogManager.GetLogger(typeof(Runner));

            _logger.Debug(String.Join(",", args));

            

            bool helpRequested = false;
            string configSuffix = "config";

            string template = null, outdir = null, application = null;
            string[] environments = null;

            var p = new OptionSet()
                {
                    {"help|?", "show this message and exit", h => helpRequested = (h != null) },
                    {"template=", "The config template file", t => template = t},
                    {"outdir=", "The output directory folder name for created configs", o => outdir = o},
                    {"environments=", "Comma separated list of environment names e.g. 'QA1,QA2'", e => environments = e.Split(',')},
                    {"applicationName=", "Name of application for which config is generated", a => application = a },
                    {"configName=", "Optional config suffix name to override default value 'config'",  c => configSuffix = c}
                };

            p.Parse(args);

            if (helpRequested)
                ShowHelp(p);



            if (String.IsNullOrEmpty(template))
                throw new ArgumentNullException(template);

            if (String.IsNullOrEmpty(outdir))
                throw new ArgumentNullException(outdir);

            if (String.IsNullOrEmpty(application))
                throw new ArgumentNullException(application);
          
            _coordinator.GenerateConfigs(template, environments, configSuffix, application, outdir);
        }

        private  void ShowHelp(OptionSet p)
        {
            p.WriteOptionDescriptions(System.Console.Out);
        }
    }
}
