using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperMVC.Data.Models.DbModel
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
