using CacheSimulator.Tokens.Enums;

namespace CacheSimulator.Tokens.Models;

public class AssociativeCache: Cache
{
    public override CacheType Type { get; } = CacheType.Associative;
    protected override  long Length { get; }
    private long TotalSets { get; }
    private long SetsLength { get; }

    public AssociativeCache(long length, long totalSets)
    {
        Length = length;
        TotalSets = totalSets;
        SetsLength = length / totalSets;

        long currentSet = 0;
        while (currentSet < totalSets)
        {
            MemoryLRU.Add(currentSet, new List<long>());
            currentSet++;
        }
    }

    protected override Dictionary<long, (bool, long)> MemoryMap { get; set; } = new();
    private Dictionary<long, List<long>> MemoryLRU { get; set; } = new();

    public override bool LoadAddress(long memoryAdrress)
    {
        var set = memoryAdrress % TotalSets;
        var firstMemoryAddr = set * SetsLength;
        var lastMemoryAddr = firstMemoryAddr + SetsLength - 1;

        var lru = MemoryLRU[set];

        long? cacheAddress = null;
        var currentAddress = firstMemoryAddr;
        var wasHit = false;

        while (currentAddress <= lastMemoryAddr)
        {
            var hasValue = MemoryMap.TryGetValue(currentAddress, out var currentMemoryAddres);
            if (!hasValue)
            {
                cacheAddress = currentAddress;
                lru.Add(currentAddress);
                break;
            };

            if (currentMemoryAddres.Item2 == memoryAdrress)
            {
                wasHit = true;
                lru.RemoveAt(lru.FindIndex(a => a == currentAddress));
                lru.Add(currentAddress);
                cacheAddress = currentAddress;
                break;
            }
            currentAddress++;
        }

        if (cacheAddress == null)
        {
            currentAddress = firstMemoryAddr;
            while (currentAddress <= lastMemoryAddr)
            {
                if (lru.First() == currentAddress)
                {
                    cacheAddress = currentAddress;
                    lru.RemoveAt(0);
                    lru.Add(currentAddress);
                    break;
                }
                currentAddress++;
            }
        }

        MemoryLRU[set] = lru;

        if (cacheAddress == null) throw new Exception("Deu ruim a lógica");

        if (wasHit)
        {
            TotalHits++;
        }
        else
        {
            TotalMisses++;
            MemoryMap[cacheAddress.Value] = (false, memoryAdrress);
        }

        return wasHit;
    }

    public override bool SaveAddress(long memoryAdrress)
    {
        throw new Exception("Nao usa");
    }

    
    public Dictionary<long, (bool, long)> GetMemoryMap()
    {
        return MemoryMap;
    }
    public override string ToString()
    {
        return Length.ToString();
    }
}