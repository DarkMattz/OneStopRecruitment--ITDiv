using Microsoft.AspNetCore.Mvc;
using Model.DBConstraint;
using Model.Subdomains.DashboardSubdomain.Staff;
using OneStopRecruitment.Areas.DashboardArea.ViewModels.Staff;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.AuthenticationHelpers;
using OneStopRecruitment.Helpers.HttpExtensions;
using Service.Modules.DashboardModule;
using System;

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

            RoleAuthenticator.AuthenticateRoleArea(HttpContext.Session.GetLoggedinUser(), BaseConstraint.Role.Staff.Id);

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
            dashboardStaffViewModel.BlastEmailList = staffService.GetNotifications();

            return View(dashboardStaffViewModel);
        }
    }
}
