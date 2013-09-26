using System.Collections.Generic;
using ConfigGen.Swampy.Service;
using NUnit.Framework;


namespace SAIG.PS.ConfigGen.IntegrationTest
{
    [TestFixture]
    public class Create_QVAS_Config
    {
        [Test]
        [Explicit("not working yet. to be developed - presumably this is to be used to test specific files")]
        public void Do()
        {
            var identifier = new TokenIdentifier();

            var coordinator = new ConfigurationCoordinator(
                new TemplateReader(),
                new TokenReplacer(identifier),
                identifier,
                new EndpointServiceClient(),                
                new ConfigWriter()
                );

            var underTest = new Runner(coordinator);

            var apps = new Dictionary<string, string>
                {
                    {"QVAS", @"SAIG.PS.QVAS\QVAS.Host\app.config.template.xml"},                    

                };

            foreach (var app in apps)
            {

                var args = new[]
                    {
                        string.Format("-template=C:\\Code\\SAIGPS\\SSR\\Trunk\\{0}", app.Value),
                        string.Format("-outdir=GeneratedConfigs\\{0}", app.Key),
                        @"-environments=SIT1,SIT2",
                        string.Format("-applicationName={0}", app.Key)
                    };

                underTest.Run(args);

            }

        }
    }

  
}
