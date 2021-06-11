using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class Helpers
    {
        public static void SetAllArray(bool[] array, bool value)
        {
            for (int i= 0; i < array.Length ; i++)
            {
                array[i] = value;
            }
        }
    }
}
