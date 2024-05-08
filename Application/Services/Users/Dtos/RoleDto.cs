﻿namespace Application.Services.Users.Dtos
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}