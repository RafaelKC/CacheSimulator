﻿using CacheSimulator.Tokens.Enums;
using CacheSimulator.Tokens.Models;

Console.Write("Enter Cache length: ");
string? input = Console.ReadLine();
var isNumber = Int32.TryParse(input, out var number);

if (isNumber)
{
    var cache = new Cache(CacheType.Direct, number);
    Console.WriteLine(cache);
}
