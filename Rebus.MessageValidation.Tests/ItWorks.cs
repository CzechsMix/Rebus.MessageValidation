using NUnit.Framework;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Tests.Contracts;
using Rebus.Transport.InMem;
using System.Threading.Tasks;

namespace Rebus.MessageValidation.Tests
{
    [TestFixture]
    public class ItWorks : FixtureBase
    {
        protected override void SetUp()
        {
            var activator = new BuiltinHandlerActivator();

            Using(activator);

            Configure.With(activator)
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "validation-check"))
                .Options(o => o.EnableMessageValidation(validationConfig =>
                {
                    validationConfig.MapValidatorsFromAssemblyOf<ItWorks>();
                    validationConfig.OnInvalidSend((context, message, validationResult) => Task.FromResult(true)); // net45 doesn't have Task.CompletedTask
                    validationConfig.OnInvalidReceive((context, message, validationResult) => Task.FromResult(true)); // see above
                }))
                .Start();
        }
    }
}