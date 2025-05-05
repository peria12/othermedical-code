using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Aurora.AdministrationService.API.Models.RequestModels.Leaves
{
    public class ScheduleLeavesRequestDto
    {
        [JsonProperty(nameof(Action))]
        [MaxLength(20, ErrorMessage = AppConstants.Maximum20Character)]
        public string Action { get; set; } = null!;
        [JsonProperty(nameof(Type))]
        public string Type { get; set; } = null!;
        [JsonProperty(nameof(practitionerId))]
        public long practitionerId { get; set; }
        [JsonProperty(nameof(LocationResourceId))]
        public  string LocationResourceId { get; set; } = null!;
        [JsonProperty(nameof(newTimeOff))]
        public List<TimeOffAddRequestDto> newTimeOff { get; set; } = null!;
        [JsonProperty(nameof(removeTimeOff))]
        public List<TimeOffUpdateRequestDto> removeTimeOff { get; set; } = null!;
        [JsonProperty(nameof(NewOverrides))]
        public string NewOverrides { get; set; } = null!;
        [JsonProperty(nameof(RemovedOverrides))]
        public string RemovedOverrides { get; set; } = null!;
        [JsonProperty(nameof(PractitionerIdentifiers))]
        public List<PractitionerIdentifierRequestDto> PractitionerIdentifiers { get; set; }=null!;
    }
}
