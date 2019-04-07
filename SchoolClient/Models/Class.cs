using System;
using System.Collections.Generic;

namespace SchoolClient.Models
{
    public partial class Class
    {
        public Class()
        {
            ClassTeacher = new HashSet<ClassTeacher>();
            Perfects = new HashSet<Perfect>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ClassTeacher> ClassTeacher { get; set; }
        public virtual ICollection<Perfect> Perfects { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
