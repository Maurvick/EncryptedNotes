using CaesarCipherApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
