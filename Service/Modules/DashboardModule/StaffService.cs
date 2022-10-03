using Model.DTO.OneStopRecruitmentDTO;
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
        List<BlastEmail> GetNotifications();
    }
    public class StaffService : IStaffService
    {
        private readonly IPeriodRepository periodRepository;
        private readonly IStageRepository stageRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IMasterScheduleRepository masterScheduleRepository;
        private readonly IBlastEmailRepository blastEmailRepository;

        public StaffService(
            IPeriodRepository periodRepository,
            IStageRepository stageRepository,
            ICandidateRepository candidateRepository,
            IMasterScheduleRepository masterScheduleRepository,
            IBlastEmailRepository blastEmailRepository
        )
        {
            this.periodRepository = periodRepository;
            this.stageRepository = stageRepository;
            this.candidateRepository = candidateRepository;
            this.masterScheduleRepository = masterScheduleRepository;
            this.blastEmailRepository = blastEmailRepository;
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

        public List<BlastEmail> GetNotifications()
        {
            List<BlastEmail> result = new List<BlastEmail>();

            PeriodDTO period = periodRepository.GetActivePeriod();
            List<BlastEmailDTO> blastEmailDTOs = blastEmailRepository.GetByIDPeriod(period != null ? period.IDPeriod : 0).ToList();

            foreach (var item in blastEmailDTOs)
            {
                BlastEmail blastEmail = new BlastEmail()
                {
                    IDBlastEmail = item.IDBlastEmail,
                    IDPeriod = item.IDPeriod,
                    Subject = item.Subject,
                    Description = item.Description,
                    BlastDateTime = item.BlastDateTime
                };
                result.Add(blastEmail);
            }

            result = result.OrderByDescending(x => x.BlastDateTime).ToList();

            return result;
        }
    }
}
