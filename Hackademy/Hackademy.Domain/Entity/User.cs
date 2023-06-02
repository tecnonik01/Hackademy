﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackademy.Domain.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string City { get; set; }

        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public double Point { get; set; }
        public Guid Image { get; set; }
        public string Bio { get; set; }
        public IList<UserCourse> UserCourses { get; set; }
        public IList<Team> Teams { get; set; }
    }
}
