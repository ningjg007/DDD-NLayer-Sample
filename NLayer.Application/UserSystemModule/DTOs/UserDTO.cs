using System;

namespace NLayer.Application.UserSystemModule.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LoginName { get; set; }

        public string LoginPwd { get; set; }

        public string Email { get; set; }

        public DateTime Created { get; set; }

        public string LastLoginToken { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
