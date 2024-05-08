namespace Application.Services.Users.Dtos
{
    public class UserSearchDto
    {
        public Guid? CurrentUserId { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? InsertTimeFrom { get; set; }
        public DateTime? InsertTimeTo { get; set; }
        public bool? ShowAdmins { get; set; }
    }
}