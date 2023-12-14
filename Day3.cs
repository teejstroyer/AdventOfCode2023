
/*
--- Day 3: Gear Ratios ---

You and the Elf eventually reach a gondola lift station; he says the gondola lift will take you up to the water source, but this is as far as he can bring you. You go inside.

It doesn't take long to find the gondolas, but there seems to be a problem: they're not moving.

"Aaah!"

You turn around to see a slightly-greasy Elf with a wrench and a look of surprise. "Sorry, I wasn't expecting anyone! The gondola lift isn't working right now; it'll still be a while before I can fix it." You offer to help.

The engineer explains that an engine part seems to be missing from the engine, but nobody can figure out which one. If you can add up all the part numbers in the engine schematic, it should be easy to work out which part is missing.

The engine schematic (your puzzle input) consists of a visual representation of the engine. There are lots of numbers and symbols you don't really understand, but apparently any number adjacent to a symbol, even diagonally, is a "part number" and should be included in your sum. (Periods (.) do not count as a symbol.)

Here is an example engine schematic:

467..114....
...*........
..35..633...
......#.....
617*.......1
.....+.58.#.
..592.......
......755...
...$.*......
.664.598....
In this schematic, two numbers are not part numbers because they are not adjacent to a symbol: 114 (top right) and 58 (middle right). Every other number is adjacent to a symbol and so is a part number; their sum is 4361.

Of course, the actual engine schematic is much larger. What is the sum of all of the part numbers in the engine schematic?

--- Part Two ---

The engineer finds the missing part and installs it in the engine! As the engine springs to life, you jump in the closest gondola, finally ready to ascend to the water source.

You don't seem to be going very fast, though. Maybe something is still wrong? Fortunately, the gondola has a phone labeled "help", so you pick it up and the engineer answers.

Before you can explain the situation, she suggests that you look out the window. There stands the engineer, holding a phone in one hand and waving with the other. You're going so slowly that you haven't even left the station. You exit the gondola.

The missing part wasn't the only issue - one of the gears in the engine is wrong. A gear is any * symbol that is adjacent to exactly two part numbers. Its gear ratio is the result of multiplying those two numbers together.

This time, you need to find the gear ratio of every gear and add them all up so that the engineer can figure out which gear needs to be replaced.

Consider the same engine schematic again:

467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..
In this schematic, there are two gears. The first is in the top left; it has part numbers 467 and 35, so its gear ratio is 16345. The second gear is in the lower right; its gear ratio is 451490. (The * adjacent to 617 is not a gear because it is only adjacent to one part number.) Adding up all of the gear ratios produces 467835.

What is the sum of all of the gear ratios in your engine schematic?

*/


public static class Day3
{
    public static void Run()
    {
        string Input = File.ReadAllText("Inputs/Day3.txt");
        var input = Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var sum = 0;

        HashSet<int> numbers = new HashSet<int>();
        Dictionary<int, int> found = new();

        for (int i = 0; i < input.Length; i++)
        {
            int left = 0;
            bool adjacent = false;
            for (int j = 0; j < input[i].Length; j++)
            {
                if (!char.IsDigit(input[i][j]))
                {
                    if (adjacent && j >= left)
                    {
                        var number = int.Parse(input[i].Substring(left, j - left));
                        sum += number;
                    }
                    left = j + 1;
                    adjacent = false;
                    continue;
                }

                for (int x = i - 1; x <= i + 1; x++)
                {
                    if (x < 0 || x >= input.Length) continue;
                    for (int y = j - 1; y <= j + 1; y++)
                    {
                        if (y < 0 || y >= input[x].Length) continue;
                        if (!char.IsDigit(input[x][y]) && input[x][y] != '.')
                        {
                            adjacent = true;
                            break;
                        }
                    }
                }
            }

            if (adjacent && input[i].Length >= left)
            {
                var number = int.Parse(input[i].Substring(left));
                sum += number;
            }
        }


        Console.WriteLine($"Part 1: {sum}");

        int sum2 = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == '*')
                {
                    HashSet<int> partNumbers = [];

                    for (int x = i - 1; x <= i + 1; x++)
                    {
                        if (x < 0 || x >= input.Length) continue;
                        for (int y = j - 1; y <= j + 1; y++)
                        {
                            if (y < 0 || y >= input[x].Length) continue;
                            if (char.IsDigit(input[x][y]))
                            {
                                int left = y;
                                while (left > 0 && char.IsDigit(input[x][left - 1])) left--;
                                int right = y;
                                while (right < input[x].Length - 1 && char.IsDigit(input[x][right + 1])) right++;
                                partNumbers.Add(int.Parse(input[x].Substring(left, right - left + 1)));
                            }
                        }
                    }
                    if (partNumbers.Count == 2)
                    {
                        sum2 += partNumbers.Aggregate((a, b) => a * b);
                    }

                }
            }

        }

        Console.WriteLine($"Part 2: {sum2}");

    }

    public static string TestInput = @"
467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..
";

}
