using AzureCosmosGremlinApiIntro.Services.Interfaces;
using Contexts;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Messages;

namespace AzureCosmosGremlinApiIntro.Services
{
    public class GraphRepository : IGraphRepository
    {
        private readonly GraphContext _graphContext;

        public GraphRepository(GraphContext graphContext)
        {
            _graphContext = graphContext;
        }

        public async Task<IEnumerable<string>> GetAllDevicesAsync(string email)
        {
             var query = @"g.V()
                            .has('value', @email)
                            .outE()
                            .hasLabel('linked_to')
                            .inV()
                            .inE()
                            .hasLabel('linked_to')
                            .outV()
                            .hasLabel('Device')
                            .values('udid')";

            query = query.Replace("@email", $"'{email}'");

            var request = RequestMessage.Build(Tokens.OpsEval)
                                        .AddArgument(Tokens.ArgsGremlin, query)
                                        .AddArgument(Tokens.ArgsEvalTimeout, 500)
                                        .Create();

            var result = await _graphContext.GremlinClient.SubmitAsync<string>(request);
            
            return result.AsEnumerable<string>();
        }

        public async Task<IEnumerable<string>> GetAllPhoneNumbersAsync(string email)
        {
            var query = @"g.V()
                            .has('value', @email)
                            .outE()
                            .hasLabel('linked_to')
                            .inV()
                            .inE()
                            .hasLabel('linked_to')
                            .outV()
                            .hasLabel('Device')
                            .outE()
                            .hasLabel('uses')
                            .inV()
                            .values('value')";

            query = query.Replace("@email", $"'{email}'");

            var request = RequestMessage.Build(Tokens.OpsEval)
                                        .AddArgument(Tokens.ArgsGremlin, query)
                                        .AddArgument(Tokens.ArgsEvalTimeout, 500)
                                        .Create();

            var result = await _graphContext.GremlinClient.SubmitAsync<string>(request);
            
            return result.AsEnumerable<string>();
        }

        public async Task<string> GetBankAccountAsync(string email)
        {
            var query = @"g.V()
                           .hasLabel('Email')
                           .has('value', @email)
                           .outE()
                           .hasLabel('linked_to')
                           .inV()
                           .values('number')";

            query = query.Replace("@email", $"'{email}'");

            var request = RequestMessage.Build(Tokens.OpsEval)
                                        .AddArgument(Tokens.ArgsGremlin, query)
                                        .AddArgument(Tokens.ArgsEvalTimeout, 500)
                                        .Create();

            var result = await _graphContext.GremlinClient.SubmitAsync<string>(request);
            
            return result.First();   
        }

        public async Task<string> GetCityAsync(string email)
        {
            var query = @"g.V()
                           .hasLabel('Email')
                           .has('value', @email)
                           .inE()
                           .hasLabel('sign_in_with')
                           .outV()
                           .outE()
                           .hasLabel('lives_in')
                           .inV()
                           .values('name')";

            query = query.Replace("@email", $"'{email}'");

            var request = RequestMessage.Build(Tokens.OpsEval)
                                        .AddArgument(Tokens.ArgsGremlin, query)
                                        .AddArgument(Tokens.ArgsEvalTimeout, 500)
                                        .Create();

            var result = await _graphContext.GremlinClient.SubmitAsync<string>(request);
            
            return result.First();   
        }

        public async Task<bool> CustomerExistsAsync(string email)
        {   
            var query = @"g.V()
                           .hasLabel('Email')
                           .has('value', @email)
                           .count()";

            query = query.Replace("@email", $"'{email}'");

            var request = RequestMessage.Build(Tokens.OpsEval)
                                  .AddArgument(Tokens.ArgsGremlin, query)
                                  .AddArgument(Tokens.ArgsEvalTimeout, 500)
                                  .Create();

            var result = await _graphContext.GremlinClient.SubmitAsync<dynamic>(request);

            return result.First() > 0 ? true : false;   
        }
    }
}