using Model.Subdomains.DashboardSubdomain.Staff;
using System.Collections.Generic;

namespace OneStopRecruitment.Areas.DashboardArea.ViewModels.Staff
{
    public class DashboardStaffViewModel
    {
        public List<BlastEmail> BlastEmailList { get; set; }
        public DashboardStaff DashboardStaff { get; set; }
    }
}
