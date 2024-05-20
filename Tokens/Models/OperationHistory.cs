using CacheSimulator.Tokens.Enums;

namespace CacheSimulator.Tokens.Models;

public class OperationHistory(int index, OperationsType type, bool wasHit, long address, long? savedOnAddress)
{
    public int Index { get; set; } = index;
    public OperationsType Type { get; set; } = type;
    public bool WasHit { get; set; } = wasHit;
    public long Address { get; set; } = address;
    public long? SavedOnAddress { get; set; } = savedOnAddress;
}