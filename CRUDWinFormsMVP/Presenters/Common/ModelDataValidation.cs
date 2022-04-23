﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUDWinFormsMVP.Presenters.Common
{
    public abstract class ModelDataValidation
    {
        public static void Validate(object model)
        {
            string errorMessage = "";
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(model);
            bool isValid = Validator.TryValidateObject(model, context, results);

            if (!isValid)
            {
                foreach (var item in results)
                    errorMessage += " - " + item.ErrorMessage + "\n";
                throw new Exception(errorMessage);
            }
        }
    }
}
