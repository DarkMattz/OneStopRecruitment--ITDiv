using System;

namespace Model.Subdomains.DashboardSubdomain.Staff
{
    public class MasterSchedule
    {
        public Guid IDSchedule { get; set; }
        public int IDPeriod { get; set; }
        public int IDStage { get; set; }
        public string StageName { get; set; }
        public int IDSubStage { get; set; }
        public string SubStageName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Room { get; set; }
    }
}
