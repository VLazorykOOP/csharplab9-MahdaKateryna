using System;
using System.Collections;
using System.IO;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Оберіть завдання:");
            Console.WriteLine("1. Перетворення виразу з префіксної форми в постфіксну");
            Console.WriteLine("2. Обробка файлу з даними про співробітників");
            Console.WriteLine("4. Музикальний диск та пісня");
            Console.WriteLine("0. Вихід");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Введено некоректний варіант. Будь ласка, виберіть знову.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                case 4:
                    Task4();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Введено некоректний варіант. Будь ласка, виберіть знову.");
                    break;
            }
        }
    }

    static void Task1()
    {
        Console.WriteLine("Введіть вираз у префіксній формі:");
        string prefixExpression = Console.ReadLine();

        string postfixExpression = ConvertPrefixToPostfix(prefixExpression);

        Console.WriteLine($"Вираз у постфіксній формі: {postfixExpression}");
    }

    static string ConvertPrefixToPostfix(string prefixExpression)
    {
        Stack<string> stack = new Stack<string>();

        for (int i = prefixExpression.Length - 1; i >= 0; i--)
        {
            char currentChar = prefixExpression[i];

            if (IsOperand(currentChar))
            {
                stack.Push(currentChar.ToString());
            }
            else if (IsOperator(currentChar))
            {
                string operand1 = stack.Pop();
                string operand2 = stack.Pop();
                string result = $"{operand1} {operand2} {currentChar}";
                stack.Push(result);
            }
        }

        return stack.Pop();
    }

    static bool IsOperand(char c)
    {
        return char.IsLetterOrDigit(c);
    }

    static bool IsOperator(char c)
    {
        return c == '+' || c == '-' || c == '*' || c == '/';
    }

    static void Task2()
    {
        string filePath = @"C:\Users\Катя\Source\Repos\csharplab9-MahdaKateryna\Lab9_10CharpT\employees.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл з даними не знайдено.");
            return;
        }


        //Queue<string> maleEmployees = new Queue<string>();
        //Queue<string> femaleEmployees = new Queue<string>();

        //using (StreamReader sr = new StreamReader(filePath))
        //{
        //    string line;
        //    while ((line = sr.ReadLine()) != null)
        //    {
        //        string[] fields = line.Split(',');

        //        if (fields.Length >= 4 && (fields[3].Trim().ToLower() == "male" || fields[3].Trim().ToLower() == "female"))
        //        {
        //            if (fields[3].Trim().ToLower() == "male")
        //            {
        //                maleEmployees.Enqueue(line);
        //            }
        //            else
        //            {
        //                femaleEmployees.Enqueue(line);
        //            }
        //        }
        //    }
        //}

        //Console.WriteLine("Male Employees:");
        //while (maleEmployees.Count > 0)
        //{
        //    Console.WriteLine(maleEmployees.Dequeue());
        //}

        //Console.WriteLine("\nFemale Employees:");
        //while (femaleEmployees.Count > 0)
        //{
        //    Console.WriteLine(femaleEmployees.Dequeue());
        //}
    




        ArrayList maleEmployees = new ArrayList();
        ArrayList femaleEmployees = new ArrayList();

        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] fields = line.Split(',');

                if (fields.Length >= 4 && (fields[3].Trim().ToLower() == "male" || fields[3].Trim().ToLower() == "female"))
                {
                    Employee employee = new Employee(fields[0], fields[1], fields[2], fields[3]);
                    if (fields[3].Trim().ToLower() == "male")
                    {
                        maleEmployees.Add(employee);
                    }
                    else
                    {
                        femaleEmployees.Add(employee);
                    }
                }
            }
        }

        Console.WriteLine("Male Employees:");
        foreach (Employee employee in maleEmployees)
        {
            Console.WriteLine(employee.ToString());
        }

        Console.WriteLine("\nFemale Employees:");
        foreach (Employee employee in femaleEmployees)
        {
            Console.WriteLine(employee.ToString());
        }
    }

    static void Task4()
    {
        MusicCatalog catalog = new MusicCatalog();

        catalog.AddDisc("Best of 90s");
        catalog.AddSong("Best of 90s", "Madonna", "Vogue");
        catalog.AddSong("Best of 90s", "Madonna", "Frozen");
        catalog.AddSong("Best of 90s", "Nirvana", "Smells Like Teen Spirit");

        Console.WriteLine("\n");

        catalog.AddDisc("Greatest Hits");
        catalog.AddSong("Greatest Hits", "Queen", "Bohemian Rhapsody");
        catalog.AddSong("Greatest Hits", "Queen", "We Will Rock You");


        Console.WriteLine("\n");

        catalog.DisplayCatalog();

        Console.WriteLine("\n");

        catalog.RemoveSong("Best of 90s", "Madonna", "Vogue");

        Console.WriteLine("\n");

        catalog.DisplayCatalog();

        Console.WriteLine("\n");

        catalog.SearchByArtist("Queen");

        Console.ReadKey();
    }


}

