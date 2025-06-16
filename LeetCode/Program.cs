using LeetCode.PascalsTriangle;

while (true)
{
    // Ask user which program they want to run by asking for a number

    Console.WriteLine("Which problem would you like to run? (1-10)");
    Console.WriteLine("1. Pascals Triangle");
    Console.WriteLine("2. Maximum Difference between Increasing Elements");

    var input = Console.ReadLine();

    if (int.TryParse(input, out int choice))
    {
        switch (choice)
        {
            case 1:
                Console.WriteLine("You chose Pascals Triangle.");
                Console.WriteLine("How many rows would you like to generate?");
                var rowsInput = Console.ReadLine();
                Console.WriteLine("");
                if (int.TryParse(rowsInput, out int numRows))
                {
                    var pascalsTriangle = PascalsTriangle.Generate(numRows);
                    for (int i = 0; i < pascalsTriangle.Count; i++)
                    {
                        var row = pascalsTriangle[i];
                        Console.WriteLine(string.Join(", ", row.Select(r => r.ToString())));
                    }
                }
                else
                {
                    Console.WriteLine("Invalid number of rows. Please enter a valid integer.");
                }
                break;
            case 2:
                Console.WriteLine("You chose Maximum Difference between Increasing Elements.");
                // Ask how large the list of numbers should be
                Console.WriteLine("How many numbers should there be in the list?");
                var countInput = Console.ReadLine();

                Console.WriteLine("");
                if (int.TryParse(countInput, out int count) && count > 0)
                {
                    var numbers = new int[count];
                    for (int i = 0; i < count; i++)
                    {
                        var randomNumber = new Random().Next(1, 100); // Generate a random number between 1 and 100
                        numbers[i] = randomNumber;
                    }
                    Console.WriteLine("Generated numbers: [ " + string.Join(", ", numbers) + " ]");
                    var maxDifference = MaximumDifference.DetermineDifference2(numbers);

                    Console.WriteLine($"The maximum difference between increasing elements is: {maxDifference}");
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("Invalid count. Please enter a positive integer.");
                }
                // Call the MaximumDifferenceBetweenIncreasingElements method here if needed
                break;
            default:
                Console.WriteLine("Invalid choice. Please select a number between 1 and 10.");
                break;
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a number.");
    }
}
