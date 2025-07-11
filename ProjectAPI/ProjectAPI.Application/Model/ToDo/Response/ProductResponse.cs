﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Application.Model.ToDo.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public bool IsDeleted { get; set; }   
    }
}
