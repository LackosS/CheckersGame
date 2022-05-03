using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using checker_game.Model;

namespace checker_game.Persistence
{
    /// <summary>
    /// Type of file manager
    /// </summary>
    public class ModelDataAccess : IModelDataAccess
    {
        /// <summary>
        /// File loading.
        /// </summary>
        public async Task<ModelTable> LoadAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line = await reader.ReadLineAsync();
                    String[] numbers = line.Split(' ');
                    Int32 tableSize = Int32.Parse(numbers[0]);
                    Int32 actualPlayer = Int32.Parse(numbers[1]);
                    Int32 white = Int32.Parse(numbers[2]);
                    Int32 black = Int32.Parse(numbers[3]);
                    ModelTable table = new ModelTable(tableSize);
                    table.White = white;
                    table.Black = black;
                    if (actualPlayer == 1) { table.ActualPlayer = Player.PlayerWhite; } else { table.ActualPlayer = Player.PlayerBlack; }
                    for (Int32 i = 0; i < tableSize; i++)
                    {
                        line = await reader.ReadLineAsync();
                        numbers = line.Split(' ');

                        for (Int32 j = 0; j < tableSize; j++)
                        {
                            //for (Int32 k = 0; k < tableSize; k++)
                            //{
                                table.SetValue(i, j, Int32.Parse(numbers[j]), Int32.Parse(numbers[j+1]));
                            //}
                        }
                    }
                    return table;
                }
            }
            catch
            {
                throw new ModelDataException();
            }
        }

        /// <summary>
        /// File saving.
        /// </summary>
        public async Task SaveAsync(String path, ModelTable table)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    Int32 actualPlayer;
                    if(table.ActualPlayer == Player.PlayerWhite) { actualPlayer = 1; } else { actualPlayer = 2; }
                    writer.Write(table.Size + " "+actualPlayer + " "+ table.White+ " " + table.Black+" "+ "\n");
                    for (Int32 i = 0; i < table.Size; i++)
                    {
                        for (Int32 j = 0; j < table.Size; j++)
                        {
                            await writer.WriteAsync(table[i, j] + " " + table.GetLevel(i,j)+" ");
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new ModelDataException();
            }
        }
    }
}
