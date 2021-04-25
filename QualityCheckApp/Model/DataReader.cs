using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QualityCheckApp.Model
{
    public abstract class  DataReader
    {
        DataTable _dataTable;
        Dictionary<string, Validation> _dictValidation;
        public virtual void ParseData()
        {

        }
        public virtual DataTable ReadFile(string fullPath, bool headerRow)
        {
            return _dataTable;
        }
        public virtual DataTable BuildValidationList(List<string> headerList, bool headerRow)
        {
            return _dataTable;
        }        
    }

}
