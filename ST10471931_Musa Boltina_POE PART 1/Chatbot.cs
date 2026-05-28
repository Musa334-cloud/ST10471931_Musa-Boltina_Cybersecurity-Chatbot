using System;
using System.Threading;

class Chatbot
{
    public void Start(User user)
    {
        // ASCII ART
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
  ███████╗███████╗ ██████╗██╗   ██╗██████╗ ███████╗   ███╗   ██╗███████╗████████╗
   ██╔════╝██╔════╝██╔════╝██║   ██║██╔══██╗██╔════╝   ████╗  ██║██╔════╝╚══██╔══╝
   ███████╗█████╗  ██║     ██║   ██║██████╔╝█████╗     ██╔██╗ ██║█████╗     ██║   
   ╚════██║██╔══╝  ██║     ██║   ██║██╔══██╗██╔══╝     ██║╚██╗██║██╔══╝     ██║   
   ███████║███████╗╚██████╗╚██████╔╝██║  ██║███████╗   ██║ ╚████║███████╗   ██║   
   ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚══════╝   ╚═╝  ╚═══╝╚══════╝   ╚═╝   

                    SECURE.NET
            Secure Today. Safe Tomorrow.
                                                 
                                                 
");
        Console.ResetColor();

        // TYPING EFFECT
        string intro = $"Hello {user.Name}, I am SecureNet - your Cybersecurity Assistant!";
        foreach (char c in intro)
        {
            Console.Write(c);
            Thread.Sleep(30);
        }
        Console.WriteLine("\n");

        // MENU LOOP
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n======= MENU =======");
            Console.WriteLine("1. How are you?");
            Console.WriteLine("2. Purpose of SecureNet");
            Console.WriteLine("3. Password tips");
            Console.WriteLine("4. Phishing awareness");
            Console.WriteLine("5. Malware info");
            Console.WriteLine("6. Firewall basics");
            Console.WriteLine("7. Social engineering");
            Console.WriteLine("8. Safe browsing");
            Console.WriteLine("9. Importance of updates");
            Console.WriteLine("10. Encryption basics");
            Console.WriteLine("11. Two-factor authentication (2FA)");
            Console.WriteLine("12. Secure backups");
            Console.WriteLine("13. Network security");
            Console.WriteLine("14. IoT security");
            Console.WriteLine("15. Incident response");
            Console.WriteLine("16. Privacy best practices");
            Console.WriteLine("0. Exit");
            Console.WriteLine("====================");
            Console.ResetColor();

            Console.Write("Choose an option (enter numbers, separated by spaces or commas; or ask 'what can i ask you about'): ");
            string choice = Console.ReadLine();

            // Special query to list topics
            if (IsAskAboutQuery(choice))
            {
                ShowTopics();
                continue;
            }

            bool exit = ProcessAndRespond(choice, user);
            if (exit) return;
        }
    }

    // Typing effect helper methods
    private void TypeWrite(string text, int delay = 30)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
    }

    private void TypeWriteLine(string text, int delay = 30)
    {
        TypeWrite(text, delay);
        Console.WriteLine();
    }

    // Determine if user asked "what can I ask you about" (including common typos)
    private bool IsAskAboutQuery(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        string lower = input.ToLowerInvariant();
        return lower.Contains("what can i ask") || lower.Contains("what can i ask you") || lower.Contains("what can i ask you about") || lower.Contains("what can i ask you aboutr");
    }

    // Display full topic list
    private void ShowTopics()
    {
        TypeWriteLine("Here are the topics you can ask me about:");
        TypeWriteLine("1. How are you?");
        TypeWriteLine("2. Purpose of SecureNet");
        TypeWriteLine("3. Password tips");
        TypeWriteLine("4. Phishing awareness");
        TypeWriteLine("5. Malware info");
        TypeWriteLine("6. Firewall basics");
        TypeWriteLine("7. Social engineering");
        TypeWriteLine("8. Safe browsing");
        TypeWriteLine("9. Importance of updates");
        TypeWriteLine("10. Encryption basics");
        TypeWriteLine("11. Two-factor authentication (2FA)");
        TypeWriteLine("12. Secure backups");
        TypeWriteLine("13. Network security");
        TypeWriteLine("14. IoT security");
        TypeWriteLine("15. Incident response");
        TypeWriteLine("16. Privacy best practices");
        TypeWriteLine("0. Exit");
    }

    // Process input that may contain multiple numeric choices and respond to each
    private bool ProcessAndRespond(string input, User user)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            TypeWriteLine("Please enter a valid option.");
            return false;
        }

        // Normalize and split input into tokens
        string normalized = input.Replace(',', ' ').Replace(';', ' ').Replace("and", " ").Trim();
        string[] parts = normalized.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        var selections = new System.Collections.Generic.List<int>();
        foreach (var p in parts)
        {
            if (p.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 1; i <= 16; i++) selections.Add(i);
                break;
            }

            if (int.TryParse(p, out int num))
            {
                selections.Add(num);
            }
        }

        if (selections.Count == 0)
        {
            TypeWriteLine("Invalid option. Try again.");
            return false;
        }

        // If user selected 0, exit with friendly message
        if (selections.Contains(0))
        {
            TypeWriteLine($"Goodbye {user.Name}! Thank you for chatting with SecureNet. Stay safe and take care!");
            return true;
        }

        // Respond to each selected topic
        foreach (int sel in selections)
        {
            if (sel >= 1 && sel <= 16)
            {
                RespondToChoice(sel);
            }
        }

        return false;
    }

    private void RespondToChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                TypeWriteLine("I'm doing great! Thanks for asking.");
                break;
            case 2:
                TypeWriteLine("I help users understand cybersecurity threats and stay safe online.");
                break;
            case 3:
                TypeWriteLine("Strong passwords are essential for protecting your accounts. Use a mix of uppercase and lowercase letters, numbers, and special characters. Avoid using personal information like your name or birthdate, and never reuse the same password across multiple sites.");
                break;
            case 4:
                TypeWriteLine("Phishing is a type of cyber attack where criminals try to trick you into revealing sensitive information like passwords or bank details. They often use fake emails or websites that look legitimate. Always check the sender’s email address, avoid clicking suspicious links, and never share personal information online unless you are sure the source is trusted.");
                break;
            case 5:
                TypeWriteLine("Malware is malicious software designed to harm your computer or steal your data. This includes viruses, spyware, and ransomware. To stay safe, install antivirus software, avoid downloading files from unknown sources, and keep your system updated.");
                break;
            case 6:
                TypeWriteLine("A firewall is a security system that monitors and controls incoming and outgoing network traffic. It acts as a barrier between your computer and potential threats, blocking unauthorized access while allowing safe communication.");
                break;
            case 7:
                TypeWriteLine("Social engineering is when attackers manipulate people into giving away confidential information. This can happen through phone calls, emails, or messages pretending to be from trusted sources. Always verify identities before sharing sensitive information.");
                break;
            case 8:
                TypeWriteLine("Safe browsing means using the internet responsibly. Avoid visiting suspicious websites, do not download unknown files, and ensure websites use HTTPS. Be cautious when entering personal information online.");
                break;
            case 9:
                TypeWriteLine("Software updates are important because they fix security vulnerabilities and improve performance. Hackers often exploit outdated systems, so always keep your operating system and applications up to date.");
                break;
            case 10:
                TypeWriteLine("Encryption protects data by converting it into a format that can only be read with the correct key. Use encryption for sensitive files, communications, and when storing data in the cloud.");
                break;
            case 11:
                TypeWriteLine("Two-factor authentication (2FA) adds an extra layer of security by requiring a second form of verification (like a code from your phone) in addition to your password.");
                break;
            case 12:
                TypeWriteLine("Keep secure backups of important data, stored offline or in a trusted cloud service. Regular backups help you recover from ransomware or hardware failure.");
                break;
            case 13:
                TypeWriteLine("Network security involves protecting the integrity and usability of your network and data. Use strong passwords on routers, keep firmware updated, and segment guest networks.");
                break;
            case 14:
                TypeWriteLine("IoT devices can be vulnerable. Change default passwords, update firmware, and isolate smart devices on a separate network when possible.");
                break;
            case 15:
                TypeWriteLine("Incident response is the process of identifying, containing, and recovering from security incidents. Have a plan, know who to contact, and keep backups ready.");
                break;
            case 16:
                TypeWriteLine("Privacy best practices include limiting data sharing, reviewing app permissions, using strong privacy settings, and being cautious about what you post online.");
                break;
            default:
                // ignore out-of-range selections
                break;
        }
    }
}