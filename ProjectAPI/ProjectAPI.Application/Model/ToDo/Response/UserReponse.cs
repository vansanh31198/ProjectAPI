﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Application.Model.ToDo.Response
{
    public class UserReponse
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
