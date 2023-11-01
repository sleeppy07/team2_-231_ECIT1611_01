using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class SubjectDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public AppUser? User { get; set; }
        public Guid? SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
