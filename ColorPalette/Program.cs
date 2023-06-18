using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

using System.Linq;

using System.Drawing;

namespace ColorPalette
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                if (!File.Exists(args[0]))
                {
                    Console.WriteLine("The file does not exist.\n");

                    Console.Write("Press key...");

                    Console.ReadKey();

                    return;
                }

                //MainAlgorithm(args[0]);

                AlgorithmAlt(args[0]);
            }
            else
            {
                Console.WriteLine("Missing arguments.\n");

                Console.Write("Press key...");

                Console.ReadKey();
            }
        }

        //

        public static void StatusProcessingShow(int in_x, int in_y, ref int in_i, ref int in_j, int in_width, int in_totalPixel)
        {
            while (true)
            {
                Console.SetCursorPosition(in_x, in_y);

                Console.Write("{0}/{1}", in_j * in_width + in_i + 1, in_totalPixel);

                Thread.Sleep(300);
            }
        }

        public static void MainAlgorithm(string name_file)
        {
            // BMP, GIF, EXIF, JPG, PNG and TIFF.

            try
            {
                using (Bitmap _file = new Bitmap(name_file))
                {
                    Console.WriteLine("Image: {0}.", Path.GetFileName(name_file));

                    Console.WriteLine("====================");

                    Console.WriteLine("Resolution: {0} x {1} pixels.", _file.Width, _file.Height);

                    Console.WriteLine("====================");

                    Stopwatch _time = new Stopwatch();

                    _time.Start();

                    //

                    List<Color> _array = new List<Color>();

                    Console.Write("Processing: ");

                    int x = Console.CursorLeft;

                    int y = Console.CursorTop;

                    int i = 0;

                    int j = 0;

                    Thread _thread = new Thread(() => StatusProcessingShow(x, y, ref i, ref j, _file.Width, _file.Width * _file.Height));

                    _thread.Start();

                    //

                    switch (Path.GetExtension(name_file))
                    {
                        case ".jpg":
                        case ".jpeg":
                            {
                                int c = -1;

                                for (j = 0; j < _file.Height; j++)
                                {
                                    for (i = 0; i < _file.Width; i++)
                                    {
                                        Color t = _file.GetPixel(i, j);

                                        c = -1;

                                        // #RGB

                                        for (int k = 0; k < _array.Count; k++)
                                        {
                                            if (t.R == _array[k].R && t.G == _array[k].G && t.B == _array[k].B)
                                            {
                                                c = -7;

                                                break;
                                            }
                                            else
                                            {
                                                if (t.ToArgb() < _array[k].ToArgb())
                                                {
                                                    c = k;

                                                    break;
                                                }
                                            }
                                        }

                                        if (c != -7)
                                        {
                                            if (c != -1)
                                            {
                                                _array.Insert(c, t);
                                            }
                                            else
                                            {
                                                _array.Add(t);
                                            }
                                        }
                                    }
                                }


                            }
                            break;
                        default:
                            {
                                int c = -1;

                                for (j = 0; j < _file.Height; j++)
                                {
                                    for (i = 0; i < _file.Width; i++)
                                    {
                                        Color t = _file.GetPixel(i, j);

                                        c = -1;

                                        // #ARGB

                                        for (int k = 0; k < _array.Count; k++)
                                        {
                                            if (t.A == _array[k].A && t.R == _array[k].R && t.G == _array[k].G && t.B == _array[k].B)
                                            {
                                                c = -7;

                                                break;
                                            }
                                            else
                                            {
                                                if (t.ToArgb() < _array[k].ToArgb())
                                                {
                                                    c = k;

                                                    break;
                                                }
                                            }
                                        }

                                        if (c != -7)
                                        {
                                            if (c != -1)
                                            {
                                                _array.Insert(c, t);
                                            }
                                            else
                                            {
                                                _array.Add(t);
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                    }

                    _thread.Abort();

                    Console.SetCursorPosition(x, y);

                    int _count = _file.Width * _file.Height;

                    Console.WriteLine("{0}/{1} Process completed.", _count, _count);

                    Console.WriteLine("====================");

                    //

                    string name = Path.GetFileNameWithoutExtension(name_file) + ".txt";

                    StreamWriter streamWriter = new StreamWriter(name);

                    foreach (Color n in _array)
                    {
                        streamWriter.WriteLine(n);
                    }

                    streamWriter.Close();

                    Console.WriteLine("Result saved to file: {0}\n", name);

                    //

                    _time.Stop();

                    _array.Clear();

                    Console.WriteLine("Elapsed time: {0} sec.\n", TimeSpan.FromMilliseconds(_time.ElapsedMilliseconds).TotalSeconds);
                }

                //

                Console.Write("Press key...");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}\n", ex.Message);

                Console.Write("Press key...");

                Console.ReadKey();
            }
        }

        //

        public static void AlgorithmAlt(string name_file)
        {
            try
            {
                using (Bitmap _file = new Bitmap(name_file))
                {
                    Console.WriteLine("Image: {0}.", Path.GetFileName(name_file));

                    Console.WriteLine("====================");

                    Console.WriteLine("Resolution: {0} x {1} pixels.", _file.Width, _file.Height);

                    Console.WriteLine("====================");

                    Stopwatch _time = new Stopwatch();

                    _time.Start();

                    //

                    List<Color> _array = new List<Color>();

                    Console.Write("Processing: ");

                    int x = Console.CursorLeft;

                    int y = Console.CursorTop;

                    int i = 0;

                    int j = 0;

                    Thread _thread = new Thread(() => StatusProcessingShow(x, y, ref i, ref j, _file.Width, _file.Width * _file.Height));

                    _thread.Start();

                    for (j = 0; j < _file.Height; j++)
                    {
                        for (i = 0; i < _file.Width; i++)
                        {
                            _array.Add(_file.GetPixel(i, j));
                        }
                    }

                    _thread.Abort();

                    Console.SetCursorPosition(x, y);

                    int _count = _file.Width * _file.Height;

                    Console.WriteLine("{0}/{1} Process completed.", _count, _count);

                    //

                    Console.Write("Removing duplicates");

                    x = Console.CursorLeft;

                    y = Console.CursorTop;

                    Console.Write("...");

                    _array = new HashSet<Color>(_array).ToList();

                    Console.SetCursorPosition(x, y);

                    Console.WriteLine(". Process completed.");

                    //

                    Console.Write("Array sorting");

                    x = Console.CursorLeft;

                    y = Console.CursorTop;

                    Console.Write("...");

                    SortColor _sortColor = new SortColor();

                    _array.Sort(_sortColor);

                    Console.SetCursorPosition(x, y);

                    Console.WriteLine(". Process completed.");

                    Console.WriteLine("====================");

                    //

                    string name = Path.GetFileNameWithoutExtension(name_file) + ".txt";

                    StreamWriter streamWriter = new StreamWriter(name);

                    foreach (Color n in _array)
                    {
                        streamWriter.WriteLine(n);
                    }

                    streamWriter.Close();

                    Console.WriteLine("Result saved to file: {0}\n", name);

                    //

                    _time.Stop();

                    _array.Clear();

                    Console.WriteLine("Elapsed time: {0} sec.\n", TimeSpan.FromMilliseconds(_time.ElapsedMilliseconds).TotalSeconds);
                }

                //

                Console.Write("Press key...");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}\n", ex.Message);

                Console.Write("Press key...");

                Console.ReadKey();
            }
        }

        public class SortColor : IComparer<Color>
        {
            public int Compare(Color in_x, Color in_y)
            {
                if (in_x == null || in_y == null)
                {
                    return 0;
                }

                return in_x.ToArgb().CompareTo(in_y.ToArgb());
            }
        }
    }
}