using HospitalManagement.Departments;
using HospitalManagement.Hospitals;
using HospitalManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Web.Pages.Hospitals
{
    public class CreateModalModel : HospitalManagementPageModel
    {
        [BindProperty]
        public CreateUpdateHospitalDto Hospital { get; set; }

        [BindProperty]
        public List<DepartmentViewModel> Departments { get; set; }

        private readonly IHospitalAppService _hospitalAppService;

        public CreateModalModel(IHospitalAppService hospitalAppService)
        {
            _hospitalAppService = hospitalAppService;
        }

        public async Task OnGetAsync()
        {
            Hospital = new CreateUpdateHospitalDto();

            //Get all categories
            var departmentLookupDto = await _hospitalAppService.GetDepartmentLookupAsync();
            Departments = ObjectMapper.Map<List<DepartmentLookupDto>, List<DepartmentViewModel>>(departmentLookupDto.Items.ToList());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var selectedDepartments = Departments.Where(x => x.IsSelected).ToList();
            if (selectedDepartments.Any())
            {
                var departmentNames = selectedDepartments.Select(x => x.Name).ToArray();
                Hospital.DepartmentNames = departmentNames;
            }

            await _hospitalAppService.CreateAsync(Hospital);
            return NoContent();
        }
    }
}
