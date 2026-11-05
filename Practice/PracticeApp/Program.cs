Console.Write("What's your name? ");
string name = Console.ReadLine();
string greeting = $"Welcome, {name.ToUpper()}!";
Console.WriteLine(greeting);
Console.Write($"Hello {name}, can you please enter your age as well: ");
int age = int.Parse(Console.ReadLine());

Console.WriteLine($"we see that you are {age} of age, pretty big!");