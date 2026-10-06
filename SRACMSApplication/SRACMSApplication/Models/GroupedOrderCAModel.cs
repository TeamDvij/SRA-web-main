using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRACMSApplication.Models
{
    public class GroupedOrderCAModel
    {
        public int CA1_3C_1 { get; set; }
        public int CA1_3C_2 { get; set; }

        public int CA1_3D_1 { get; set; }
        public int CA1_3D_2 { get; set; }

        public int CA1_yadi_1 { get; set; }
        public int CA1_yadi_2 { get; set; }

        public int CA2_3C_1 { get; set; }
        public int CA2_3C_2 { get; set; }

        public int CA2_3D_1 { get; set; }
        public int CA2_3D_2 { get; set; }
        public int CA2_yadi_1 { get; set; }
        public int CA2_yadi_2 { get; set; }
        public int CA3_3C_1 { get; set; }
        public int CA3_3C_2 { get; set; }

        public int CA3_3D_1 { get; set; }
        public int CA3_3D_2 { get; set; }
        public int CA3_yadi_1 { get; set; }
        public int CA3_yadi_2 { get; set; }


        //तांत्रिक Technical
        public int tech1_permission_1 { get; set; }
        public int tech1_permission_2 { get; set; }
        public int tech1_cc { get; set; }
        public int tech1_occup { get; set; }

        public int tech2_permission_1 { get; set; }
        public int tech2_permission_2 { get; set; }
        public int tech2_cc { get; set; }
        public int tech2_occup { get; set; }

        public int tech3_permission_1 { get; set; }
        public int tech3_permission_2 { get; set; }
        public int tech3_cc { get; set; }
        public int tech3_occup { get; set; }

    }
}