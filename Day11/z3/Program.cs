using System;
using System.Collections.Generic;

namespace RobotCommandPattern
{
    public interface ICommand
    {
        void Execute();
    }

    public class RobotVacuum
    {
        private string _name;
        private bool _isCleaning;

        public RobotVacuum(string name = "Робот-пылесос")
        {
            _name = name;
            _isCleaning = false;
        }

        public void StartCleaning()
        {
            if (_isCleaning)
            {
                Console.WriteLine($"{_name} уже убирается.");
            }
            else
            {
                _isCleaning = true;
                Console.WriteLine($"{_name} начал уборку.");
            }
        }

        public void StopCleaning()
        {
            if (_isCleaning)
            {
                _isCleaning = false;
                Console.WriteLine($"{_name} остановил уборку.");
            }
            else
            {
                Console.WriteLine($"{_name} не убирается.");
            }
        }

        public void ReturnToBase()
        {
            _isCleaning = false;
            Console.WriteLine($"{_name} возвращается на базу для зарядки.");
        }
    }

    public class StartCleaningCommand : ICommand
    {
        private RobotVacuum _robot;

        public StartCleaningCommand(RobotVacuum robot)
        {
            _robot = robot;
        }

        public void Execute()
        {
            _robot.StartCleaning();
        }
    }

    public class StopCleaningCommand : ICommand
    {
        private RobotVacuum _robot;

        public StopCleaningCommand(RobotVacuum robot)
        {
            _robot = robot;
        }

        public void Execute()
        {
            _robot.StopCleaning();
        }
    }

    public class ReturnToBaseCommand : ICommand
    {
        private RobotVacuum _robot;

        public ReturnToBaseCommand(RobotVacuum robot)
        {
            _robot = robot;
        }

        public void Execute()
        {
            _robot.ReturnToBase();
        }
    }

    public class RobotController
    {
        private Queue<ICommand> _commandQueue;
        private List<string> _commandHistory;

        public RobotController()
        {
            _commandQueue = new Queue<ICommand>();
            _commandHistory = new List<string>();
        }

        public void SetCommand(ICommand command)
        {
            _commandQueue.Enqueue(command);
            Console.WriteLine($"Команда {command.GetType().Name} добавлена в очередь.");
        }

        public void ExecuteCommands()
        {
            Console.WriteLine("\n--- Выполнение команд ---");
            while (_commandQueue.Count > 0)
            {
                ICommand command = _commandQueue.Dequeue();
                command.Execute();
                _commandHistory.Add($"{DateTime.Now:HH:mm:ss} - {command.GetType().Name}");
            }
            Console.WriteLine("--- Все команды выполнены ---\n");
        }

        public void ExecuteCommand(ICommand command)
        {
            Console.WriteLine($"\n--- Выполнение команды {command.GetType().Name} ---");
            command.Execute();
            _commandHistory.Add($"{DateTime.Now:HH:mm:ss} - {command.GetType().Name}");
        }

        public void ShowHistory()
        {
            Console.WriteLine("\n--- История команд ---");
            if (_commandHistory.Count == 0)
            {
                Console.WriteLine("Команд не было.");
            }
            else
            {
                foreach (string record in _commandHistory)
                {
                    Console.WriteLine(record);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Паттерн 'Команда' - Управление роботизированным пылесосом ===\n");

            RobotVacuum robot = new RobotVacuum("Roomba X5");
            RobotController controller = new RobotController();

            ICommand startCommand = new StartCleaningCommand(robot);
            ICommand stopCommand = new StopCleaningCommand(robot);
            ICommand returnCommand = new ReturnToBaseCommand(robot);

            Console.WriteLine("Способ 1: Одиночные команды");
            controller.ExecuteCommand(startCommand);
            controller.ExecuteCommand(stopCommand);
            controller.ExecuteCommand(returnCommand);

            Console.WriteLine("\nСпособ 2: Очередь команд");
            controller.SetCommand(startCommand);
            controller.SetCommand(returnCommand);
            controller.ExecuteCommands();

            Console.WriteLine("\nСпособ 3: Сложный сценарий уборки");
            controller.SetCommand(startCommand);
            controller.SetCommand(stopCommand);
            controller.SetCommand(startCommand);
            controller.SetCommand(returnCommand);
            controller.ExecuteCommands();

            controller.ShowHistory();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}