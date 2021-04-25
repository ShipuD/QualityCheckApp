using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QualityCheckApp
{
    interface IValidation
    {
        public List<string> GetValidationErrors(DataTable dt);
        public List<string> CheckForDuplicates(DataTable dt);
    }
}
