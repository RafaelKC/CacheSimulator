using CacheSimulator.Tokens.Enums;

namespace CacheSimulator.Tokens.Models;

public class DirectCache(long length): Cache
{
    public override CacheType Type { get; } = CacheType.Direct;
    protected override  long Length { get; } = length;
    
    protected override Dictionary<long, (bool, long)> MemoryMap { get; set; } = new Dictionary<long, (bool, long)>();
    
    public override bool LoadAddress(long memoryAdrress)
    {
        var cacheAddress = memoryAdrress % Length;
        var isOnCache = MemoryMap.TryGetValue(cacheAddress, out var addressOnCache);
        var wasHit = isOnCache && addressOnCache.Item2 == memoryAdrress;

        if (wasHit)
        {
            TotalHits++;
        }
        else
        {
            TotalMisses++;
            MemoryMap[cacheAddress] = (false, memoryAdrress);
        }

        return wasHit;
    }

    public override bool SaveAddress(long memoryAdrress)
    {
        var cacheAddress = memoryAdrress % Length;
        var isOnCache = MemoryMap.TryGetValue(cacheAddress, out var addressOnCache);
        var wasHit = isOnCache && addressOnCache.Item2 == memoryAdrress;

        TotalHits++;

        MemoryMap[cacheAddress] = (true, memoryAdrress);
        return wasHit;
    }

    public override string ToString()
    {
        return Length.ToString();
    }
}