using CacheSimulator.Tokens.Enums;

namespace CacheSimulator.Tokens.Models;

public class Cache(CacheType type, long length)
{
    public CacheType Type { get; } = type;
    public long Length { get; } = length;
    public int TotalHits { get; } = 0;
    public int TotalMisses { get; } = 0;
    public List<OperationHistory> History { get; } = new List<OperationHistory>();
    
    private Dictionary<long, long> MemoryMap { get; set; } = new Dictionary<long, long>();
    
    public bool LoadAddress(long memoryAdrress)
    {
        throw new Exception("Method do not implemented");
    }
    
    public bool SaveAddress(long memoryAdrress)
    {
        throw new Exception("Method do not implemented");
    }

    public override string ToString()
    {
        return base.ToString();
    }
}