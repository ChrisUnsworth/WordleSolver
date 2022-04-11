using WordleSolver.common;
using WordleSolver.Search;
using WordleSolver.Strategy;

var start = DateTime.Now;

var result = Widest.GetStrategy();

Console.WriteLine($"Wow that took {DateTime.Now - start} max depth = {Evaluator.MaxDepth(result)}");

