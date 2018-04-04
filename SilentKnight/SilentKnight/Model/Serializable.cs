using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Model
{
    interface ISerializable
    {
        public void Serialize(string filename ){}
        public void Deserialize(string filename) {}
    }
}


