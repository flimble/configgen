using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using ConfigGen.ConfigGen.SwampyProxy;
using SAIG.PS.ConfigGen.Interfaces;

namespace SAIG.PS.ConfigGen
{
    public class ConfigurationCoordinator
    {


        protected readonly ITemplateReader _reader;
        protected readonly ITokenReplacer _replacer;
        protected readonly ITokenIdentifier _finder;
        protected readonly IEndpointService _proxy; 
        protected readonly ConfigWriter _writer;
        protected readonly ILog _logger = LogManager.GetLogger(typeof(ConfigurationCoordinator));

        public ConfigurationCoordinator(
            ITemplateReader reader, 
            ITokenReplacer replacer, 
            ITokenIdentifier finder, 
            IEndpointService  proxy, 
            ConfigWriter writer)
        {
            _reader = reader;
            _replacer = replacer;
            _finder = finder;
            _proxy = proxy;
            _writer = writer;
        }

        public void GenerateConfigs(string templatePath, string[] environmentname, string configSuffix, string appName, string targetpath)
        {
            
            _logger.DebugFormat("Starting Config Generation using template at {0}", templatePath);



            string templateText =
                _reader.Read(templatePath);


          
            _finder.SearchForTokens(templateText);
            var tokens = from t in _finder.TokensFound
                         select _finder.StripTokens(t);

            
          

            foreach (string environment in environmentname)
            {
                var request = new GetEndpointsRequest
                    {
                        callingApplication = "ConfigGen", 
                        environment = environment,
                        keys = tokens.ToArray()
                    };

                var endpoints = _proxy.GetEndpoints(request).GetEndpointsResult;



                var keyvalueReplacement = endpoints.ToDictionary(x => x.Key, y => y.Value);

                string generatedConfigText = _replacer.Replace(templateText, keyvalueReplacement);

                var singleRequest = new GetSingleEndpointRequest
                    {
                        callingApplication = "ConfigGen",
                        environment = environment,
                        key = string.Format("{0}.Host", appName)
                    };

                string servername = _proxy.GetSingleEndpoint(singleRequest).GetSingleEndpointResult.Value;

                string configName = string.Format("{0}.{1}.{2}", servername, environment, configSuffix);
                
                _writer.Write(targetpath, configName, generatedConfigText);
            }

        }
    }


}