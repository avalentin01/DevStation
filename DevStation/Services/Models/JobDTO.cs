﻿using DevStation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevStation.Services.Models
{
    public class JobDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EmployerDTO Employer { get; set; }
        public bool Active { get; set; }
        public string Img { get; set; }
    }
}