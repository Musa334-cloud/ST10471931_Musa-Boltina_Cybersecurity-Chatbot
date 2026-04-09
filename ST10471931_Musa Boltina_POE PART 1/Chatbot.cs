using System;
using System.Threading;

class Chatbot
{
    public void Start(User user)
    {
        // ASCII ART
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
   _____                          _   _      _   
  / ____|                        | \ | |    | |  
 | (___   ___  ___ _   _ _ __ ___|  \| | ___| |_ 
  \___ \ / _ \/ __| | | | '__/ _ \ . ` |/ _ \ __|
  ____) |  __/ (__| |_| | | |  __/ |\  |  __/ |_ 
 |_____/ \___|\___|\__,_|_|  \___|_| \_|\___|\__|
                                                 
                                                 
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
            Console.WriteLine("0. Exit");
            Console.WriteLine("====================");
            Console.ResetColor();

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(choice))
            {
                Console.WriteLine("Please enter a valid option.");
                continue;
            }

            switch (choice)
            {
                case "1":
                    Console.WriteLine("I'm doing great! Thanks for asking.");
                    break;
                case "2":
                    Console.WriteLine("I help users understand cybersecurity threats and stay safe online.");
                    break;
                case "3":
                    Console.WriteLine("Strong passwords are essential for protecting your accounts. Use a mix of uppercase and lowercase letters, numbers, and special characters. Avoid using personal information like your name or birthdate, and never reuse the same password across multiple sites.");
                    break;

                case "4":
                    Console.WriteLine("Phishing is a type of cyber attack where criminals try to trick you into revealing sensitive information like passwords or bank details. They often use fake emails or websites that look legitimate. Always check the sender’s email address, avoid clicking suspicious links, and never share personal information online unless you are sure the source is trusted.");
                    break;

                case "5":
                    Console.WriteLine("Malware is malicious software designed to harm your computer or steal your data. This includes viruses, spyware, and ransomware. To stay safe, install antivirus software, avoid downloading files from unknown sources, and keep your system updated.");
                    break;

                case "6":
                    Console.WriteLine("A firewall is a security system that monitors and controls incoming and outgoing network traffic. It acts as a barrier between your computer and potential threats, blocking unauthorized access while allowing safe communication.");
                    break;

                case "7":
                    Console.WriteLine("Social engineering is when attackers manipulate people into giving away confidential information. This can happen through phone calls, emails, or messages pretending to be from trusted sources. Always verify identities before sharing sensitive information.");
                    break;

                case "8":
                    Console.WriteLine("Safe browsing means using the internet responsibly. Avoid visiting suspicious websites, do not download unknown files, and ensure websites use HTTPS. Be cautious when entering personal information online.");
                    break;

                case "9":
                    Console.WriteLine("Software updates are important because they fix security vulnerabilities and improve performance. Hackers often exploit outdated systems, so always keep your operating system and applications up to date.");
                    break;
                case "0":
                    Console.WriteLine($"Goodbye {user.Name}! Stay safe online.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}