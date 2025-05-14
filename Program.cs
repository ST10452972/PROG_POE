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
    static string userName = "";
    static string userInterest = "";
    static string lastTopic = "";

    static void Main()
    {
        // Play voice greeting
        PlayVoiceGreeting();

        //Display computer face
        DisplayAsciiComputer();


        // Display ASCII logo
        DisplayAsciiArt();

       
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
    static void GreetUser()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("What is your name? ");
            Console.ResetColor();
            userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Name cannot be empty. Please enter a valid name.");
                Console.ResetColor();
                continue;
            }

            if (Regex.IsMatch(userName, "^\\d+$"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Name cannot be numbers only. Please enter a valid name.");
                Console.ResetColor();
                continue;
            }

            break;
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"Hello, {userName}! I'm here to help you stay safe online.");
        Console.ResetColor();
    }


    static void MainLoop()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{userName}, what would you like to ask me? (Type 'exit' to quit)> ");
            Console.ResetColor();
            string input = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("[!] Please enter a valid question.");
                continue;
            }

            if (input.Contains("exit"))
            {
                Console.WriteLine("Goodbye! Stay safe out there.");
                break;
            }

            if (Regex.IsMatch(input, "^\\d+$"))
            {
                Console.WriteLine("[!] That looks like a number. Please ask a question using words.");
                continue;
            }

            if (Regex.IsMatch(input, "^[a-zA-Z]+$") && !keywordResponses.ContainsKey(input))
            {
                Console.WriteLine("[!] That looks like a string of letters, but I didn’t recognize the topic. Please try a more complete question.");
                continue;
            }

            DetectSentiment(input);
            RememberInterest(input);
            RespondToKeywords(input);
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
