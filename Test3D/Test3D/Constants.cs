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
        public const string AUTHORIZED_CHARS = "UDTQCSM012345678";

        public static Dictionary<string, char> FLAT_IDENTIFIERS = new Dictionary<string, char>()
        {
            { "FIRST_FLOOR", 'U' },
            { "SECOND_FLOOR", 'D' },
            { "THIRD_FLOOR", 'T' },
            { "FOURTH_FLOOR", 'Q' },
            { "FIFTH_FLOOR", 'C' },
            { "SIXTH_FLOOR", 'S' },
            { "SEVENTH_FLOOR", 'M' }
        };

        public static Dictionary<string, float> FLAT_VALUES = new Dictionary<string, float>()
        {
            { "FIRST_FLOOR", -0.5f },
            { "SECOND_FLOOR", 2.055f },
            { "THIRD_FLOOR", 4.677f },
            { "FOURTH_FLOOR", 7.166f },
            { "FIFTH_FLOOR", 9.722f },
            { "SIXTH_FLOOR", 12.388f },
            { "SEVENTH_FLOOR", 14.433f }
        };

        public static Dictionary<string, char> CLIMB_IDENTIFIERS = new Dictionary<string, char>()
        {
            { "Vertical-20°", '0' },
            { "Vertical-27.5°", '1' },
            { "Vertical-40°", '2' },
            { "Horizontal-Right-20°", '3' },
            { "Horizontal-Right-30°", '4' },
            { "Horizontal-Right-40°", '5' },
            { "Horizontal-Left-20°", '6' },
            { "Horizontal-Left-30°", '7' },
            { "Horizontal-Left-40°", '8' }
        };

        public enum ShaderName { Default, Glass };
        public enum ElevatingDirection { Horizontal, Vertical };
        public enum HorizontalElevationConfig { None, RightInclination, LeftInclination };
    }
}
