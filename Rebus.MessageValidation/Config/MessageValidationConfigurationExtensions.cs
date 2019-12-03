using System;
using System.Threading.Tasks;
using Rebus.Pipeline;
using FluentValidation.Results;

namespace Rebus.Config
{
    public static class RebusMessageValidationConfigurationExtensions
    {
        public static void EnableMessageValidation(this OptionsConfigurer configurer, Action<MessageValidationConfigurer> configure)
        {
            var validationConfig = new MessageValidationConfigurer();
            configure(validationConfig);
        }

        public class MessageValidationConfigurer
        {
            public MessageValidationConfigurer MapValidatorsFromAssemblyOf<TValidator>()
            {
                throw new NotImplementedException();
            }

            public MessageValidationConfigurer OnInvalidSend(Func<IMessageContext, object, ValidationResult, Task> handler)
            {
                throw new NotImplementedException();
            }

            public MessageValidationConfigurer OnInvalidReceive(Func<IMessageContext, object, ValidationResult, Task> handler)
            {
                throw new NotImplementedException();
            }
        }
    }
}
