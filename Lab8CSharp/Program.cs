using System.Text.RegularExpressions;

Console.WriteLine("Lab 8 (variant 7)");
Console.Write("Enter the number of task (1 - 5): ");
int choice = Int32.Parse(Console.ReadLine());
switch (choice){
    case 1:{
        Task1 lab8task1 = new Task1();
        lab8task1.Run();
    }break;
    case 2:{
        Task2 lab8task2 = new Task2();
        lab8task2.Run();
    }break;
}


class Task1{
    public void Run(){
        Console.WriteLine("Enter the path to the file with text:");
        string inputFile = Console.ReadLine();

        Console.WriteLine("Enter the path to the file for results:");
        string outputFile = Console.ReadLine();

        Console.WriteLine("Enter the regular expression for searching:");
        string searchPattern = Console.ReadLine();

        Console.WriteLine("Enter the text for changing:");
        string replaceText = Console.ReadLine();

        int occurrences = SearchAndReplace(inputFile, outputFile, searchPattern, replaceText);
        
        Console.WriteLine($"Replacement completed. {occurrences} occurrences replaced. Check the output file for the result.");
    }

    private int SearchAndReplace(string inputFile, string outputFile, string searchPattern, string replaceText){
        int occurrences = 0;
        try{
            string[] lines = File.ReadAllLines(inputFile);

            using (StreamWriter writer = new StreamWriter(outputFile)){
                foreach (string line in lines){
                    string modifiedLine = line;
                    MatchCollection matches = Regex.Matches(line, searchPattern);

                    foreach (Match match in matches){
                        occurrences++;
                        if (!string.IsNullOrEmpty(replaceText)){
                            modifiedLine = modifiedLine.Replace(match.Value, replaceText);
                        }
                    }

                    writer.WriteLine(modifiedLine);
                }
            }
        }
        catch (Exception ex){
            Console.WriteLine($"Error: {ex.Message}");
        }

        return occurrences;
    }
}

class Task2{
    public void Run(){
        try{
            Console.WriteLine("Enter the path to the input file:");
            string inputFile = Console.ReadLine();

            Console.WriteLine("Enter the path to the output file:");
            string outputFile = Console.ReadLine();

            string inputText = File.ReadAllText(inputFile);

            string replacedText = ReplaceEnglishWords(inputText, out int occurrences);

            File.WriteAllText(outputFile, replacedText);

            Console.WriteLine($"Replacement completed. {occurrences} occurrences replaced. Check the output file for the result.");
        }
        catch (Exception ex){
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static string ReplaceEnglishWords(string text, out int occurrences){
        Regex regex = new Regex(@"\b[a-zA-Z]+\b");

        occurrences = regex.Matches(text).Count;
        return regex.Replace(text, "...");
    }
}