using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interlog.HRTool.Data.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [StringLength(32)]
        public string FirstName { get; set; }
        [StringLength(32)]
        public string LastName { get; set; }
        [StringLength(64)]
        public string UserName { get; set; }
        [StringLength(320)]
        public string Email { get; set; }
        [StringLength(128)]
        public string Password { get; set; }

        public int DepartmentId { get; set; }


        public ICollection<Profile>? Profiles { get; set; }

        public Department Department { get; set; }


    }
}
