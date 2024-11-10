using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9NETMAUIMobile.Models
{
    public class GetModelInformationModel
    {
        public string model_code { get; set; }
    }

    public class GetModelInformationResponse
    {
        public string model_name { get; set; }
        public decimal daily_hire_rate { get; set; }
    }
    public class SendModelInformation
    {
        public string model_code { get; set; }
        public decimal daily_hire_rate { get; set; }
        public string model_name { get; set; }
    }
}
