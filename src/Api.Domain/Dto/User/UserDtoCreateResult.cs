﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Domain.Dto.User
{
    public class UserDtoCreateResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateCreate { get; set; }

    }
}
