using System;
using System.IO;

namespace FileOperations
{
    class FileManager
    {
        public void CreateFile(string path, string content)
        {
            File.WriteAllText(path, content);
            Console.WriteLine($"Файл создан: {path}");
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine($"Файл удален: {path}");
            }
            else
            {
                Console.WriteLine($"Ошибка: файл {path} не существует");
            }
        }

        public void CopyFile(string sourcePath, string destPath)
        {
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, destPath, true);
                Console.WriteLine($"Файл скопирован: {sourcePath} -> {destPath}");
            }
            else
            {
                Console.WriteLine($"Ошибка: исходный файл {sourcePath} не существует");
            }
        }

        public void MoveFile(string sourcePath, string destPath)
        {
            if (File.Exists(sourcePath))
            {
                File.Move(sourcePath, destPath);
                Console.WriteLine($"Файл перемещен: {sourcePath} -> {destPath}");
            }
            else
            {
                Console.WriteLine($"Ошибка: файл {sourcePath} не существует");
            }
        }

        public void RenameFile(string oldPath, string newPath)
        {
            if (File.Exists(oldPath))
            {
                File.Move(oldPath, newPath);
                Console.WriteLine($"Файл переименован: {oldPath} -> {newPath}");
            }
            else
            {
                Console.WriteLine($"Ошибка: файл {oldPath} не существует");
            }
        }

        public void DeleteFilesByPattern(string directory, string pattern)
        {
            if (Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory, pattern);
                foreach (string file in files)
                {
                    File.Delete(file);
                    Console.WriteLine($"Удален: {file}");
                }
                Console.WriteLine($"Удалено файлов: {files.Length}");
            }
            else
            {
                Console.WriteLine($"Ошибка: директория {directory} не существует");
            }
        }

        public void ListFiles(string directory)
        {
            if (Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory);
                Console.WriteLine($"\nФайлы в директории {directory}:");
                foreach (string file in files)
                {
                    Console.WriteLine($"  {Path.GetFileName(file)}");
                }
            }
            else
            {
                Console.WriteLine($"Ошибка: директория {directory} не существует");
            }
        }

        public void SetReadOnly(string path, bool readOnly)
        {
            if (File.Exists(path))
            {
                File.SetAttributes(path, readOnly ? FileAttributes.ReadOnly : FileAttributes.Normal);
                Console.WriteLine($"Файл {path}: {(readOnly ? "только чтение" : "доступна запись")}");
            }
        }
    }

    class FileInfoProvider
    {
        public void GetFileInfo(string path)
        {
            if (File.Exists(path))
            {
                FileInfo info = new FileInfo(path);
                Console.WriteLine($"\nИнформация о файле: {path}");
                Console.WriteLine($"  Размер: {info.Length} байт");
                Console.WriteLine($"  Дата создания: {info.CreationTime}");
                Console.WriteLine($"  Дата последнего изменения: {info.LastWriteTime}");
            }
            else
            {
                Console.WriteLine($"Файл {path} не существует");
            }
        }

        public long GetFileSize(string path)
        {
            if (File.Exists(path))
            {
                return new FileInfo(path).Length;
            }
            return -1;
        }

        public void CompareFiles(string path1, string path2)
        {
            long size1 = GetFileSize(path1);
            long size2 = GetFileSize(path2);

            if (size1 == -1)
                Console.WriteLine($"Файл {path1} не существует");
            else if (size2 == -1)
                Console.WriteLine($"Файл {path2} не существует");
            else
            {
                if (size1 == size2)
                    Console.WriteLine($"Файлы одинакового размера: {size1} байт");
                else if (size1 > size2)
                    Console.WriteLine($"{path1} больше {path2} на {size1 - size2} байт");
                else
                    Console.WriteLine($"{path2} больше {path1} на {size2 - size1} байт");
            }
        }

        public void CheckFilePermissions(string path)
        {
            if (File.Exists(path))
            {
                bool canRead = false;
                bool canWrite = false;
                bool canExecute = false;

                try
                {
                    using (FileStream fs = File.OpenRead(path)) { canRead = true; }
                }
                catch { canRead = false; }

                try
                {
                    using (FileStream fs = File.OpenWrite(path)) { canWrite = true; }
                }
                catch { canWrite = false; }

                Console.WriteLine($"\nПрава доступа к файлу {path}:");
                Console.WriteLine($"  Чтение: {(canRead ? "Да" : "Нет")}");
                Console.WriteLine($"  Запись: {(canWrite ? "Да" : "Нет")}");
                Console.WriteLine($"  Выполнение: {canExecute}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string baseDir = @"C:\Temp\TestDir";
            string filePath = Path.Combine(baseDir, "marchuk.EV");
            string copyPath = Path.Combine(baseDir, "marchuk_copy.EV");
            string moveDir = Path.Combine(baseDir, "Moved");
            string movedFilePath = Path.Combine(moveDir, "marchuk.EV");
            string renamedFilePath = Path.Combine(baseDir, "familiya.io");

            Directory.CreateDirectory(baseDir);
            Directory.CreateDirectory(moveDir);

            FileManager manager = new FileManager();
            FileInfoProvider provider = new FileInfoProvider();

            Console.WriteLine("=== Задание 1: Основные операции с файлами ===\n");

            // 1. Создать файл, записать текст, прочитать и вывести
            Console.WriteLine("1. Создание файла и запись текста:");
            manager.CreateFile(filePath, "Привет, это тестовый файл!");
            string content = File.ReadAllText(filePath);
            Console.WriteLine($"Прочитано из файла: {content}");

            // 2. Проверить существование перед удалением
            Console.WriteLine("\n2. Проверка существования файла:");
            if (File.Exists(filePath))
                Console.WriteLine($"Файл {filePath} существует");
            else
                Console.WriteLine($"Файл {filePath} не существует");

            // 3. Получить информацию о файле
            Console.WriteLine("\n3. Информация о файле:");
            provider.GetFileInfo(filePath);

            // 4. Скопировать файл
            Console.WriteLine("\n4. Копирование файла:");
            manager.CopyFile(filePath, copyPath);
            Console.WriteLine($"Копия существует? {File.Exists(copyPath)}");

            // 5. Переместить файл
            Console.WriteLine("\n5. Перемещение файла:");
            manager.MoveFile(filePath, movedFilePath);

            // 6. Переименовать файл
            Console.WriteLine("\n6. Переименование файла:");
            manager.RenameFile(movedFilePath, renamedFilePath);

            // 7. Обработать ошибку при удалении несуществующего файла
            Console.WriteLine("\n7. Удаление несуществующего файла:");
            manager.DeleteFile(Path.Combine(baseDir, "notexist.EV"));

            // 8. Сравнить два файла по размеру
            Console.WriteLine("\n8. Сравнение файлов по размеру:");
            provider.CompareFiles(copyPath, renamedFilePath);

            // 9. Удалить все файлы с расширением .EV
            Console.WriteLine("\n9. Удаление всех файлов .EV в директории:");
            manager.DeleteFilesByPattern(baseDir, "*.EV");

            // 10. Вывести список всех файлов
            Console.WriteLine("\n10. Список всех файлов в директории:");
            manager.ListFiles(baseDir);

            // 11. Запретить запись и попытаться записать
            Console.WriteLine("\n11. Запрет записи в файл:");
            manager.SetReadOnly(copyPath, true);
            try
            {
                File.WriteAllText(copyPath, "Попытка записи в защищенный файл");
                Console.WriteLine("Запись удалась (неожиданно)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи: {ex.Message}");
            }

            // 12. Проверить доступные права
            Console.WriteLine("\n12. Проверка прав доступа:");
            provider.CheckFilePermissions(copyPath);

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}