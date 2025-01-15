using GoalManageService.Services;
using GoalManageService.Models;

namespace GoalManagerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class TaskServiceTests
        {
            private GoalManager _service;

            [TestInitialize]
            public void Setup()
            {
                _service = new GoalManager();
            }

            [TestMethod]
            public void AddTask_ShouldAddTaskSuccessfully()
            {
                var goal = _service.AddGoal("Test Task", new DateTime(2000, 11, 12), Priority.Normal);

                Assert.IsNotNull(goal);
                Assert.AreEqual("Test Task", goal.Title);
                Assert.AreEqual(Priority.Normal, goal.Priority);
                Assert.AreEqual(new DateTime(2000, 11, 12), goal.EndDate);
                Assert.AreEqual(GoalStatus.InProgress, goal.Status);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void AddTask_ShouldThrowException_WhenTitleIsEmpty()
            {
                _service.AddGoal("", new DateTime(2000, 11, 12), Priority.Normal);
            }

            [TestMethod]
            public void RemoveTask_ShouldReturnTrue_WhenTaskExists()
            {
                var goal = _service.AddGoal("Test Task", new DateTime(2000, 11, 12), Priority.Normal);

                var result = _service.RemoveGoal(goal.Id);

                Assert.IsTrue(result);
                Assert.AreEqual(0, _service.GetAllGoals().Count);
            }

            [TestMethod]
            public void RemoveTask_ShouldReturnFalse_WhenTaskDoesNotExist()
            {
                var result = _service.RemoveGoal(new Guid());

                Assert.IsFalse(result);
            }

            [TestMethod]
            public void UpdateTask_ShouldUpdateTaskSuccessfullyWithTitle()
            {
                var goal = _service.AddGoal("Old Title", new DateTime(2000, 11, 12), Priority.Medium);

                var result = _service.UpdateGoal(goal.Id, "New Title", null, null, null);

                Assert.IsTrue(result);
                Assert.AreEqual("New Title", goal.Title);
            }

            [TestMethod]
            public void UpdateTask_ShouldUpdateTaskSuccessfullyWithDateTime()
            {
                var goal = _service.AddGoal("Old Title", new DateTime(2000, 11, 12), Priority.Medium);

                var result = _service.UpdateGoal(goal.Id, "New Title", new DateTime(2016, 11, 12), null, null);

                Assert.IsTrue(result);
                Assert.AreEqual("New Title", goal.Title);
                Assert.AreEqual(new DateTime(2016, 11, 12), goal.EndDate);
            }

            [TestMethod]
            public void UpdateTask_ShouldUpdateTaskSuccessfullyWithStatus()
            {
                var goal = _service.AddGoal("Old Title", new DateTime(2000, 11, 12), Priority.Medium);

                var result = _service.UpdateGoal(goal.Id, "New Title", new DateTime(2016, 11, 12), GoalStatus.Success, null);

                Assert.IsTrue(result);
                Assert.AreEqual("New Title", goal.Title);
                Assert.AreEqual(new DateTime(2016, 11, 12), goal.EndDate);
                Assert.AreEqual(GoalStatus.Success, goal.Status);
            }

            [TestMethod]
            public void UpdateTask_ShouldUpdateTaskSuccessfullyWithPriority()
            {
                var goal = _service.AddGoal("Old Title", new DateTime(2000, 11, 12), Priority.Medium);

                var result = _service.UpdateGoal(goal.Id, "New Title", new DateTime(2016, 11, 12), GoalStatus.Success, Priority.High);

                Assert.IsTrue(result);
                Assert.AreEqual("New Title", goal.Title);
                Assert.AreEqual(new DateTime(2016, 11, 12), goal.EndDate);
                Assert.AreEqual(GoalStatus.Success, goal.Status);
                Assert.AreEqual(Priority.High, goal.Priority);
            }

            [TestMethod]
            public void GetAllTasks_ShouldReturnAllTasks()
            {
                _service.AddGoal("Task 1", new DateTime(2000, 11, 12), Priority.Normal);
                _service.AddGoal("Task 2", new DateTime(2000, 11, 12), Priority.Normal);

                var goal = _service.GetAllGoals();

                Assert.AreEqual(2, goal.Count);
            }

            [TestMethod]
            public void GetTaskById_ShouldReturnCorrectTask()
            {
                var goal = _service.AddGoal("Test Task", new DateTime(2015, 09, 16), Priority.High);

                var status = _service.TryGetGoalById(goal.Id, out Goal? go);

                Assert.IsNotNull(go);
                Assert.IsTrue(status);
            }
        }
    }
}