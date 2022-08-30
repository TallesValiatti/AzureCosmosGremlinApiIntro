using System.Net.WebSockets;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure.IO.GraphSON;

namespace Contexts
{
    public class GraphContext : IDisposable
    {
        private string Host = "aca-my-app-eastus.gremlin.cosmos.azure.com";
        private string PrimaryKey = "<your-primary-key>";
        private string Database = "my-database";
        private string Container = "my-graph";
        private bool EnableSSL = true; 
        private int Port = 443;

        private GremlinClient _gremlinClient;
        public GremlinClient GremlinClient
        {
            get { return _gremlinClient; }
            set { _gremlinClient = value; }
        }
        
        public GraphContext()
        {
            string containerLink = "/dbs/" + Database + "/colls/" + Container;
            var gremlinServer = new GremlinServer(Host, Port, enableSsl: EnableSSL, 
                                                    username: containerLink, 
                                                    password: PrimaryKey);

            ConnectionPoolSettings connectionPoolSettings = new ConnectionPoolSettings()
            {
                MaxInProcessPerConnection = 10,
                PoolSize = 30, 
                ReconnectionAttempts= 3,
                ReconnectionBaseDelay = TimeSpan.FromMilliseconds(500)
            };

            var webSocketConfiguration = new Action<ClientWebSocketOptions>(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(10);
            });

            _gremlinClient = new GremlinClient(gremlinServer, 
                                               new GraphSON2Reader(),
                                               new GraphSON2Writer(), 
                                               GremlinClient.GraphSON2MimeType, 
                                               connectionPoolSettings, 
                                               webSocketConfiguration);
        }

        public void Dispose()
        {
            if(_gremlinClient is not null)
                _gremlinClient.Dispose();
        }
    }
}