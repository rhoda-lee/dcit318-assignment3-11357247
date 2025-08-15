using System;
using System.Collections.Generic;
using System.IO;

public class StudentResultProcessor
{
    public List<Student> ReadStudentsFromFile(string inputFilePath)
    {
        var students = new List<Student>();

        using var sr = new StreamReader(inputFilePath);
        int lineNo = 0;
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            lineNo++;
            var parts = line.Split(',', StringSplitOptions.TrimEntries);
            if (parts.Length < 3)
                throw new MissingFieldException($"Line {lineNo}: missing fields.");

            if (!int.TryParse(parts[0], out var id))
                throw new InvalidScoreFormatException($"Line {lineNo}: ID '{parts[0]}' is not a valid integer.");

            var name = parts[1];
            if (!int.TryParse(parts[2], out var score))
                throw new InvalidScoreFormatException($"Line {lineNo}: Score '{parts[2]}' is not a valid integer.");

            students.Add(new Student(id, name, score));
        }

        return students;
    }

    public void WriteReportToFile(List<Student> students, string outputFilePath)
    {
        using var sw = new StreamWriter(outputFilePath);
        foreach (var s in students)
        {
            sw.WriteLine($"{s.FullName} (ID: {s.Id}): Score = {s.Score}, Grade = {s.GetGrade()}");
        }
    }
}
