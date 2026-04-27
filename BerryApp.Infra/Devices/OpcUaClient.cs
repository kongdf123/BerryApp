using Opc.Ua.Client;
using Opc.Ua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Infra.Devices
{
    public class OpcUaClient : IPlcClient
    {
        private Session _session;

        public async Task ConnectAsync(string endpointUrl)
        {
            var config = new ApplicationConfiguration
            {
                ApplicationName = "BerryMES OPC Client",
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    AutoAcceptUntrustedCertificates = true
                },
                TransportConfigurations = new TransportConfigurationCollection(),
                TransportQuotas = new TransportQuotas { OperationTimeout = 15000 },
                ClientConfiguration = new ClientConfiguration()
            };

            await config.Validate(ApplicationType.Client);

            var endpoint = CoreClientUtils.SelectEndpoint(endpointUrl, false);
            var endpointConfig = EndpointConfiguration.Create(config);
            var configuredEndpoint = new ConfiguredEndpoint(null, endpoint, endpointConfig);

            _session = await Session.Create(
                config,
                configuredEndpoint,
                false,
                "BerryMES Session",
                60000,
                null,
                null);
        }

        public async Task<Dictionary<string, double>> ReadTelemetryAsync()
        {
            var result = new Dictionary<string, double>();

            // Example NodeIds (you will adjust based on your OPC server)
            var nodes = new Dictionary<string, string>
            {
                { "1", "ns=2;s=Machine1.Temperature" },
                { "2", "ns=2;s=Machine2.Temperature" }
            };

            foreach (var kv in nodes)
            {
                var value = _session.ReadValue(kv.Value);

                if (value?.Value != null)
                {
                    result[kv.Key] = Convert.ToDouble(value.Value);
                }
            }

            return result;
        }
    }
}
