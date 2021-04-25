using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QualityCheckApp
{
    class Validation:IValidation
    {
        string ColumnName { get; set; }
        string ValidationType { get; set; }
        string Pattern { get; set; }        
        bool Required { get; set; }

        public static Dictionary<string, Validation> dict = new Dictionary<string, Validation>() 
        {
           //Nan check pattern
            { ConstValidation.AGE, new Validation () { ColumnName = ConstValidation.AGE,ValidationType=ConstValidation.Nan, Pattern = @"^\d+",  Required = true}},             
            //Check for MM/dd/YYYY format
            { ConstValidation.SIGNUPDATE, new Validation() { ColumnName = ConstValidation.SIGNUPDATE,ValidationType=ConstValidation.SIGNUPDATE, Pattern = @"^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4} (2[0-3]|[01]?[0-9]):([0-5]?[0-9]):([0-5]?[0-9])$", Required = true}},

            //account types of type ["google", "facebook", "other"]
            { ConstValidation.ACCOUNTTYPE, new Validation() { ColumnName = ConstValidation.GUID,ValidationType=ConstValidation.ACCOUNTTYPE, Pattern = @"/^(google|facebook|other)$/", Required = true}},
           
        };
        /// <summary>
        /// This validation will stop for first error
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<string> GetValidationErrors(DataTable dt)
        {
            List<string> errors = new List<string>();

            //1. Check for duplicates using Guid

            List<string> duplicates = CheckForDuplicates(dt);

            if (duplicates.Count() > 0)
            {
                errors.Add(string.Format("Row GUID:{0} is duplicated for : {1} times", String.Join(", ", duplicates.ToArray()), duplicates.Count()));
                return errors;
            }
            foreach (DataRow row in dt.AsEnumerable())
            {
                try
                {
                    //2. Check for all invidual validation on each column configure in teh dictionary
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (dict.ContainsKey(col.ColumnName))
                        {
                            Validation validation = dict[col.ColumnName];
                            object colValue = row.Field<object>(col.ColumnName);


                            if (validation.Required)
                            {
                                if (colValue == null)
                                {
                                    errors.Add(string.Format("Row GUID:{0},Field:{1},Error {2}", row.Field<string>(ConstValidation.GUID), col.ColumnName, validation.ValidationType));
                                    break;
                                }
                            }
                            //
                            string colValueStr = colValue.ToString();
                            Match match = Regex.Match(colValueStr, validation.Pattern);
                            if (!match.Success)
                            {
                                errors.Add(string.Format("Row GUID:{0},Field:{1},Error {2}", row.Field<string>(ConstValidation.GUID), col.ColumnName, validation.ValidationType));
                                break;
                            }
                        }
                    }
                   
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    break;
                }
            }
            return errors;
        }
        
        public List<string> CheckForDuplicates(DataTable dt)
        {   
            var duplicates = dt.AsEnumerable()
                   .Select(dr => dr.Field<string>(ConstValidation.GUID))
                   .GroupBy(x => x)
                   .Where(g => g.Count() > 1)
                   .SelectMany(g => g)
                   .ToList();

            return duplicates;
        }
    }
}
