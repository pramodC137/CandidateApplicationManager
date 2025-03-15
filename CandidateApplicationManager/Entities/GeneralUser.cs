namespace CandidateApplicationManager.Entities
{
    public class GeneralUser
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get;set; }
        public DateTime? LastModifiedOn { get; private set; }
        public DateTime? LastLogon { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
