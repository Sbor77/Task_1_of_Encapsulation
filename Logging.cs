using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string message = "Log message";
        
        Pathfinder consoleLogger = new Pathfinder(message, true, false);
        Pathfinder fileLogger = new Pathfinder(message, false, true);
        Pathfinder fridayFileLogger = new Pathfinder(message, false, false, false, true);
        Pathfinder fridayConsoleLogger = new Pathfinder(message, false, false, true, false);
        Pathfinder fridayFileAndConsoleLogger = new Pathfinder(message, true, false, false, true);
        
        consoleLogger.Find();
        fileLogger.Find();
        fridayFileLogger.Find();
        fridayConsoleLogger.Find();
        fridayFileAndConsoleLogger.Find();        
    } 
}

interface ILogger
{
    void Find()
}

class Pathfinder : ILogger
{
    private string _message;
    private bool _toConsole;
    private bool _toFile;
    private bool _toConsoleFriday;
    private bool _toFileFriday;

    public Pathfinder(string message, bool toConsole = false, bool toFile = false, bool toConsoleFriday = false, bool toFileFriday = false)
    {
        _message = message;
        _toConsole = toConsole;
        _toFile = toFile;
        _toConsoleFriday = toConsoleFriday;
        _toFileFriday = toFileFriday;

        if (toConsole == false && toFile == false && toConsoleFriday == false && toFileFriday == false)
            throw new ArgumentOutOfRangeException();        
    }

    public void Find()
    {
        bool friday = (DateTime.Now.DayOfWeek == DayOfWeek.Friday);

        if (_toConsole || (_toConsoleFriday && friday))
            Console.WriteLine(_message);

        if (_toFile || (_toFileFriday && friday))
            File.WriteAllText("log.txt", _message);
    }
}