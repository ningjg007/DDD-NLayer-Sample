using System;

namespace NLayer.Application.UserSystemModule.DTOs
{
    public class PermissionDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string ActionUrl { get; set; }

        public int SortOrder { get; set; }

        public DateTime Created { get; set; }
    }
}
