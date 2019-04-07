using System;
using System.Collections.Generic;

namespace SchoolClient.Models
{
    public partial class ClassTeacher
    {
        public int Id { get; set; }
        public int TeacherClassId { get; set; }
        public int ClassTeacherId { get; set; }

        public virtual Class ClassTeacherNavigation { get; set; }
        public virtual Teacher TeacherClass { get; set; }
    }
}