class Employee : IComparable, ICloneable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string Gender { get; set; }

    public Employee(string firstName, string lastName, string position, string gender)
    {
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        Gender = gender;
    }

    public override string ToString()
    {
        return $"{FirstName}, {LastName}, {Position}, {Gender}";
    }

    public int CompareTo(object obj)
    {
        Employee other = obj as Employee;
        if (other == null)
        {
            throw new ArgumentException("Object is not an Employee");
        }
        return this.LastName.CompareTo(other.LastName);
    }

    public object Clone()
    {
        return new Employee(this.FirstName, this.LastName, this.Position, this.Gender);
    }
}



class MusicCatalog
{
    private Hashtable discs = new Hashtable();

    public void AddDisc(string discTitle)
    {
        if (!discs.ContainsKey(discTitle))
        {
            discs.Add(discTitle, new Hashtable());
            Console.WriteLine($"Диск {discTitle} додано.");
        }
        else
        {
            Console.WriteLine($"Диск {discTitle} вже існує.");
        }
    }

    public void RemoveDisc(string discTitle)
    {
        if (discs.ContainsKey(discTitle))
        {
            discs.Remove(discTitle);
            Console.WriteLine($"Диск {discTitle} видалено.");
        }
        else
        {
            Console.WriteLine($"Диск {discTitle} не існує.");
        }
    }

    public void AddSong(string discTitle, string artist, string songTitle)
    {
        if (discs.ContainsKey(discTitle))
        {
            Hashtable disc = (Hashtable)discs[discTitle];
            if (!disc.ContainsKey(artist))
            {
                disc.Add(artist, new ArrayList());
            }
            ArrayList songs = (ArrayList)disc[artist];
            if (!songs.Contains(songTitle))
            {
                songs.Add(songTitle);
                Console.WriteLine($"Пісню {songTitle} виконавця {artist} додано до диску {discTitle}.");
            }
            else
            {
                Console.WriteLine($"Пісня {songTitle} виконавця {artist} вже існує на диску {discTitle}.");
            }
        }
        else
        {
            Console.WriteLine($"Диск {discTitle} не існує.");
        }
    }

    public void RemoveSong(string discTitle, string artist, string songTitle)
    {
        if (discs.ContainsKey(discTitle))
        {
            Hashtable disc = (Hashtable)discs[discTitle];
            if (disc.ContainsKey(artist))
            {
                ArrayList songs = (ArrayList)disc[artist];
                if (songs.Contains(songTitle))
                {
                    songs.Remove(songTitle);
                    Console.WriteLine($"Пісню {songTitle} виконавця {artist} видалено з диску {discTitle}.");
                }
                else
                {
                    Console.WriteLine($"Пісні {songTitle} виконавця {artist} не існує на диску {discTitle}.");
                }
            }
            else
            {
                Console.WriteLine($"Виконавець {artist} не існує на диску {discTitle}.");
            }
        }
        else
        {
            Console.WriteLine($"Диск {discTitle} не існує.");
        }
    }

    public void DisplayCatalog()
    {
        Console.WriteLine("Каталог музичних дисків:");
        foreach (DictionaryEntry discEntry in discs)
        {
            Console.WriteLine($"Диск: {discEntry.Key}");
            Hashtable disc = (Hashtable)discEntry.Value;
            foreach (DictionaryEntry artistEntry in disc)
            {
                Console.WriteLine($"  Виконавець: {artistEntry.Key}");
                ArrayList songs = (ArrayList)artistEntry.Value;
                foreach (string song in songs)
                {
                    Console.WriteLine($"    Пісня: {song}");
                }
            }
        }
    }

    public void SearchByArtist(string artist)
    {
        Console.WriteLine($"Пошук пісень виконавця {artist}:");
        bool found = false;
        foreach (DictionaryEntry discEntry in discs)
        {
            Hashtable disc = (Hashtable)discEntry.Value;
            if (disc.ContainsKey(artist))
            {
                ArrayList songs = (ArrayList)disc[artist];
                foreach (string song in songs)
                {
                    Console.WriteLine($"  Диск: {discEntry.Key}, Пісня: {song}");
                    found = true;
                }
            }
        }
        if (!found)
        {
            Console.WriteLine($"Пісень виконавця {artist} не знайдено.");
        }
    }
}