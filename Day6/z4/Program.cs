using System;

namespace EventHandlerPattern
{
    public class TemperatureEventArgs : EventArgs
    {
        public double Temperature { get; }
        public DateTime Time { get; }

        public TemperatureEventArgs(double temperature)
        {
            Temperature = temperature;
            Time = DateTime.Now;
        }
    }

    class TemperatureSensor
    {
        public event EventHandler<TemperatureEventArgs> TemperatureChanged;

        private double currentTemperature;

        public void SetTemperature(double newTemperature)
        {
            currentTemperature = newTemperature;
            OnTemperatureChanged(new TemperatureEventArgs(currentTemperature));
        }

        protected virtual void OnTemperatureChanged(TemperatureEventArgs e)
        {
            TemperatureChanged?.Invoke(this, e);
        }
    }

    class CoolingSystem
    {
        public void OnTemperatureChanged(object sender, TemperatureEventArgs e)
        {
            if (e.Temperature > 25)
            {
                Console.WriteLine($"[КОНДИЦИОНЕР] Температура {e.Temperature}°C > 25°C -> ВКЛЮЧАЮСЬ");
            }
            else
            {
                Console.WriteLine($"[КОНДИЦИОНЕР] Температура {e.Temperature}°C <= 25°C -> ВЫКЛЮЧЕН");
            }
        }
    }

    class AlarmSystem
    {
        public void OnTemperatureChanged(object sender, TemperatureEventArgs e)
        {
            if (e.Temperature > 35)
            {
                Console.WriteLine($"[СИГНАЛИЗАЦИЯ] ВНИМАНИЕ! Перегрев! Температура {e.Temperature}°C!");
            }
            else if (e.Temperature > 30)
            {
                Console.WriteLine($"[СИГНАЛИЗАЦИЯ] Предупреждение: высокая температура {e.Temperature}°C");
            }
            else
            {
                Console.WriteLine($"[СИГНАЛИЗАЦИЯ] Температура в норме: {e.Temperature}°C");
            }
        }
    }

    class TemperatureMonitor
    {
        private TemperatureSensor sensor;
        private CoolingSystem cooling;
        private AlarmSystem alarm;

        public TemperatureMonitor(TemperatureSensor sensor, CoolingSystem cooling, AlarmSystem alarm)
        {
            this.sensor = sensor;
            this.cooling = cooling;
            this.alarm = alarm;

            Subscribe();
        }

        public void Subscribe()
        {
            sensor.TemperatureChanged += cooling.OnTemperatureChanged;
            sensor.TemperatureChanged += alarm.OnTemperatureChanged;
        }

        public void Unsubscribe()
        {
            sensor.TemperatureChanged -= cooling.OnTemperatureChanged;
            sensor.TemperatureChanged -= alarm.OnTemperatureChanged;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            TemperatureSensor sensor = new TemperatureSensor();
            CoolingSystem cooling = new CoolingSystem();
            AlarmSystem alarm = new AlarmSystem();
            TemperatureMonitor monitor = new TemperatureMonitor(sensor, cooling, alarm);

            Console.WriteLine("=== Контроль температуры в помещении ===\n");

            Console.WriteLine("Установка температуры: 22°C");
            sensor.SetTemperature(22);
            Console.WriteLine();

            Console.WriteLine("Установка температуры: 27°C");
            sensor.SetTemperature(27);
            Console.WriteLine();

            Console.WriteLine("Установка температуры: 32°C");
            sensor.SetTemperature(32);
            Console.WriteLine();

            Console.WriteLine("Установка температуры: 38°C");
            sensor.SetTemperature(38);
            Console.WriteLine();

            Console.WriteLine("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}