﻿using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RoleUserListeDto:IDto
    {
        public int ID { get; set; }
        public int roleID { get; set; }
        public int userID { get; set; }
    }
}
