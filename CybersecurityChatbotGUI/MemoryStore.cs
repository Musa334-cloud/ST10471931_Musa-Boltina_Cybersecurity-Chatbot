using System;
using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    class MemoryStore
    {
        public string UserName { get; set; } = string.Empty;
        public string FavouriteTopic { get; set; } = string.Empty;

        private Dictionary<string, string> _store = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public void Store(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(key))
                _store[key] = value;
        }

        public string Recall(string key)
        {
            return _store.TryGetValue(key, out string val) ? val : null;
        }

        public string GetPersonalisedOpener()
        {
            if (!string.IsNullOrWhiteSpace(FavouriteTopic) && !string.IsNullOrWhiteSpace(UserName))
                return $"As someone interested in {FavouriteTopic}, {UserName}, here's something relevant: ";
            if (!string.IsNullOrWhiteSpace(FavouriteTopic))
                return $"As someone interested in {FavouriteTopic}, here's something relevant: ";
            if (!string.IsNullOrWhiteSpace(UserName))
                return $"Great question, {UserName}! ";
            return string.Empty;
        }
    }
}