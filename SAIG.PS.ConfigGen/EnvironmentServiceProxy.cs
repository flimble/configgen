using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAIG.PS.ConfigGen.SwampyEnvironments;

namespace SAIG.PS.ConfigGen
{
    public class EnvironmentServiceProxy : IEnvironmentServiceProxy
    {
        IEndpointService _service;

        public EnvironmentServiceProxy()
        {
            _service = new EndpointServiceClient();
        }



        public KeyPair[] GetEndpoints(string environment, string[] keys, string callingApplication)
        {
            return _service.GetEndpoints(environment, keys, callingApplication);
        }

        public KeyPair GetSingleEndpoint(string environment, string key, string callingApplication)
        {
            return _service.GetSingleEndpoint(environment, key, callingApplication);
        }
    }
}
