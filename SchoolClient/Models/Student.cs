using System;
using System.Collections.Generic;

namespace SchoolClient.Models
{
    public partial class Student
    {
        public Student()
        {
            Perfects = new HashSet<Perfect>();
          
        }

        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public int? ClassId { get; set; }

        public virtual Class Class { get; set; }
        public virtual ICollection<Perfect> Perfects { get; set; }
    }
}
