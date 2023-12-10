// See https://aka.ms/new-console-template for more information
var parser = new SensorReportParser(SensorReportFiles.PuzzleInput);
var report = parser.ParseSensorReport();
Console.WriteLine($"Last Predicted Sum: {report.SumOfExtrapolatedValues(isBackwards: false)}");
Console.WriteLine($"First Predicted Sum: {report.SumOfExtrapolatedValues(isBackwards: true)}");

