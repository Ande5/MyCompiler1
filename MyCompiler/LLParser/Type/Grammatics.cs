using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompiler
{
    /// <summary>
    /// Структура грамматики для нисходящего разбора
    /// </summary>
    public struct Grammatics
    {
        public string m_name;
        public int number;

        public Grammatics(int number, string name)
        {
            m_name = name;
            this.number = number;
        }
    }
}
