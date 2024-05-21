using CacheSimulator.Tokens.Enums;
using CacheSimulator.Tokens.Models;

Cache? cache = null;

try
{
    Console.Write("Enter cache type: (0) Direct or (1) Associative ");
    string? inputType = Console.ReadLine();
    var isType = Enum.TryParse(inputType, out CacheType type);

    Console.Write("Enter cache length ");
    string? inputLength = Console.ReadLine();
    var isNumber = Int32.TryParse(inputLength, out var length);

    if (!isType || !isNumber)
    {
        throw new Exception("Invalid Input");
    }

    switch(type) {
        case CacheType.Direct:
            cache = new DirectCache(length);
            break;
        case CacheType.Associative:
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }
}
catch (Exception e)
{
    Console.WriteLine("Invalid Input");
}

if (cache is not null)
{
    Console.WriteLine(cache);
}

