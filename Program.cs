using System;
using System.Collections.Generic;
using CacheSimulator.Tokens.Enums;
using CacheSimulator.Tokens.Models;

namespace CacheSimulator
{
    class Program
    {
        private static long TamanhoCache { get; set; }
        private static long TotalBlocos { get; set; } = 2;
        private static long TamanhoBlocos { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Digite o tamanho da cache:");
            TamanhoCache = long.Parse(Console.ReadLine());
            TamanhoBlocos = TamanhoCache / TotalBlocos;

            Cache cache = new AssociativeCache(TamanhoCache, TotalBlocos);


            List<long> posicoesDeMemoria = new List<long> { 33, 3, 11, 10, 5, 11, 9, 9, 12, 6 };

            int totalAcessos = 0;

            foreach (var posicao in posicoesDeMemoria)
            {
                var foiHit = cache.LoadAddress(posicao);

               if (!foiHit)  totalAcessos++;

                Console.WriteLine($"Endereço de Memória: {posicao}, {(foiHit ? "Hit" : "Miss")}");
                PrintCache(cache, TamanhoCache);
                Console.WriteLine();
            }


            PrintResults(cache, totalAcessos);
        }

        static void PrintCache(Cache cache, long cacheSize)
        {

            Console.WriteLine("\nConteúdo da Cache:");
            Console.WriteLine("Posição da Cache | Posição de Memória");
            for (long i = 0; i < cacheSize; i++)
            {
                var newBloco = i % TamanhoBlocos;

                if (newBloco == 0)
                {
                    Console.WriteLine($"-------------------------------------------------");
                }

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

        static void PrintResults(Cache cache, int totalAcessos)
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