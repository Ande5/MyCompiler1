using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompiler
{
    public static class LLParserEvent
    {
        /// <summary>
        /// Делегат, для методов информирования
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        public delegate void PrintResult(string text);
        public static PrintResult PrintCompileInfo;
        public static PrintResult PrintMessage;
    }
}
