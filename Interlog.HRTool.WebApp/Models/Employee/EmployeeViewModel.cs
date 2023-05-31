using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.WebApp.Models.Employee
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {

        }

        public EmployeeViewModel(int id, string firstName, string lastName, int departmentId) {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DepartmentId = departmentId;
        }

        public int Id { get; set; }
        public string? FirstName { get; }
        public string? LastName { get; }


        public int DepartmentId { get; set; }

        public int[] ProfileIds { get; set; }
    }
}
