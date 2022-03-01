using WordleSolver.common;
using WordleSolver.Search;

// See https://aka.ms/new-console-template for more information
var start = DateTime.Now;
Console.WriteLine("Building Node for TESTS.... wich me luck...");

var node = BruteForce.FromFirstGuess(new Word("TESTS"));

Console.WriteLine($"Wow that took {DateTime.Now - start}, good work everyone...");

 