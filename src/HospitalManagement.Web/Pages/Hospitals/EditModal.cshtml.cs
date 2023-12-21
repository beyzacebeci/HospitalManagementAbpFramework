using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalManagement.Hospitals;
using HospitalManagement.Departments;
using HospitalManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace HospitalManagement.Web.Pages.Hospitals
{
    public class EditModalModel : HospitalManagementPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateHospitalDto EditingHospital { get; set; }

        [BindProperty]
        public List<DepartmentViewModel> Departments { get; set; }


        private readonly IHospitalAppService _hospitalAppService;

        public EditModalModel(IHospitalAppService hospitalAppService)
        {
            _hospitalAppService = hospitalAppService;
        }

        public async Task OnGetAsync()
        {
            var hospitalDto = await _hospitalAppService.GetAsync(Id);
            EditingHospital = ObjectMapper.Map<HospitalDto, CreateUpdateHospitalDto>(hospitalDto);

            //get all departments
            var departmentLookupDto = await _hospitalAppService.GetDepartmentLookupAsync();
            Departments = ObjectMapper.Map<List<DepartmentLookupDto>, List<DepartmentViewModel>>(departmentLookupDto.Items.ToList());

            //mark as Selected for Departments in the hospital
            if (EditingHospital.DepartmentNames != null && EditingHospital.DepartmentNames.Any())
            {
                Departments
                    .Where(x => EditingHospital.DepartmentNames.Contains(x.Name))
                    .ToList()
                    .ForEach(x => x.IsSelected = true);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //ValidateModel();

            var selectedDepartments = Departments.Where(x => x.IsSelected).ToList();
            if (selectedDepartments.Any())
            {
                var departmentNames = selectedDepartments.Select(x => x.Name).ToArray();
                EditingHospital.DepartmentNames = departmentNames;
            }

            await _hospitalAppService.UpdateAsync(Id, EditingHospital);
            return NoContent();
        }

    }
}