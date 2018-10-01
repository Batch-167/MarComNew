using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class ResultResponse
    {
        public ResultResponse()
        {
            Success = true;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Reference { get; set; }
        public object Entity { get; set; }
    }
}
