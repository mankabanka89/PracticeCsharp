using System;
using System.IO;

namespace FileWatcherBackup
{
    class FileWatcher
    {
        private FileSystemWatcher watcher;
        private string watchedFolder;
        private string backupFolder;

        public FileWatcher(string watchedFolder, string backupFolder)
        {
            this.watchedFolder = watchedFolder;
            this.backupFolder = backupFolder;

            if (!Directory.Exists(watchedFolder))
            {
                Directory.CreateDirectory(watchedFolder);
                Console.WriteLine($"Создана папка для отслеживания: {watchedFolder}");
            }

            if (!Directory.Exists(backupFolder))
            {
                Directory.CreateDirectory(backupFolder);
                Console.WriteLine($"Создана папка для бэкапов: {backupFolder}");
            }

            watcher = new FileSystemWatcher(watchedFolder);
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;

            watcher.Created += OnCreated;
            watcher.Changed += OnChanged;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;

            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = true;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"[СОЗДАН] {e.Name} в {DateTime.Now:HH:mm:ss}");
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"[ИЗМЕНЕН] {e.Name} в {DateTime.Now:HH:mm:ss}");

            if (File.Exists(e.FullPath))
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = Path.GetFileNameWithoutExtension(e.Name);
                string extension = Path.GetExtension(e.Name);
                string backupName = $"{fileName}_{timestamp}{extension}.bak";
                string backupPath = Path.Combine(backupFolder, backupName);

                try
                {
                    File.Copy(e.FullPath, backupPath, true);
                    Console.WriteLine($"  -> Бэкап создан: {backupName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  -> Ошибка бэкапа: {ex.Message}");
                }
            }
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"[УДАЛЕН] {e.Name} в {DateTime.Now:HH:mm:ss}");
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"[ПЕРЕИМЕНОВАН] {e.OldName} -> {e.Name} в {DateTime.Now:HH:mm:ss}");
        }

        public void Stop()
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                Console.WriteLine("\nОтслеживание остановлено");
            }
        }

        public void ShowBackupFiles()
        {
            if (Directory.Exists(backupFolder))
            {
                string[] backups = Directory.GetFiles(backupFolder);
                Console.WriteLine($"\nФайлы в папке бэкапов ({backupFolder}):");
                foreach (string backup in backups)
                {
                    FileInfo info = new FileInfo(backup);
                    Console.WriteLine($"  {Path.GetFileName(backup)} ({info.Length} байт)");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string watchedFolder = Path.Combine(Directory.GetCurrentDirectory(), "watched");
            string backupFolder = Path.Combine(Directory.GetCurrentDirectory(), "backup");

            Console.WriteLine("=== Автоматическое создание резервной копии ===\n");
            Console.WriteLine($"Отслеживаемая папка: {watchedFolder}");
            Console.WriteLine($"Папка для бэкапов: {backupFolder}");
            Console.WriteLine("\nИнструкция:");
            Console.WriteLine("1. Создайте любой файл в папке 'watched'");
            Console.WriteLine("2. Измените содержимое файла (и сохраните)");
            Console.WriteLine("3. Удалите файл");
            Console.WriteLine("4. Нажмите 'q' для выхода\n");

            FileWatcher watcher = new FileWatcher(watchedFolder, backupFolder);

            Console.WriteLine("Отслеживание запущено...\n");

            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
            }

            watcher.Stop();
            watcher.ShowBackupFiles();

            Console.WriteLine("\nПрограмма завершена. Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}