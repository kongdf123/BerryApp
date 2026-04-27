// ==========================================
// OPC UA CLIENT EXAMPLE (USING OPC FOUNDATION LIBRARY)
// ==========================================
/*
NuGet: OPCFoundation.NetStandard.Opc.Ua
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using System.Threading.Tasks;

namespace BerryApp.Infra.Devices
{
    public class OpcUaClientWrapper
    {
        private Session _session;

        public async Task ConnectAsync(string endpointUrl)
        {
            var config = new ApplicationConfiguration
            {
                ApplicationName = "IndustrialApp",
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    AutoAcceptUntrustedCertificates = true
                }
            };

            await config.Validate(ApplicationType.Client);

            var endpoint = CoreClientUtils.SelectEndpoint(endpointUrl, false);
            var endpointConfig = EndpointConfiguration.Create(config);
            var configuredEndpoint = new ConfiguredEndpoint(null, endpoint, endpointConfig);

            _session = await Session.Create(config, configuredEndpoint, false, "IndustrialApp", 60000, null, null);
        }

        public async Task WriteNodeAsync(string nodeId, bool value)
        {
            var writeValue = new WriteValue
            {
                NodeId = new NodeId(nodeId),
                AttributeId = Attributes.Value,
                Value = new DataValue(new Variant(value))
            };

            var collection = new WriteValueCollection { writeValue };
            await _session.WriteAsync(null, collection, CancellationToken.None);
        }
    }
}
