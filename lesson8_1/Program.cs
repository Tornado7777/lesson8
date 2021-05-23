/*Создать консольное приложение, которое при старте выводит приветствие, записанное в настройках 
 * приложения (application-scope). Запросить у пользователя имя, возраст и род деятельности, а 
 * затем сохранить данные в настройках. При следующем запуске отобразить эти сведения. Задать 
 * приложению версию и описание.
 */
using System;
using System.Configuration;
using User_App;

namespace lesson8_1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            ReadAllSettings();
            UserLibrary New_user = User_input();
            AddUpdateAppSettings("User_name", New_user.User_name);
            AddUpdateAppSettings("User_age", Convert.ToString(New_user.User_age));
            AddUpdateAppSettings("User_career", New_user.User_career);
            ReadAllSettings();
        }

        static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static UserLibrary User_input()
        {
            User_App.UserLibrary New_user = new UserLibrary();
            Console.WriteLine("Введите свое имя:");
            New_user.User_name = Console.ReadLine();
            Console.WriteLine("Введите свой возраст:");
            while (New_user.User_age == 0)
            {
                try
                {
                    New_user.User_age = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Возраст нужно вводить цифрами");
                }
            }
            Console.WriteLine("Введите свой род деятельности:");
            New_user.User_career = Console.ReadLine();
            return New_user;
        }
       

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
