using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Web.Models
{
    public class DepartmentViewModel
    {
        [HiddenInput] 
        public Guid Id{get;set;}
        
        public bool IsSelected{get;set;}
       
        [Required]
        [HiddenInput]
        public string Name { get; set; }
    }
}
