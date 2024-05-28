using System;
using System.Collections.Generic;
using CacheSimulator.Tokens.Enums;
using CacheSimulator.Tokens.Models;

namespace CacheSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o tamanho da cache:");
            long tamanhoCache = long.Parse(Console.ReadLine());

            DirectCache cache = new DirectCache(tamanhoCache);


            List<long> posicoesDeMemoria = new List<long> { 0, 1, 2 ,3 ,1, 4, 5, 6 };

            int totalAcessos = 0;

            foreach (var posicao in posicoesDeMemoria)
            {
                totalAcessos++;

                bool foiHit = cache.SaveAddress(posicao);
                long posicaoCache = posicao % tamanhoCache;
               
                Console.WriteLine($"Endereço de Memória: {posicao}, {(foiHit ? "Hit" : "Miss")}");
                PrintCache(cache, tamanhoCache);
                Console.WriteLine();
            }


            PrintResults(cache, totalAcessos);
        }

        static void PrintCache(DirectCache cache, long cacheSize)
        {

            Console.WriteLine("\nConteúdo da Cache:");
            Console.WriteLine("Posição da Cache | Posição de Memória");
            for (long i = 0; i < cacheSize; i++)
            {
                
                if (cache.GetMemoryMap().TryGetValue(i, out var value))
                {
                    Console.WriteLine($"        {i}        |        {value.Item2}        ");
                }
                else
                {
                    Console.WriteLine($"        {i}        |        (vazio)        ");
                }
            }
        }
        static void PrintResults(DirectCache cache, int totalAcessos)
        {
            int totalHits = cache.GetTotalHits();
            int totalMisses = cache.GetTotalMisses();
            double taxaDeAcerto = (double)totalHits / totalAcessos * 100;

            Console.WriteLine("\nConectividade em Sistemas Ciberfisicos - Mapemamento Direto - Rafael e Victor:");
            Console.WriteLine("\n------------------------------------------------------------------------------");
            Console.WriteLine($"Total de Acessos à Memória: {totalAcessos}");
            Console.WriteLine($"Total de Hits: {totalHits}");
            Console.WriteLine($"Total de Misses: {totalMisses}");
            Console.WriteLine($"Taxa de Acerto de Hits: {taxaDeAcerto:F2}%");
        }
    }
}