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
    case 3:{
        Task3 lab8task3 = new Task3();
        lab8task3.Run();
    }break;
    case 4:{
        Task4 lab8task4 = new Task4();
        lab8task4.Run();
    }break;
    case 5:{
        Task5 lab8task5 = new Task5();
        lab8task5.Run();
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

class Task3{
    public void Run(){
        try{
            Console.WriteLine("Enter the path to the input file:");
            string inputFile = Console.ReadLine();

            Console.WriteLine("Enter the path to the output file:");
            string outputFile = Console.ReadLine();

            string inputText = File.ReadAllText(inputFile);

            int occurrences;
            string replacedText = RemoveDuplicates(inputText, out occurrences);

            File.WriteAllText(outputFile, replacedText);

            Console.WriteLine($"Replacement completed. {occurrences} occurrences replaced. Check the output file for the result.");
        }
        catch (Exception ex){
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public string RemoveDuplicates(string text, out int occurrences){
        string[] words = text.Split(new char[] { ' ', ',', '.', '!', '?', ':', ';', '-', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        HashSet<string> uniqueWords = new HashSet<string>(words, StringComparer.OrdinalIgnoreCase);
        occurrences = words.Length - uniqueWords.Count;
        return string.Join(" ", uniqueWords);
    }
}

class Task4{
     public void Run(){
        try{
            Console.WriteLine("Enter the size of array:");
            int size = Convert.ToInt32(Console.ReadLine());
            int[] sequence = new int[size];

            Console.WriteLine("Enter numbers: ");
            for (int i = 0; i < size; i++){
                Console.Write("a[{0}] = ", i);
                sequence[i] = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Enter the number to filter by:");
            int filter = Convert.ToInt32(Console.ReadLine());

            string fileName = "binary_output.bin";

            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                foreach (int num in sequence)
                {
                    if (num % filter == 0)
                    {
                        writer.Write(num);
                    }
                }
            }

            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                Console.WriteLine($"Numbers multiples of {filter} in the file:");
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int num = reader.ReadInt32();
                    Console.WriteLine(num);
                }
            }
        }
        catch (Exception ex){
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

class Task5{
    public void Run(){
        try{
            //1
            string studentSurname = "Калюжна";
            string tempFolderPath = @"C:\temp\";

            string studentFolder1 = studentSurname + "1";
            string studentFolder2 = studentSurname + "2";
            Directory.CreateDirectory(Path.Combine(tempFolderPath, studentFolder1));
            Directory.CreateDirectory(Path.Combine(tempFolderPath, studentFolder2));

            string t1FilePath = Path.Combine(tempFolderPath, studentFolder1, "t1.txt");
            File.WriteAllText(t1FilePath, "<Шевченко Степан Іванович, 2001> року народження, місце проживання <м. Суми>");
            string t2FilePath = Path.Combine(tempFolderPath, studentFolder1, "t2.txt");
            File.WriteAllText(t2FilePath, "<Комар Сергій Федорович, 2000> року народження, місце проживання <м. Київ>");

            string t3FilePath = Path.Combine(tempFolderPath, studentFolder2, "t3.txt");

            string t1Content = File.ReadAllText(t1FilePath);
            string t2Content = File.ReadAllText(t2FilePath);

            File.WriteAllText(t3FilePath, t1Content + Environment.NewLine + t2Content);

            string destinationFolder2 = Path.Combine(tempFolderPath, studentFolder2);
            File.Move(t2FilePath, Path.Combine(destinationFolder2, "t2.txt"));

            File.Copy(t1FilePath, Path.Combine(destinationFolder2, "t1.txt"), true);

            Directory.Move(Path.Combine(tempFolderPath, studentFolder1), Path.Combine(tempFolderPath, "ALL"));

            string[] allFiles = Directory.GetFiles(Path.Combine(tempFolderPath, "ALL"));
            foreach (string file in allFiles)
            {
                Console.WriteLine($"File Name: {Path.GetFileName(file)}");
                Console.WriteLine($"Size: {new FileInfo(file).Length} bytes");
                Console.WriteLine($"Path: {file}");
                Console.WriteLine();
            }
        }
        catch (Exception ex){
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}