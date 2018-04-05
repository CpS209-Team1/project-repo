using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

interface ISerializable
{
    void Serialize(string);
    void Deserialize(string);
}