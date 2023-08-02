using CoreX.Base;
using MassTransit;

namespace Doit.AccountModule.Presentation.Consumer
{
    public class GlobalFaultConsumer : IConsumer<DevError>
    {
        public async Task Consume(ConsumeContext<DevError> context)
        {
            throw new ErrorException(context.Message);
    
        }
    }
}




