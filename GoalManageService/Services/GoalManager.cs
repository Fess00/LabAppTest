using GoalManageService.Models;

namespace GoalManageService.Services
{
    public class GoalManager
    {
        private readonly List<Goal> _goals = new();

        public Goal AddGoal(string title, DateTime endDate, Priority priority)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            var goal = new Goal { Id = new Guid(), Title = title, Status = GoalStatus.InProgress, Priority = priority, CreateDate = DateTime.Now, EndDate = endDate };
            _goals.Add(goal);
            return goal;
        }

        public bool TryAddGoal(Goal goal)
        {
            if (goal is null)
                return false;

            _goals.Add(goal);
            return true;
        }

        public bool RemoveGoal(Guid id)
        {
            var goal = _goals.FirstOrDefault(t => t.Id == id);
            if (goal == null) return false;

            _goals.Remove(goal);
            return true;
        }

        public bool UpdateGoal(Guid id, string? newTitle = null, DateTime? newEndDate = null, GoalStatus? goalStatus = null, Priority? priority = null)
        {
            var goal = _goals.FirstOrDefault(t => t.Id == id);
            if (goal == null) return false;

            if (newTitle is not null)
                goal.Title = newTitle;

            if (newEndDate is not null)
                goal.EndDate = (DateTime)newEndDate;

            if (goalStatus is not null)
                goal.Status = (GoalStatus)goalStatus;

            if (priority is not null)
                goal.Priority = (Priority)priority;

            return true;
        }

        public List<Goal> GetAllGoals() => _goals;

        public bool TryGetGoalById(Guid id, out Goal? goal)
        {
            goal = _goals.FirstOrDefault(t => t.Id == id);
            return goal is null ? false : true;
        }
    }
}
