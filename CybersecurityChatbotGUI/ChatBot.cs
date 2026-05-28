using System;
using System.Collections.Generic;

namespace CybersecurityChatbotGUI
{
    class ChatBot
    {
        private KeywordResponder _keywords;
        private SentimentDetector _sentiment;
        private MemoryStore _memory;

        private bool _awaitingName = true;
        private string _lastTopic = null;

        private Random _random = new Random();

        private List<string> _fallbackResponses = new List<string>
        {
            "I'm not sure about that one. Try asking me about passwords, phishing, or malware!",
            "Hmm, I didn't quite get that. Type 'what can you do' to see my topics.",
            "I'm still learning! Could you rephrase that or ask about a cybersecurity topic?",
            "That's outside my expertise for now. Try asking about encryption or 2FA!"
        };

        // Responses for when sentiment is detected but NO keyword is found
        private List<string> _worriedNoKeyword = new List<string>
        {
            "I can hear that you're worried. That's completely okay! You're already doing the right thing by asking questions. Try asking me about phishing, malware, or passwords and I'll help you understand.",
            "It's normal to feel anxious about online safety. Take a breath — I'm here to help. Ask me about any cybersecurity topic and we'll work through it together.",
            "Feeling unsafe online is very common. The good news is there are simple steps to protect yourself. Ask me about passwords or privacy to get started!"
        };

        private List<string> _frustratedNoKeyword = new List<string>
        {
            "I completely understand your frustration — cybersecurity can feel overwhelming at first. Let's slow down. What specific topic is confusing you? Try typing 'passwords', 'phishing', or 'malware'.",
            "No need to feel frustrated! I'm here to make this as simple as possible. Ask me about any topic from the menu on the left and I'll explain it clearly.",
            "It's okay to feel confused — this stuff isn't always easy. Tell me which topic you want to understand better and I'll break it down step by step."
        };

        private List<string> _curiousNoKeyword = new List<string>
        {
            "I love your curiosity! There's so much to learn about cybersecurity. Pick any topic from the left menu or ask me about passwords, phishing, malware, privacy, scams, firewall, encryption, 2FA, backup, or VPN!",
            "Great that you want to learn more! Type any cybersecurity topic you're curious about and I'll give you useful tips.",
            "Curiosity is the first step to staying safe online! Ask me anything — I'm here to help you learn."
        };

        public ChatBot()
        {
            _keywords = new KeywordResponder();
            _sentiment = new SentimentDetector();
            _memory = new MemoryStore();
        }

        public string GetGreeting()
        {
            return "👋 Hello! I'm SecureNet, your Cybersecurity Assistant.\nWhat's your name?";
        }

        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type something so I can help you! 😊";

            string trimmed = input.Trim();

            // STEP 1: Capture name
            if (_awaitingName)
            {
                _memory.UserName = trimmed;
                _awaitingName = false;
                return "Nice to meet you, " + _memory.UserName + "! 🛡️\n" +
                       "I'm here to help you stay safe online.\n\n" +
                       "You can ask me about: passwords, phishing, malware, privacy, scams, " +
                       "firewalls, encryption, 2FA, backup, or VPN.\n\n" +
                       "Or click any topic from the menu on the left!";
            }

            string lower = trimmed.ToLowerInvariant();

            // STEP 2: Follow-up handling
            if ((lower.Contains("tell me more") || lower.Contains("explain more") ||
                 lower.Contains("more info") || lower.Contains("more about that"))
                && _lastTopic != null)
            {
                string followUp = _keywords.GetResponse(_lastTopic);
                return "Sure! Here's more on " + _lastTopic + ":\n\n" + followUp;
            }

            // STEP 3: Detect sentiment
            Sentiment detected = _sentiment.Detect(trimmed);
            string sentimentOpener = _sentiment.GetSentimentResponse(detected);

            // STEP 4: Keyword matching
            string keywordResponse = _keywords.GetResponse(trimmed);

            if (keywordResponse != null)
            {
                // Update last topic and memory
                foreach (string kw in _keywords.GetAllKeywords())
                {
                    if (lower.Contains(kw.ToLowerInvariant()))
                    {
                        _lastTopic = kw;
                        _memory.FavouriteTopic = kw;
                        _memory.Store("favouriteTopic", kw);
                        break;
                    }
                }

                // If sentiment detected, prepend empathetic opener
                if (detected != Sentiment.Neutral && !string.IsNullOrWhiteSpace(sentimentOpener))
                {
                    return sentimentOpener + "\n\n" + keywordResponse;
                }

                // Add personalised opener if we know their favourite topic
                string opener = _memory.GetPersonalisedOpener();
                if (!string.IsNullOrWhiteSpace(opener))
                    return opener + keywordResponse;

                return keywordResponse;
            }

            // STEP 5: Sentiment detected but NO keyword matched
            // This is where we respond with empathy even when no topic is found
            if (detected == Sentiment.Worried)
                return _worriedNoKeyword[_random.Next(_worriedNoKeyword.Count)];

            if (detected == Sentiment.Frustrated)
                return _frustratedNoKeyword[_random.Next(_frustratedNoKeyword.Count)];

            if (detected == Sentiment.Curious)
                return _curiousNoKeyword[_random.Next(_curiousNoKeyword.Count)];

            if (detected == Sentiment.Happy)
                return "That's great to hear, " + _memory.UserName + "! 😊 Keep that positive energy! " +
                       "Ask me about any cybersecurity topic whenever you're ready.";

            // STEP 6: Special phrases
            if (lower.Contains("how are you"))
                return "I'm doing great, thanks for asking " + _memory.UserName + "! 😊 " +
                       "Ready to help you stay cyber-safe!";

            if (lower.Contains("what can you do") || lower.Contains("help") ||
                lower.Contains("topics") || lower.Contains("what can i ask"))
            {
                var topics = _keywords.GetAllKeywords();
                string topicList = string.Join("\n🔒 ", topics);
                return "Here are the topics I can help you with, " + _memory.UserName + ":\n\n🔒 " +
                       topicList + "\n\nJust type any of these words and I'll give you advice!";
            }

            if (lower.Contains("purpose") || lower.Contains("what are you") ||
                lower.Contains("who are you"))
                return "I'm SecureNet — your personal Cybersecurity Assistant! " +
                       "I help people like you, " + _memory.UserName + ", stay safe online. 🛡️";

            if (lower.Contains("bye") || lower.Contains("goodbye"))
                return "Goodbye, " + _memory.UserName + "! 👋 Stay safe online. " +
                       "Remember: think before you click!";

            // STEP 7: Fallback
            return _fallbackResponses[_random.Next(_fallbackResponses.Count)];
        }
    }
}
