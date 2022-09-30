using Microsoft.AspNetCore.Mvc;
using Model.Subdomains.DashboardSubdomain.Staff;
using OneStopRecruitment.Areas.DashboardArea.ViewModels.Staff;
using OneStopRecruitment.Controllers;
using Service.Modules.DashboardModule;

namespace OneStopRecruitment.Areas.DashboardArea.Controllers
{
    [Area("DashboardArea")]
    public class StaffController : BaseController
    {
        private readonly IStaffService staffService;
        public StaffController(IStaffService staffService)
        {
            this.staffService = staffService;
        }

        public IActionResult Index()
        {
            DashboardStaffViewModel dashboardStaffViewModel = new DashboardStaffViewModel();

            var currentPeriod = staffService.GetCurrentPeriod();

            Stage currentStage = new Stage();
            int numberCandidate = 0;
            if (currentPeriod != null)
            {
                currentStage = staffService.GetCurrentStage(currentPeriod.IDStage);
                numberCandidate = staffService.CountCandidateByPeriod(currentPeriod.IDPeriod);
            }

            string currentPeriodName = currentPeriod != null ? currentPeriod.PeriodName : "-";
            string currentStageName = currentStage != null ? currentStage.StageName : "-";
            string numberOfCandidates = numberCandidate.ToString();

            DashboardStaff dashboardStaff = new DashboardStaff();
            dashboardStaff.CurrentPeriod = currentPeriodName;
            dashboardStaff.CurrentStage = currentStageName;
            dashboardStaff.NumberOfCandidates = numberOfCandidates;
            dashboardStaff.UpcomingSchedules = staffService.GetUpcomingSchedule();

            dashboardStaffViewModel.DashboardStaff = dashboardStaff;

            return View(dashboardStaffViewModel);
        }
    }
}
