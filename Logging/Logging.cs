namespace test.Logging
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            if(type == "error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR -" + message);
            }

            if(type == "info")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("INFO -" + message);
            }
        }
    }
}
