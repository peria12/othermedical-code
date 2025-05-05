namespace Aurora.AdministrationService.API.Services.IService.AzureBus
{
    public interface IAzureBusCommandServices
    {
        Task<GenericResponse<string>> SendData<TPayload>(TPayload payload, string regionCode, string topicName) where TPayload : class;
    }
}
