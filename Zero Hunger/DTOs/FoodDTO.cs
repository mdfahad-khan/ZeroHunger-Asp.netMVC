using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger.DTOs
{
    public class FoodDTO: ExpireDateDTO
    {


        [Required(ErrorMessage = "Food name is required")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name cannot contain numbers or special characters")]
        public string F_name { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Quantity must be a positive integer")]
        public int F_quantity { get; set; }

         [Required(ErrorMessage = "Date of Birth is required")]
         

        public DateTime ExpireDate { get; set; }
        public int? AssignedEmployeeId { get; set; }
    }
}