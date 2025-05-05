using Aurora.AdministrationService.API.Services.IService.AzureBus;
using Aurora.AdministrationService.CrossCutting.Helper;
using Azure.Messaging.ServiceBus;

namespace Aurora.AdministrationService.AzureBus
{
    public class AzureBusCommandServices : IAzureBusCommandServices
    {
        private readonly IConfiguration _configuration;

        public AzureBusCommandServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GenericResponse<string>> SendData<TPayload>(TPayload payload,string regionCode, string topicName) where TPayload : class
        {
            var connectionString = _configuration[ServiceBusConnectionString];

            //Setting get from Destinations App 
            string payloadstr = SerializeHelper.Serialize(payload);
           
            try
            {
                await using var client = new ServiceBusClient(connectionString);
                ServiceBusSender sender = client.CreateSender(topicName);

                // Create a message
                ServiceBusMessage message = new ServiceBusMessage(payloadstr);
                message.ApplicationProperties.Add(CommonConstants.ServiceBusHeaderKey, regionCode);
                // Send the message
                await sender.SendMessageAsync(message);
                return new GenericResponse<string>
                {
                    IsSuccess = true,
                    HasError = false,
                };
            }
            catch (Exception)
            {
                return new GenericResponse<string>
                {
                    HasError = true,
                    IsSuccess = false,
                };
            }
        }
    }
}
