// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

var calibrationValueParser = new CalibrationValueParser(new CalibrationDocument());
var calibrationValues = calibrationValueParser.GetCalibrationValues();
Console.WriteLine($"Number of values: {calibrationValues.Count}");
Console.WriteLine($"Sum of calibration values: {calibrationValues.Sum()}");
// for(var i = 0; i < calibrationValues.Count; i++)
// {
//     Console.WriteLine($"Index: {i} - {calibrationValues[i]}");
// }
