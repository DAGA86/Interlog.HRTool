using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Interlog.HRTool.WebApp.Models.Employee
{
    public class EmployeeEditViewModel
    {
        public EmployeeEditViewModel()
        {

        }

        public EmployeeEditViewModel(int id, string firstName, string lastName, int departmentId)
        {
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

        public SelectList? Department { get; set; }
        public MultiSelectList? Profiles { get; set; }
    }
}
