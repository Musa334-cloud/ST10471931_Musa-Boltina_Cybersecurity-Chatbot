using System;

class Program
{
    static void Main(string[] args)
    {
        // PLAY AUDIO
        AudioPlayer audio = new AudioPlayer();
        audio.PlayGreeting();

        // TITLE
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("==========================================================");
        Console.WriteLine("      Welcome to SecureNet Cybersecurity Bot");
        Console.WriteLine("==========================================================");
        Console.ResetColor();

        // GET USER NAME
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            name = "User";
        }

        // CREATE USER OBJECT
        User user = new User(name);

        // START CHATBOT
        Chatbot bot = new Chatbot();
        bot.Start(user);
    }
}