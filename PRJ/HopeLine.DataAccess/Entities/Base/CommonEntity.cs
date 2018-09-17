﻿using System;

namespace HopeLine.DataAccess.Entities.Base
{
    public class CommonEntity : IEntity
    {
        public CommonEntity()
        {
            DateAdded = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
