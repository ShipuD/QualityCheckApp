using QualityCheckApp.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace QualityCheckApp
{
    class Program
    {
        //const string dataFileName = @"G:\Work\QualityCheckApp\Data\data-clean.csv"; 
        
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a full file path");
            string dataFileName = Console.ReadLine();

            List<string> errors = new List<string>();
            
            CSVReader csvReader = new CSVReader();
            DataTable dt = csvReader.ReadFile(dataFileName, true);
            Validation validation = new Validation();

            errors = validation.GetValidationErrors(dt);

            if (errors.Count <= 0)
            {
                Console.WriteLine("Validation complete.");
            }
            else
            {
                Console.WriteLine(string.Format("Validation failed - {0} errors found", errors.Count));
            }
            
        }
    }
}
