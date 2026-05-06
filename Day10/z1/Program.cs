using System;
using System.Collections.Generic;

namespace SingletonPatternTask
{
    public sealed class ConfigManager
    {
        private static ConfigManager? _instance;
        private static readonly object _lock = new object();
        private readonly Dictionary<string, string> _configData;

        private ConfigManager()
        {
            _configData = new Dictionary<string, string>();
        }

        public static ConfigManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigManager();
                    }
                }
            }

            return _instance;
        }

        public void SetConfig(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key cannot be null or empty", nameof(key));
            }

            _configData[key] = value;
        }

        public string? GetConfig(string key)
        {
            return _configData.TryGetValue(key, out string? value) ? value : null;
        }
    }

    class Program
    {
        static void Main()
        {
            ConfigManager config = ConfigManager.GetInstance();

            config.SetConfig("Version", "1.0.0");
            config.SetConfig("Timeout", "30");

            ConfigManager sameConfig = ConfigManager.GetInstance();
            Console.WriteLine(sameConfig.GetConfig("Version"));
            Console.WriteLine(sameConfig.GetConfig("Timeout"));
        }
    }
}
