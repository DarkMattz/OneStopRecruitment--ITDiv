﻿using Microsoft.AspNetCore.Mvc;
using Model.Subdomains.DashboardSubdomain.Trainer;
using OneStopRecruitment.Areas.DashboardArea.ViewModels.Trainer;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.HttpExtensions;
using Service.Modules.DashboardModule;

namespace OneStopRecruitment.Areas.DashboardArea.Controllers
{
    [Area("DashboardArea")]
    public class TrainerController : BaseController
    {
        private readonly ITrainerService trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }

        public IActionResult Index()
        {
            DashboardTrainerViewModel viewModel = new DashboardTrainerViewModel();
            Login login = HttpContext.Session.GetLoggedinUser();

            Dashboard dashboard = new Dashboard();
            dashboard.CurrentPeriod = trainerService.GetCurrentPeriod();
            dashboard.CurrentStage = trainerService.GetCurrentStage();
            dashboard.NumberOfCandidates = trainerService.CountCandidateByPeriod().ToString();
            viewModel.Dashboard = dashboard;

            if(dashboard.CurrentStage.StageLevel != 4)
            {
                viewModel.TrainerAssignmentSchedule = trainerService.GetTrainerAssignmentSchedule(login.IDUser);
                viewModel.AvailableAssignmentSchedule = trainerService.GetAvailableAssignmentSchedule();
            }
            else
            {
                viewModel.CandidateList = trainerService.GetTodayInterview();
            }            

            return View(viewModel);
        }
    }
}
