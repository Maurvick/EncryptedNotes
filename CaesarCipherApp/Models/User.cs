using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipherApp.Models
{
    class User
    {
        public int Id { get; set; }
        public int Key { get; set; }
        public string? Text { get; set; }
    }
}
