using CaesarCipherApp.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;

namespace CaesarCipherApp.Services
{
    internal class FileManager
    {
        private static readonly string PATH = $"{Environment.CurrentDirectory}\\user.json";

        public BindingList<User> LoadUsersFromFile() 
        {
            var fileExists = File.Exists(PATH);

            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();

                return new BindingList<User>();
            }

            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<BindingList<User>>(fileText);
            }
        }
        
        public void SaveData(BindingList<User> users)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(users);

                writer.Write(output);
            }
        }
    }
}
