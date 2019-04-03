using System;
using System.Collections.Generic;

namespace SchoolProjectAPI.Models
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
        public int? TeacherClassId { get; set; }

        public virtual ICollection<ClassTeacher> ClassTeacher { get; set; }
    }
}
