using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementTN.Models
{
    public class ClassDivision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassDivision()
        {
            this.Teachers = new HashSet<Teacher>();
            this.Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Classno { get; set; }
        public string Division { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Teacher> Teachers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }

    }
}