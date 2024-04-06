using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Classes
{
    public class Post
    {
        public Post(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
