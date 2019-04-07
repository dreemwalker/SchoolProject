using System;
using System.Collections.Generic;

namespace SchoolClient.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            ClassTeacher = new HashSet<ClassTeacher>();
        }

        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Position { get; set; }

        public virtual ICollection<ClassTeacher> ClassTeacher { get; set; }
    }
}
