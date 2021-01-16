using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class Constants
    {
        public static string XNB_CONTENT_PATH = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName).FullName + @"\Content\bin\Windows\";
        public const string XNB_EXTENSION = ".xnb";
    }
}
