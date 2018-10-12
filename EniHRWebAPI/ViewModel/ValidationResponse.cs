using System;
using System.Collections.Generic;

namespace EniHRWebAPI.ViewModel
{
    public class ValidationResponse
    {
        public int rowIndex { get; set; }
        public bool isChanged { get; set; }
        public bool isNew { get; set; }
        public List<ChangeColumn> changedColumns;
    }

    public class ChangeColumn
    {
        public int colIndex { get; set; }
        public string currentValue { get; set; }
        public string newValue { get; set; }
    }
}
