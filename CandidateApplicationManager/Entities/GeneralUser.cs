using Newtonsoft.Json;

namespace CandidateApplicationManager.Entities
{
    public class GeneralUser
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("userName")]
        public string? UserName { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }

        [JsonProperty("createdOn")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("updatedOn")]
        public DateTime? UpdatedOn { get;set; }

        [JsonProperty("modifiedOn")]
        public DateTime? ModifiedOn { get; private set; }

        [JsonProperty("lastLogon")]
        public DateTime? LastLogon { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
