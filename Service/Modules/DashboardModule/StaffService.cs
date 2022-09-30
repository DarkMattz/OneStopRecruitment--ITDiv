using Model.Subdomains.DashboardSubdomain.Staff;
using Repository.Repositories.OneStopRecruitmentRepository;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.DashboardModule
{
    public interface IStaffService
    {
        Period GetCurrentPeriod();
        Stage GetCurrentStage(int IDStage);
        int CountCandidateByPeriod(int IDPeriod);
        List<MasterSchedule> GetUpcomingSchedule();
    }
    public class StaffService : IStaffService
    {
        private readonly IPeriodRepository periodRepository;
        private readonly IStageRepository stageRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IMasterScheduleRepository masterScheduleRepository;

        public StaffService(
            IPeriodRepository periodRepository,
            IStageRepository stageRepository,
            ICandidateRepository candidateRepository,
            IMasterScheduleRepository masterScheduleRepository
        )
        {
            this.periodRepository = periodRepository;
            this.stageRepository = stageRepository;
            this.candidateRepository = candidateRepository;
            this.masterScheduleRepository = masterScheduleRepository;
        }

        public Period GetCurrentPeriod()
        {
            var periodDTO = periodRepository.GetActivePeriod();

            if (periodDTO != null)
            {
                Period period = new Period();
                period.IDPeriod = periodDTO.IDPeriod;
                period.IDStage = periodDTO.IDStage;
                period.PeriodName = periodDTO.PeriodName;
                period.IsActive = periodDTO.IsActive;
                period.DeadlineStart = periodDTO.DeadlineStart;
                period.DeadlineEnd = periodDTO.DeadlineEnd;

                return period;
            }

            return null;
        }

        public Stage GetCurrentStage(int IDStage)
        {
            var stageDTO = stageRepository.GetStageById(IDStage);

            if (stageDTO != null)
            {
                Stage stage = new Stage();
                stage.IDStage = IDStage;
                stage.StageName = stageDTO.StageName;
                stage.StageLevel = stageDTO.StageLevel;

                return stage;
            }

            return null;
        }

        public int CountCandidateByPeriod(int IDPeriod)
        {
            return candidateRepository.GetCandidateByPeriod(IDPeriod).Count();
        }

        public List<MasterSchedule> GetUpcomingSchedule()
        {
            return masterScheduleRepository.GetUpcomingSchedule();
        }
    }
}
