using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    class TaskManager
    {
        private TaskStorageHelper _storage;
        private ActivityLogger _logger;

        public TaskManager(ActivityLogger logger)
        {
            _storage = new TaskStorageHelper();
            _logger = logger;
        }

        // Add a new task and log it
        public string AddTask(string title, string description, string reminder)
        {
            _storage.AddTask(title, description, reminder);

            string logEntry = "Task added: '" + title + "'";
            if (!string.IsNullOrWhiteSpace(reminder))
                logEntry += " (Reminder: " + reminder + ")";

            _logger.Log(logEntry);

            string response = "✅ Task added: '" + title + "'\n" + description;
            if (!string.IsNullOrWhiteSpace(reminder))
                response += "\n⏰ Reminder set: " + reminder;

            return response;
        }

        // Get all tasks
        public List<CyberTask> GetAllTasks()
        {
            return _storage.LoadTasks();
        }

        // Mark a task as complete
        public void MarkAsComplete(int id)
        {
            var tasks = _storage.LoadTasks();
            foreach (var task in tasks)
            {
                if (task.Id == id)
                {
                    _storage.MarkAsComplete(id);
                    _logger.Log("Task marked complete: '" + task.Title + "'");
                    break;
                }
            }
        }

        // Delete a task
        public void DeleteTask(int id)
        {
            var tasks = _storage.LoadTasks();
            foreach (var task in tasks)
            {
                if (task.Id == id)
                {
                    _storage.DeleteTask(id);
                    _logger.Log("Task deleted: '" + task.Title + "'");
                    break;
                }
            }
        }
    }
}