using System.Collections.Concurrent;
using System.Data.Common;
using System.Diagnostics;

namespace Day_9;

public class Solver
{
    private struct File(int id, int index, bool isEmpty)
    {
        public int Id { get; set; } = id;
        public int Index { get; set; } = index;
        public bool IsEmpty { get; set; } = isEmpty;
        public long Checksum => Id * Index;
    }
    
    public static long Run_PartOne(string input)
    {
        // Parse string
        var numbers = input.Select(c => int.Parse(c.ToString())).ToList();

        // Generate filestructure
        List<File> files = new List<File>();
        for (int i = 0; i < numbers.Count; i++)
        {
            var count = numbers[i];
            
            for (int j = 0; j < count; j++)
            {
                files.Add(new File(i / 2, i, i % 2 != 0));
            }
        }

        // Rearrange files
        bool isDone = false;
        int startIndex = 0;
        for (int i = files.Count - 1; i > 0 && !isDone; i--)
        {
            var fileA = files[i];
            if (fileA.IsEmpty) continue;
            
            for (int j = startIndex; j < files.Count; j++)
            {
                var fileB = files[j];
                if (!fileB.IsEmpty) continue;
                if (j > i)
                {
                    isDone = true;
                    break;
                }

                files[i] = new File(fileB.Id, i, fileB.IsEmpty);
                files[j] = new File(fileA.Id, j, fileA.IsEmpty);
                startIndex = j + 1;
                break;
            }
            if (isDone) break;
        }
        
        // Re-index files
        long sum = 0L;
        for (int i = 0; i < files.Count; i++)
        {
            files[i] = new File(files[i].Id, i, files[i].IsEmpty);
                       
            if(files[i].IsEmpty) continue;
            
            sum += files[i].Checksum;
        }

        // Validate checksum
        return sum;
    }
    
    public static long Run_PartTwo(string input)
    {
        // Parse string
        var numbers = input.Select(c => int.Parse(c.ToString())).ToList();
        
        // Generate filestructure
        List<File> files = new List<File>();
        for (int i = 0; i < numbers.Count; i++)
        {
            var count = numbers[i];
            
            for (int j = 0; j < count; j++)
            {
                files.Add(new File(i / 2, i, i % 2 != 0));
            }
        }
        
        // Rearrange files
        for (int i = files.Count - 1; i > 0; i--)
        {
            var fileA = files[i];
            if (fileA.IsEmpty)
            {
                continue;
            }
           
            // Get size for files
            var numberOfFiles = 0;
            for (int j = i; j > 0; j--)
            {
                if (files[j].Id != fileA.Id)
                {
                    break;
                }
                
                numberOfFiles++;
            }

            // Make sure files fit
            bool fileFits = false;
            int fitIndex = 0;
            for (int j = 0; j < files.Count && j < i; j++)
            {
                if (!files[j].IsEmpty) continue;
                
                int emptyFileCount = 1;
                for (var k = j + 1; k < numberOfFiles + j && k < files.Count; k++)
                {
                    if (!files[k].IsEmpty)
                    {
                        break;
                    }
                    emptyFileCount++;
                    if (emptyFileCount == numberOfFiles) break;
                }

                if (emptyFileCount != numberOfFiles)
                {
                    j += emptyFileCount - 1;
                    continue;
                }
                
                fileFits = true;
                fitIndex = j;
                break;
            }
            if (!fileFits)
            {
                i -= numberOfFiles - 1;
                continue;
            }

            // Move files
            int rightIndex = i;
            for (int j = fitIndex; j < fitIndex + numberOfFiles; j++)
            {
                var fileB = files[j];
                
                files[rightIndex] = new File(fileB.Id, rightIndex, fileB.IsEmpty);
                files[j] = new File(fileA.Id, j, fileA.IsEmpty);
                rightIndex--;
            }
            i -= numberOfFiles - 1;
        }
       
        // Re-index files
        long sum = 0L;
        for (int i = 0; i < files.Count; i++)
        {
            files[i] = new File(files[i].Id, i, files[i].IsEmpty);
            
            if(files[i].IsEmpty) continue;
            
            sum += files[i].Checksum;
        }
     
        // Validate checksum
        return sum;
    }
}