namespace PJP_VAV0256;

class Program
{
    private static void Main()
    {
        if (!int.TryParse(Console.ReadLine(), out var n))
        {
            Console.WriteLine("ERROR");
            return;
        }

        for (var i = 0; i < n; i++)
        {
            var line = Console.ReadLine();
            if (line == null)
            {
                Console.WriteLine("ERROR");
                continue;
            }
            try
            {
                var parser = new Parser(line);
                var result = parser.ParseExpression();
                parser.ExpectEnd();
                Console.WriteLine(result);
            }
            catch
            {
                Console.WriteLine("ERROR");
            }
        }
    }
}