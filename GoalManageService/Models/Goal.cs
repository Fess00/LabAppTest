namespace GoalManageService.Models
{
    public class Goal
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public GoalStatus Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
