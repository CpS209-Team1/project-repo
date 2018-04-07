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
        List<string> Serialize();
        void Deserialize(StreamReader);
    }
}
