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
    static void DetectSentiment(string input)
    {
        if (input.Contains("worried") || input.Contains("scared") || input.Contains("anxious") || input.Contains("overwhelmed"))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("It's completely understandable to feel that way. Scammers and cyber threats can be intimidating. Let me share some tips to help you stay safe.");
            Console.ResetColor();
        }
        else if (input.Contains("curious") || input.Contains("interested"))
        {
            Console.WriteLine("Curiosity is great! Learning more about cybersecurity is one of the best ways to protect yourself online.");
        }
        else if (input.Contains("frustrated") || input.Contains("confused"))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("I'm here to help clear things up. Cybersecurity can be tricky, but you're not alone.");
            Console.ResetColor();
        }
    }
    static void RememberInterest(string input)
    {
        foreach (var keyword in keywordResponses.Keys)
        {
            if (input.Contains(keyword))
            {
                userInterest = keyword;
                Console.WriteLine($"Great! I'll remember that you're interested in {keyword}. It's an important part of staying safe online.\n");
                break;
            }
        }
    }

    static void RespondToKeywords(string input)
    {
        bool matched = false;

        foreach (var keyword in keywordResponses.Keys)
        {
            if (input.Contains(keyword))
            {
                matched = true;
                lastTopic = keyword;
                Random rand = new Random();
                List<string> responses = keywordResponses[keyword];
                string reply = responses[rand.Next(responses.Count)];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{reply}");
                Console.ResetColor();
                return;
            }
        }

        if (!matched && !string.IsNullOrEmpty(lastTopic))
        {
            Console.WriteLine($"We were talking about {lastTopic}. Would you like more tips or details on that topic?");
        }
        else if (!matched)
        {
            Console.WriteLine("I didn’t quite understand that. Could you rephrase?");
        }
    }
}
