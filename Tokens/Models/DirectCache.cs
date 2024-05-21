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
        if (isOnCache && addressOnCache.Item2 == memoryAdrress)
        {
            TotalHits += 1;
            AddHistory(OperationsType.Read, memoryAdrress);
            return true;
        }

        var needToSaveOldValueOnMemory = isOnCache && addressOnCache.Item1;
        AddHistory(OperationsType.Read, memoryAdrress, false, needToSaveOldValueOnMemory ? addressOnCache.Item2 : null);
        TotalMisses += 1;
        MemoryMap.Add(cacheAddress, (false, memoryAdrress));
        return false;

    }
    
    public override bool SaveAddress(long memoryAdrress)
    {
        var cacheAddress = memoryAdrress % Length;
        var isOnCache = MemoryMap.TryGetValue(cacheAddress, out var addressOnCache);

        MemoryMap.Add(cacheAddress, (true, memoryAdrress));

        var wasHit = isOnCache && addressOnCache.Item2 == memoryAdrress;
        var needToSaveOldValueOnMemory = isOnCache && !wasHit && addressOnCache.Item1;

        AddHistory(OperationsType.Write, memoryAdrress, wasHit, needToSaveOldValueOnMemory ? addressOnCache.Item2 : null);

        return wasHit;
    }

    public override string ToString()
    {
        return Length.ToString();
    }

    protected override void AddHistory(OperationsType op, long principalMemoryAddress, bool wasHit = true, long? saveOnAddress = null)
    {
        var nextIndex = History.Count;
        History.Add(new OperationHistory(nextIndex, op, wasHit, principalMemoryAddress, saveOnAddress));
    }
}