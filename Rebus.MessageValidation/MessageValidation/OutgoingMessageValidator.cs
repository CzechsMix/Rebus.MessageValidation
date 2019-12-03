using FluentValidation;
using FluentValidation.Results;
using Rebus.Messages;
using Rebus.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebus.MessageValidation
{
    class OutgoingMessageValidator : IOutgoingStep
    {
        private Dictionary<Type, IEnumerable<AbstractValidator<object>>> _validators;
        private readonly Func<IMessageContext, object, ValidationResult, Task> _invalidSendHandler;

        public OutgoingMessageValidator(
            Dictionary<Type, IEnumerable<AbstractValidator<object>>> validators,
            Func<IMessageContext, object, ValidationResult, Task> invalidSendHandler)
        {
            _validators = validators;
            _invalidSendHandler = invalidSendHandler;
        }

        public async Task Process(OutgoingStepContext context, Func<Task> next)
        {
            var message = context.Load<Message>();
            var validators = _validators[message.Body.GetType()];


            if (validators != null)
            {
                foreach (var validator in validators)
                {
                    var result = validator.Validate(message.Body);
                    if (result.IsValid) continue;

                    await _invalidSendHandler(MessageContext.Current, message.Body, result);
                }
            }

            await next();
        }
    }
}
