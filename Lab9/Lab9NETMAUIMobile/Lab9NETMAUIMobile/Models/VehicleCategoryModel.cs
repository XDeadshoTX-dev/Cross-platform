using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9NETMAUIMobile.Models
{
    public class GetVehicleCategoryModel
    {
        public string vehicle_category_code { get; set; }
    }

    public class GetVehicleCategoryResponse
    {
        public string vehicle_category_description { get; set; }
    }
    public class SendVehicleCategoryModel
    {
        public string vehicle_category_code { get; set; }
        public string vehicle_category_description { get; set; }
    }
}
