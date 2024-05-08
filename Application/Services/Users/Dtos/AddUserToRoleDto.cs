namespace Application.Services.Users.Dtos
{
    public class AddUserToRoleDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}