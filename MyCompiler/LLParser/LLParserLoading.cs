using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompiler
{
    public class LLParserLoading
    {
        List<string> m_nterminals = new List<string>();
        List<string> m_terminals = new List<string>();
        List<Grammatics> m_Rules = new List<Grammatics>();
        List<Grammatics> m_Terminals = new List<Grammatics>();
        List<Grammatics> m_NTerminals = new List<Grammatics>();
        private string  [] m_rule = {"S -> #else Q",
                                     "Q -> Y Z",
                                     "Q -> S Z",
                                     "S -> eps",
                                     "Z -> #then L",
                                     "L -> Y I",
                                     "L -> S I",
                                     "I -> #if K S",
                                     "Y -> := id D",
                                     "K -> A Y",
                                     "D -> eps",
                                     "D -> G",
                                     "G -> id B",
                                     "G -> A",
                                     "B -> eps",
                                     "B -> [ A C",
                                     "D -> [ A C G",
                                     "C -> ]",
                                     "A -> [ A F",
                                     "F -> ] G",
                                     "A -> id",
                                     "A -> const",
                                     "A -> R A",
                                     "R -> * A",
                                     "R -> & A",
                                     "R -> + A",
                                     "R -> - A",
                                     "R -> > A",
                                     "R -> | A",
                                     "Y -> eps" };

        int[,] m_Tabel = {
                                     {1,4,4,31,31,31,31,31,31,31,31,31,31,31,4},
                                     {3,3,3,2,31,31,31,31,31,31,31,31,31,31,3},
                                     {31,5,31,31,31,31,31,31,31,31,31,31,31,31,31},
                                     {7,7,7,6,31,31,31,31,31,31,31,31,31,31,7},
                                     {31,31,8,31,31,31,31,31,31,31,31,31,31,31,31},
                                     {30,30,30,9,31,31,31,31,31,31,31,31,31,31,30},
                                     {31,31,31,31,10,10,31,10,10,10,10,10,10,10,31},
                                     {11,11,11,31,12,17,31,12,12,12,12,12,12,12,11},
                                     {31,31,31,31,13,14,31,14,14,14,14,14,14,14,31},
                                     {15,15,15,15,15,16,15,15,15,15,15,15,15,15,15},
                                     {31,31,31,31,31,31,18,31,31,31,31,31,31,31,31},
                                     {31,31,31,31,21,19,31,22,23,23,23,23,23,23,31},
                                     {31,31,31,31,31,31,20,31,31,31,31,31,31,31,31},
                                     {31,31,31,31,31,31,31,31,24,25,26,27,28,29,31},
                                     {32,31,31,31,31,31,31,31,31,31,31,31,31,31,31},
                                     {31,32,31,31,31,31,31,31,31,31,31,31,31,31,31},
                                     {31,31,32,31,31,31,31,31,31,31,31,31,31,31,31},
                                     {31,31,31,32,31,31,31,31,31,31,31,31,31,31,31},
                                     {31,31,31,31,32,31,31,31,31,31,31,31,31,31,31},
                                     {31,31,31,31,31,32,31,31,31,31,31,31,31,31,31},
                                     {31,31,31,31,31,31,32,31,31,31,31,31,31,31,31},
                                     {31,31,31,31,31,31,31,32,31,31,31,31,31,31,31},
                                     {31,31,31,31,31,31,31,31,32,31,31,31,31,31,31},
                                     {31,31,31,31,31,31,31,31,31,32,31,31,31,31,31},
                                     {31,31,31,31,31,31,31,31,31,31,32,31,31,31,31},
                                     {31,31,31,31,31,31,31,31,31,31,31,32,31,31,31},
                                     {31,31,31,31,31,31,31,31,31,31,31,31,32,31,31},
                                     {31,31,31,31,31,31,31,31,31,31,31,31,31,32,31},
                                     {31,31,31,31,31,31,31,31,31,31,31,31,31,31,33}    
                                  };

        /// <summary>
        /// Парсим управляюще приавило
        /// </summary>
        public void Read_Regulation()
        {
            Grammatics rule_down = new Grammatics();
            for (int k = 0; k < m_rule.LongLength; k++ )
            {
                string[] str = m_rule[k].Split(new[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                if ((Search_nterminals(str[0])))
                {
                    m_nterminals.Add(str[0]);
                }
                rule_down.m_name = str[1];
                rule_down.number = k + 1;
                m_Rules.Add(rule_down);
            }
        }
        /// <summary>
        /// Поиск терминалов в списке
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool Search_terminals(string str)
        {
            bool flag = true;
            foreach (var terminal in m_terminals)
            {
                if (terminal == str)
                {
                    flag = false;
                }
            }
            return flag;
        }
        /// <summary>
        /// Поиск нетерминалов в списке
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool Search_nterminals(string str)
        {
            bool flag = true;
            foreach (var nterminal in m_nterminals)
            {
                if (nterminal == str)
                {
                    flag = false;
                }
            }
            return flag;
        }
        /// <summary>
        /// Парсим правило на поиск терминалов
        /// </summary>
        public void CheckRule_terminals()
        {
            foreach (var rule in m_Rules)
            {
                string[] rule_pars = rule.m_name.Split(' ');
                for (int k = 0; k < rule_pars.Length; k++)
                {
                    if (Search_terminals(rule_pars[k]) && rule_pars[k] != "eps")
                    {
                        if (Search_nterminals(rule_pars[k]))
                        {
                            m_terminals.Add(rule_pars[k]);
                        }
                    }
                }
            }
            m_terminals.Add("$");
            AddTerminals();
            AddNTerminals();
        }
        /// <summary>
        /// Формирование Териминалов
        /// </summary>
        public void AddTerminals()
        {
            for (int k = 0; k < m_terminals.Count; k++)
            {
                m_Terminals.Add(new Grammatics(k + 1, m_terminals[k]));
            }
        }
        /// <summary>
        /// Формирование грамматики с нетерминалами и терминалами
        /// </summary>
        public void AddNTerminals()
        {
            int index = 1;
            for (int k = 0; k < m_nterminals.Count; k++)
            {
                m_NTerminals.Add(new Grammatics(index, m_nterminals[k]));
                index++;
            }
            for (int k = 0; k < m_terminals.Count; k++)
            {
                m_NTerminals.Add(new Grammatics(index, m_terminals[k]));
                index++;
            }
        }
        public LLParserLoading()
        {
            Read_Regulation();
            CheckRule_terminals();
        }
        /// <summary>
        /// Список терминалов
        /// </summary>
        public List<Grammatics> Terminals
        {
            get { return m_Terminals; }
        }
        /// <summary>
        /// Список терминалов и нетерминалов
        /// </summary>
        public List<Grammatics> NTerminals
        {
            get { return m_NTerminals; }
        }
        /// <summary>
        /// Список правил
        /// </summary>
        public List<Grammatics> Rules
        {
            get { return m_Rules; }
        }
        /// <summary>
        /// Управляющая таблица грамматики
        /// </summary>
        public int[,] Tabel
        {
            get { return m_Tabel; }
        }
    }
}
