﻿using HopeLine.DataAccess.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : Add props
    public class Specialization : BaseEntity
    {

        public Specialization()
        {
            Topics = new List<Topic>();
            MentorSpecializations = new List<MentorSpecialization>();
        }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public ICollection<Topic> Topics { get; set; }

        public ICollection<MentorSpecialization> MentorSpecializations { get; set; }
    }

}
