using CacheSimulator.Tokens.Enums;

namespace CacheSimulator.Tokens.Models;

public abstract class Cache
{
    public abstract CacheType Type { get; }
    protected abstract long Length { get; }
    protected int TotalHits { get; set; } = 0;
    protected int TotalMisses { get; set; } = 0;
    protected List<OperationHistory> History { get; } = new List<OperationHistory>();
    
    protected abstract Dictionary<long, (bool, long)> MemoryMap { get; set; }

    public abstract bool LoadAddress(long memoryAdrress);

    public abstract bool SaveAddress(long memoryAdrress);

    public override  string ToString()
    {
        throw new Exception("Not use Cache class");
    }

    protected abstract void AddHistory(OperationsType op, long principalMemoryAddress, bool wasHit = true, long? saveOnAddress = null);
}