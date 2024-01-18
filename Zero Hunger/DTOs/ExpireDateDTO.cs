using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger.DTOs
{
    public class ExpireDateDTO : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime ExpireDate)
            {
                if (ExpireDate >= DateTime.Now)
                {
                    return true;
                }
            }

            return false;
        }
    }
}