using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
        #region Меню
        Menu:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("==========================МЕНЮ==========================");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n1)Информация о дисках  \n2)Перейти в директорию  \n3)Информация о файле/директории  \n4)Создать файл  \n5)Открыть файл \n6)Записать данные в файл \n7)Выход");
            Console.ResetColor();

            int Menu = 0;
            int FileType;
            int DirCreate;
            int ContinueDir;

            string dirName = null;
            string FileName = null;
            string Type = null;
            string TextInFile = null;

            DriveInfo[] drives;

            while (Menu == 0)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nВведите цифру: ");
                    Menu = Convert.ToInt32(Console.ReadLine());
                    Console.ResetColor();
                }
                catch (FormatException FE)
                {
                    Console.ResetColor();
                    Console.WriteLine(FE.Message);
                }
            }
            #endregion

            switch (Menu)
            {
                #region Информация о дисках
                case 1:
                    drives = DriveInfo.GetDrives();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Список дисков: ");
                    Console.ResetColor();

                    foreach (DriveInfo drive in drives)
                    {
                        Console.Write($"{drive.Name} \nТип: {drive.DriveType} \nГотовность диска: ");

                        if (drive.IsReady == true)
                            Console.WriteLine("готов\n");
                        else
                            Console.WriteLine("не готов\n");
                    }
                    Console.WriteLine();

                    goto Menu;
                #endregion

                #region Перейти в директорию
                case 2:
                Dir:
                    if (dirName == null)
                    {
                        drives = DriveInfo.GetDrives();

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Доступные директории: ");
                        Console.ResetColor();

                        foreach (DriveInfo drive in drives)
                        {
                            Console.WriteLine(drive.Name);
                            Console.WriteLine();
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Введите директорию: ");
                    dirName = Console.ReadLine();
                    Console.ResetColor();

                    if (Directory.Exists(dirName))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nПодкаталоги:");
                        Console.ResetColor();
                        string[] dirs = Directory.GetDirectories(dirName);
                        foreach (string s in dirs)
                            Console.WriteLine(s);

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nФайлы:");
                        Console.ResetColor();
                        string[] files = Directory.GetFiles(dirName);
                        foreach (string s in files)
                            Console.WriteLine(s);
                    }
                    else
                        Console.WriteLine("\nНазвание директории введено неверно.");



                    ExitOrContinue:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\n1)Выйти\t2)Продолжить: ");
                        ContinueDir = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException FE)
                    {
                        Console.WriteLine($"\n{FE.Message}");
                        goto ExitOrContinue;
                    }

                    switch (ContinueDir)
                    {
                        case 1:
                            Console.WriteLine();
                            goto Menu;
                        case 2:
                            Console.WriteLine();
                            goto Dir;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nВыберите из предложенных вариантов.");
                            goto ExitOrContinue;
                    }
                #endregion

                #region Информация о файле/дериктории
                case 3:
                Info:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Введите директорию или путь к файлу: ");
                    dirName = Console.ReadLine();
                    Console.ResetColor();

                    if (Directory.Exists(dirName))
                    {
                        DirectoryInfo dirInf = new DirectoryInfo(dirName);
                        Console.WriteLine($"Название каталога: {dirInf.Name} \tвремя создания: {dirInf.CreationTime}");
                    }
                    else if (File.Exists(dirName))
                    {
                        FileInfo fileInf = new FileInfo(dirName);
                        Console.WriteLine($"Имя файла: {fileInf.Name} \tвремя создания: {fileInf.CreationTime} \tразмер: {fileInf.Length} байт");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nНазвание директории/путь к файлу введены неверно.");
                        Console.ResetColor();
                    }
                    Console.WriteLine();

                Exit:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\n1)Выйти\t2)Продолжить: ");
                        ContinueDir = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException FE)
                    {
                        Console.WriteLine($"\n{FE.Message}");
                        goto Exit;
                    }

                    switch (ContinueDir)
                    {
                        case 1:
                            Console.WriteLine();
                            goto Menu;
                        case 2:
                            Console.WriteLine();
                            goto Info;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nВыберите из предложенных вариантов.");
                            goto Exit;
                    }
                #endregion

                #region Создать файл
                case 4:
                Start:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Введите директорию, где будет создан ваш файл: ");
                Directory:
                    dirName = Console.ReadLine();
                    Console.ResetColor();

                DirCreate:
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                        if (!dirInfo.Exists)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\nДиректория не найдена. Создать? - 1)Да 2)Нет: ");
                            DirCreate = Convert.ToInt32(Console.ReadLine());
                            Console.ResetColor();

                            switch (DirCreate)
                            {
                                case 1:
                                    Directory.CreateDirectory(dirName);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Директория создана.");
                                    Console.ResetColor();
                                    break;
                                case 2:
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Введите дирректорию повторно: ");
                                    Console.ResetColor();
                                    goto Directory;
                                default:
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Выберите из предложенных вариантов.");
                                    Console.ResetColor();
                                    goto DirCreate;
                            }
                        }

                        Console.Write("\nВыбеите тип файла, который хотите создать: 1)txt 2)csv: ");
                        FileType = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException FE)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"\n{FE.Message}\n");
                        Console.ResetColor();
                        goto Directory;
                    }
                    catch (ArgumentException AE)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"\n{AE.Message}\n");
                        Console.ResetColor();
                        goto Start;
                    }

                    switch (FileType)
                    {
                        case 1:
                            Type = "txt";
                            break;
                        case 2:
                            Type = "csv";
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Выберите один из двух типов файлов.\n");
                            goto DirCreate;
                    }

                    Console.Write("Введите название файла: ");
                    FileName = Console.ReadLine();
                    using (new FileStream($@"{dirName}\{FileName}.{Type}", FileMode.Create))
                        Console.WriteLine($"{Type} файл создан.", Console.ForegroundColor);
                    Console.ResetColor();
                    Console.WriteLine();

                Exi:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\n1)Выйти\t2)Продолжить: ");
                        ContinueDir = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException FE)
                    {
                        Console.WriteLine($"\n{FE.Message}");
                        goto Exi;
                    }

                    switch (ContinueDir)
                    {
                        case 1:
                            Console.WriteLine();
                            goto Menu;
                        case 2:
                            Console.WriteLine();
                            goto Start;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nВыберите из предложенных вариантов.");
                            goto Exi;
                    }
                #endregion

                #region Открыть файл
                case 5:
                Open:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("Введите директорию: ");
                        dirName = Console.ReadLine();
                        Console.ResetColor();

                        if (Directory.Exists(dirName))
                        {
                            string[] files = Directory.GetFiles(dirName, "*.*", SearchOption.TopDirectoryOnly)
                             .Where(x => x.EndsWith(".txt") | x.EndsWith(".csv")).ToArray();

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nФайлы: ");
                            Console.ResetColor();

                            foreach (string s in files)
                            {
                                if (dirName.EndsWith(@"\"))
                                    Console.WriteLine(s.Replace(dirName, ""));
                                else
                                    Console.WriteLine(s.Replace(dirName + @"\", ""));
                            }

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\nВведите название файла, который хотите открыть, вместе с его типом: ");
                            FileName = Console.ReadLine();
                            Console.ResetColor();

                            using (FileStream fstream = File.OpenRead($"{dirName}\\{FileName}"))
                            {
                                byte[] ArrayText = new byte[fstream.Length];
                                fstream.Read(ArrayText, 0, ArrayText.Length);
                                string textFromFile = Encoding.Default.GetString(ArrayText);
                                Console.WriteLine($"\nТекст из файла: {textFromFile}\n");
                            }
                        }
                        else
                        {
                            using (FileStream fstream = File.OpenRead($"{dirName}"))
                            {
                                byte[] ArrayText = new byte[fstream.Length];
                                fstream.Read(ArrayText, 0, ArrayText.Length);
                                string textFromFile = Encoding.Default.GetString(ArrayText);
                                Console.WriteLine($"\nТекст из файла: {textFromFile}\n");
                            }
                        }
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Директория введена некорректно.\n");
                    }
                    catch (FileNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Файл не найден.\n");
                    }
                    catch (NotSupportedException NSE)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(NSE.Message);
                    }
                    catch (FormatException FE)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(FE.Message);
                    }
                    Console.ResetColor();

                Ex:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\n1)Выйти\t2)Продолжить: ");
                        ContinueDir = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException FE)
                    {
                        Console.WriteLine($"\n{FE.Message}");
                        goto Ex;
                    }

                    switch (ContinueDir)
                    {
                        case 1:
                            Console.WriteLine();
                            goto Menu;
                        case 2:
                            Console.WriteLine();
                            goto Open;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nВыберите из предложенных вариантов.");
                            goto Ex;
                    }
                #endregion

                #region Записать данные в файл
                case 6:
                Res:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("Введите директорию: ");
                        dirName = Console.ReadLine();
                        Console.ResetColor();

                        if (Directory.Exists(dirName))
                        {
                            string[] files = Directory.GetFiles(dirName, "*.*", SearchOption.TopDirectoryOnly)
                             .Where(x => x.EndsWith(".txt") | x.EndsWith(".csv")).ToArray();

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nФайлы: ");
                            Console.ResetColor();

                            foreach (string s in files)
                            {
                                if (dirName.EndsWith(@"\"))
                                    Console.WriteLine(s.Replace(dirName, ""));
                                else
                                    Console.WriteLine(s.Replace(dirName + @"\", ""));
                            }

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("\nВведите название файла, в который вы хотите записать данные, вместе с его типом: ");
                            FileName = Console.ReadLine();
                            Console.ResetColor();

                            using (FileStream fstream = new FileStream($@"{dirName}\{FileName}", FileMode.OpenOrCreate))
                            {
                                Console.Write("Введите текст: ");
                                TextInFile = Console.ReadLine();
                                byte[] array = Encoding.Default.GetBytes(TextInFile);
                                fstream.Write(array, 0, array.Length);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("\nТекст записан в файл.\n");
                            }
                        }
                        else
                        {
                            using (FileStream fstream = new FileStream(dirName, FileMode.OpenOrCreate))
                            {
                                Console.Write("Введите текст: ");
                                TextInFile = Console.ReadLine();
                                byte[] array = Encoding.Default.GetBytes(TextInFile);
                                fstream.Write(array, 0, array.Length);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("\nТекст записан в файл.\n");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"\n{ex.Message}\n");
                        goto Res;
                    }

                    Console.ResetColor();

                E:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\n1)Выйти\t2)Продолжить: ");
                        ContinueDir = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException FE)
                    {
                        Console.WriteLine($"\n{FE.Message}");
                        goto E;
                    }

                    switch (ContinueDir)
                    {
                        case 1:
                            Console.WriteLine();
                            goto Menu;
                        case 2:
                            Console.WriteLine();
                            goto Res;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nВыберите из предложенных вариантов.");
                            goto E;
                    }
                #endregion

                #region Выход
                case 7:
                    Environment.Exit(0);
                    goto Menu;
                #endregion

                #region Ошибка
                default:
                    Console.WriteLine("Попробуйте ввести цифру ещё раз.");
                    goto Menu;
                    #endregion
            }
        }
    }
}