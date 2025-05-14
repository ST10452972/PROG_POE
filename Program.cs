using System;
using System.Media;
using System.Threading;
using NAudio.Wave;


class CyberSecurityBot
{
    static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            {"password", new List<string> {
                "Use strong, unique passwords for each account.",
                "Avoid using your name or birthday in passwords.",
                "Consider using a password manager to store your credentials."}},

            {"phishing", new List<string> {
                "Never click on suspicious links in emails.",
                "Watch for spelling mistakes in emails asking for your login details.",
                "Always verify the sender's email address."}},

            {"privacy", new List<string> {
                "Review your social media privacy settings regularly.",
                "Be cautious about the personal information you share online.",
                "Use two-factor authentication whenever possible."}}
        };
    static void Main()
    {
        // Play voice greeting
        PlayVoiceGreeting();

        //Display computer face
        DisplayAsciiComputer();


        // Display ASCII logo
        DisplayAsciiArt();

        // Ask for user name
        Console.Write("Enter your name: ");
        string userName = Console.ReadLine();
        Console.WriteLine($"\nHello {userName}! Welcome to the Cybersecurity Awareness Bot.");

        // Start chatbot interaction
        ChatbotLoop(userName);
    }

    static void PlayVoiceGreeting()
    {
        string path = "greeting.wav";

        if (File.Exists(path))
        {
            using (var audioFile = new AudioFileReader(path))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
        else
        {
            Console.WriteLine("Warning: Sound file not found. Skipping greeting sound...");
        }
    }

    static void DisplayAsciiArt()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@" 
       
         CYBERSECURITY AWARENESS 
                 BOT             
        
        ");
        Console.ResetColor();
    }
    static void DisplayAsciiComputer()
    {
        Console.WriteLine(@"
      .__________________________.
      | .______________________. |
      | |      ^  -  ^         | |
      | |      (o)  (o)        | |
      | |         --           | |
      | |      \_______/       | |
      | |______________________| |
      |__________________________|
    
    ");
    }


    static void ChatbotLoop(string userName)
    {
        while (true)
        {
            Console.Write("\nAsk me a cybersecurity question (or type 'exit' to quit): ");
            string userInput = Console.ReadLine().Trim().ToLower();

            if (userInput == "exit")
            {
                Console.WriteLine("Goodbye! Stay safe online.");
                break;
            }

            RespondToUser(userInput);
        }
    }

    static void RespondToUser(string input)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        switch (input)
        {
            case "how are you?":
                Console.WriteLine("I'm a bot, so I don't have feelings, but I'm here to help!");
                break;
            case "what’s your purpose?":
                Console.WriteLine("I provide cybersecurity tips to help you stay safe online.");
                break;
            case "what can i ask you about?":
                Console.WriteLine("You can ask about password safety, phishing, and safe browsing.");
                break;
            case "tell me about password safety":
                Console.WriteLine("Use long, unique passwords and enable two-factor authentication.");
                break;
            case "what is phishing?":
                Console.WriteLine("Phishing is a scam where attackers trick you into revealing sensitive information.");
                break;
            case "how can i browse safely?":
                Console.WriteLine("Use secure websites, avoid public Wi-Fi, and enable browser security features.");
                break;
            default:
                Console.WriteLine("I didn’t quite understand that. Could you rephrase?");
                break;
        }
        Console.ResetColor();
    }
}
