﻿using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CoursesClassListDto:IDto
    {
        public int ID { get; set; }
        public string ClassName { get; set; }
        public string CoursesName { get; set; }
        
        public int CoursesCredi { get; set; }
    }
}