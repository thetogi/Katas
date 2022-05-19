using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas.Interface_Casting
{
    internal class InterfaceCastingDemo : MyBaseClass, myInterface 
    {
        public int myInt { get; set; }
        public byte myByte { get; }

    }

    internal class MyBaseClass
    {
        public Int64 myInt64 { get; set; }
    }
}
