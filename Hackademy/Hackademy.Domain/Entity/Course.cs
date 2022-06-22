﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackademy.Domain.Entity
{
    public class Course
    {

        public int CourseId { get; set; } 
        public string CourseName { get; set; }  
        public string Description { get; set; } 
        public DateTime StartDate { get; set; }  
        public DateTime EndDate { get; set; }
        public int MaxCapacity { get; set;  }
        public IList<UserCourse> UserCourses { get; set; }
    }
}
