using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NDesk.Options;
using SAIG.PS.ConfigGen.ConfigGen.SwampyProxy;
using SAIG.PS.ConfigGen.Interfaces;
using log4net;

namespace SAIG.PS.ConfigGen    
{
    public class Program
    {
        private static IWindsorContainer _container;

        private static string[] GetReadArgsFromUserInput()
        {

            Console.WriteLine("Please enter path to template.xml...");
            var templatePath = string.Format("-template={0}", Console.ReadLine());
            

            Console.WriteLine("Please enter name of output directory...");
            var outdir = string.Format("-outdir={0}", Console.ReadLine());

            Console.WriteLine("Enter comma-separated list of environments to build...");
            var environments = string.Format("-environments={0}", Console.ReadLine());

            Console.WriteLine("Enter name of appication for which is being generated...");
            var appname = string.Format("-applicationName={0}", Console.ReadLine());

            return new string[] { templatePath, outdir, environments, appname };
        }

        /// <summary>
        /// Uses NDesk for commandline parsing.
        /// </summary>
        /// <seealso cref="www.ndesk.org"/>
        /// <param name="args"></param>
        public static int Main(string[] args)
        {
            ConfigureIOC();

            if(Environment.UserInteractive && args.Length == 0)
            {
                args = GetReadArgsFromUserInput();
            }

            try
            {
                var runner = new Runner(_container.Resolve<ConfigurationCoordinator>());
                runner.Run(args);

                return (int) ExitCode.Success;
            }
            catch (Exception e)
            {
                return (int) ExitCode.Failure;
            }                       
        }

      

        private static void ConfigureIOC()
        {
            _container = new WindsorContainer();
            _container.Register(
                Component.For<ITemplateReader>().ImplementedBy<TemplateReader>(), 
                Component.For<ITokenIdentifier>().ImplementedBy<TokenIdentifier>(), 
                Component.For<ITokenReplacer>().ImplementedBy<TokenReplacer>(),
                Component.For<ConfigWriter>().ImplementedBy<ConfigWriter>(),
                Component.For<IEndpointService>().Instance(new EndpointServiceClient()),
                Component.For<ConfigurationCoordinator>().ImplementedBy<ConfigurationCoordinator>()
                );

        }

    }
}
