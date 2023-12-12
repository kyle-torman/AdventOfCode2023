// See https://aka.ms/new-console-template for more information
var parser = new PipeMapParser(PipeFiles.PuzzleInput);
var pipeMap = parser.ParsePipeMap();
var pipeLoop = pipeMap.GetPipeLoop();
Console.WriteLine($"Farthest Distance: {pipeLoop.Count / 2}");
