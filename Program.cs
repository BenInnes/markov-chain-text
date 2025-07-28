using System;
using System.IO;
using System.Runtime.CompilerServices;

class Program
{
    static void Main()
    {
        Console.WriteLine("Has lexicon been created? (y/n)");
        string MakeFile = Console.ReadLine().ToLower();

        Console.WriteLine("Please enter the filepath to your lexicon, or where you want it if it has not been created yet");
        string lexiconFilePath = Console.ReadLine().Trim().Trim('"');


        if (MakeFile == "n")
        {
            //read all from file and print  for error checking purposes
            Console.WriteLine("Please enter the filepath to your data");
            string data = Console.ReadLine().Trim().Trim('"');

            Console.WriteLine(data);
            //split file into words using whitespace as delimiter
            string[] words = data.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);


            //write to file
            using (StreamWriter writer = new StreamWriter(lexiconFilePath, false)) //false means if file is already present, overwrite it
            {
                for (int i = 0; i <= words.Length - 3; i++)
                {
                    string line = $"{words[i]} {words[i + 1]}";
                    writer.WriteLine($"{line},{words[i + 2]}");
                    Console.WriteLine(line + "," + words[i + 2] + " written successfully");
                }
                Console.WriteLine("File writing complete");
            }
        }


        Console.WriteLine("How many words would you like generated?");

        string wordCountStr = Console.ReadLine(); //read user response as string
        int wordCount = 1000; //generates 1000 words if user input fails to parse

        if (!int.TryParse(wordCountStr, out wordCount)) //attempt to parse user input to int
        {
            Console.WriteLine("Input failed to parse. Generation automatically set to 1000 words. Please restart the program if this is incorrect.");
        }

        Console.WriteLine("Please enter two words as a seed (words must exist in lexicon), case sensitive (sorry)");

        string seed = Console.ReadLine();
        //check seed exists in lexicon
        
        bool found = false;

        using (StreamReader reader = new StreamReader(lexiconFilePath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(",");

                if (parts.Length > 0 && parts[0].Equals(seed, StringComparison.OrdinalIgnoreCase)) //parts is the current phrase in file, check if it is not a null character and it is equal to the seed
                {
                    found = true;
                    Console.WriteLine("found seed in lexicon");
                    break;
                }
            }
        }

        string text = seed; //start the text


        string? targetPhrase = seed;
        List<string> sequence = new List<string>(seed.Split(' '));
        //moved from line 81 (debug)

        for (int i = 0; i <= wordCount - 2; i++) //generate text until word count is reached (starts at 2 because 2 seed words)
        {
           
            

            Dictionary<string, int> thirdWordCounts = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader(lexiconFilePath))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(",");

                    if (parts.Length < 2)
                    {
                        continue;
                    }
                    string phrase = parts[0].Trim();
                    string thirdWord = parts[1].Trim();

                    if (phrase.Equals(targetPhrase, StringComparison.OrdinalIgnoreCase))
                    {
                        if (thirdWordCounts.ContainsKey(thirdWord))
                        {
                            thirdWordCounts[thirdWord]++;
                        }
                        else
                        {
                            thirdWordCounts[thirdWord] = 1;
                        }
                    }
                }
            }

            if (thirdWordCounts.Count ==0)
            {
                Console.WriteLine("No matches for phrase: \"{targetPhrase}\". Stopping generation");
                break;
            }

            string selectedWord = WeightedRandomChoice(thirdWordCounts);
            sequence.Add(selectedWord);

            //move target phrase to teh right
            targetPhrase = $"{sequence[sequence.Count - 2]} {sequence[sequence.Count - 1]}";
        }

        //end of code
        Console.WriteLine("Final result: ");
        Console.WriteLine(string.Join(" ", sequence));
    }

    static string WeightedRandomChoice(Dictionary<string, int> items)
    {
        Random rand = new Random();
        int totalWeight = items.Values.Sum();
        int randomValue = rand.Next(0, totalWeight);

        foreach (var item in items)
        {
            randomValue -= item.Value;
            if (randomValue < 0)
            {
                return item.Key;
            }
        }
        Console.WriteLine("FALLBACK TRIGGERED. REDO FROM START");
        return items.Keys.First();
    }
}
