// See https://aka.ms/new-console-template for more information
var schematicParser = new EngineSchematicParser(EngineSchematicFiles.PuzzleInput);
var engineSchematic = schematicParser.ParseEngineSchematic();
var schematicPartNumbers = engineSchematic.GetSchematicPartNumbers();
var schematicGears = engineSchematic.GetSchematicGears();
Console.WriteLine($"Schematic Gear Ratio Sum: {schematicGears.Sum(gear => gear.GearRatio)}");
Console.WriteLine($"Part Number Sum: {schematicPartNumbers.Sum(part => part.Value)}");
Console.WriteLine($"Part Numbers: {string.Join(",", schematicPartNumbers.Select(part => part.Value))}");

