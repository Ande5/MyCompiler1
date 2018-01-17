using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompiler
{
    class LLParser
    {
        private List<Grammatics> m_rule, m_terminals, m_nterminals;
        private Grammatics eps, id, constNT;
        private int[,] m_tabel;
        private List<Grammatics> m_element_str = new List<Grammatics>();
        private List<Grammatics> m_eps_rules = new List<Grammatics>();
        public enum NumberCheck { True, False, Error };
        public LLParser() { }
        public LLParser(LLParserLoading loading)
        {
            m_rule = loading.Rules;
            m_tabel = loading.Tabel;
            m_terminals = loading.Terminals;
            m_nterminals = loading.NTerminals;
        }
        public void Run(object obj)
        {
            string str = obj as string;
            if (str != null)
            {
                Run(str);
            }
        }
        public void Run(string grammar_str)
        {
            m_element_str.Clear();
            m_eps_rules.Clear();
            Search_Terminals();
            Search_Rules_Eps();
            grammar_str += " " + eps.m_name;
            string[] str = grammar_str.Split(' ');
            bool flag = false;
            NumberCheck check_number = NumberCheck.False;
            for (int k = 0; k < str.Length; k++)
            {
                Search_Terminals(str, k, ref flag, check_number);
                if (flag == false && check_number != NumberCheck.Error)
                {
                    Search_ID(str[k], ref flag, ref check_number);
                }
                flag = false;
            }
            if (check_number != NumberCheck.Error)
            {
                Algoritm_Down();
            }
            Array.Clear(str, 0, str.Length);
        }
        /// <summary>
        /// Определение являеться ли переменная id
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <param name="check_number"></param>
        public void Search_ID(string number, ref bool flag, ref NumberCheck check_number)
        {
            if (IsThisNumberDown(number) == NumberCheck.False)
            {
                if (number.Length <= 8 && number.Length > 0)
                {
                    m_element_str.Add(id);
                    flag = true;
                }
                else
                {
                    Message_Errors(number, check_number);
                }
            }
        }
        /// <summary>
        /// Вывод ошибок
        /// </summary>
        /// <param name="number"></param>
        /// <param name="check_number"></param>
        public void Message_Errors(string number, NumberCheck check_number)
        {
            if (number.Length > 8)
            {
                check_number = NumberCheck.Error;
                CompilerEvent.PrintMessageLLParser("Длина идентификатора должна быть меньше 8 символов!\nОшибка --> " + number);
            }
            if (number.Length == 0)
            {
                check_number = NumberCheck.Error;
                CompilerEvent.PrintMessageLLParser("Длина идентификатора должна быть больше 0 символов!\n");
            }
        }
        /// <summary>
        /// Проверка числа по варианту
        /// </summary>
        /// <param name="str1"></param>
        /// <returns></returns>
        public NumberCheck IsThisNumberDown(string str1)
        {
            if (CheckNumber(str1, "0123456789ABCDF.Ee-"))
            {
                m_element_str.Add(constNT);
                return NumberCheck.True;   
            }
            else
            {
                return NumberCheck.False;
            }
        }

        public bool CheckNumber(string str, string symbol)
        {
            bool number = false;
            for (int k = 0; k < str.Length; k++ )
            {
               number = symbol.Contains(Convert.ToString(str[k]));
               if (!number) { return false; }
            }
            return number;
        }
        public void Algoritm_Down()
        {
            string str_nterminals = m_nterminals[0].m_name + " " + eps.m_name;
            int index_i = 0, index_j = 0;
            index_i = m_element_str[0].number;
            index_j = Search_Index_J(Convert.ToString(str_nterminals[0]));
            int number_rule = 0;
            string pr = "";
            while (m_tabel[index_j - 1, index_i - 1] != 33)
            {
                number_rule = m_tabel[index_j - 1, index_i - 1]; // определение номера правила
                if (number_rule == 32)
                {
                    int rule = str_nterminals.IndexOf(" ");
                    str_nterminals = str_nterminals.Remove(0, rule + 1); // удаление первого нетерминала
                    m_element_str.RemoveAt(0); // удаление первого элемента из списка 
                    Print_Info(m_element_str, str_nterminals, pr);
                }
                else
                {
                    foreach (var rule in m_rule)
                    {
                        if (rule.number == number_rule)
                        {
                            string[] str_nterminals_array = str_nterminals.Split(' ');
                            str_nterminals_array[0] = rule.m_name;
                            str_nterminals = "";
                            Scaning_Eps_Rule(str_nterminals_array, ref str_nterminals, rule.number);
                            pr += " " + Convert.ToString(rule.number);
                            Print_Info(m_element_str, str_nterminals, pr);
                        }
                    }
                }
                index_i = m_element_str[0].number;
                string[] laM = str_nterminals.Split(' ');
                index_j = Search_Index_J(laM[0]);
                if (m_tabel[index_j - 1, index_i - 1] == 31)
                {
                    CompilerEvent.PrintCompileInfoLLParser("Ошибка при выполнении нисходящего разбора!");
                    m_element_str.Clear();
                    break;
                }
            }

        }
        public int Search_Index_J(string symbol)
        {
            foreach (var nterminal in m_nterminals)
            {
                if (symbol == nterminal.m_name)
                {
                     return nterminal.number;
                }
            }
            return 0;
        }
        public void Print_Info(List<Grammatics> element_str, string str_nterminals, string pr)
        {
            string s1 = "";
            foreach (var element in element_str)
            {
                s1 += element.m_name + " ";
            }
            CompilerEvent.PrintCompileInfoLLParser("\nСтрока:" + s1);
            CompilerEvent.PrintCompileInfoLLParser("Магазин:" + str_nterminals);
            CompilerEvent.PrintCompileInfoLLParser("Правила:" + pr);
        }
        /// <summary>
        /// Формирование списка элементов из терминалов
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <param name="flag"></param>
        /// <param name="check_number"></param>
        public void Search_Terminals(string[] str, int index, ref bool flag, NumberCheck check_number)
        {
            foreach (var terminal in m_terminals)
            {
                if (terminal.m_name == str[index] && check_number != NumberCheck.Error)
                {
                    m_element_str.Add(terminal);
                    flag = true;
                }
            }
        }
        public void Scaning_Eps_Rule(string[] str_nterminals_array, ref string str_nterminals, int rule_number)
        {
            bool flag = false;
            foreach (var eps_rule in m_eps_rules)
            {
                if (rule_number == eps_rule.number)
                {
                    Add_Eps_str_nterminals(str_nterminals_array, ref str_nterminals);
                    flag = true;
                }
            }
            if (flag == false)
            {
                Add_Eps_str_nterminals(str_nterminals_array, ref str_nterminals);
                str_nterminals = str_nterminals_array[0] + " " + str_nterminals;
            }
        }
        public void Add_Eps_str_nterminals(string[] str_nterminals_array, ref string str_nterminals)
        {
            for (int k = 1; k < str_nterminals_array.Length; k++)
            {
                str_nterminals += str_nterminals_array[k] + " ";
            }
        }
        /// <summary>
        /// Поиск терминалов {$, id, const}
        /// </summary>
        public void Search_Terminals()
        {
            foreach (var terminal in m_terminals)
            {
                switch (terminal.m_name)
                {
                    case "$":
                        {
                            eps = terminal;
                            break;
                        }
                    case "id":
                        {
                            id = terminal;
                            break;
                        }
                    case "const":
                        {
                            constNT = terminal;
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Поиск eps правил (записываем в массив)
        /// </summary>
        public void Search_Rules_Eps()
        {
            // Для eps правил можно задать свой символ
            foreach (var rule in m_rule)
            {
                if (rule.m_name == "eps")
                {
                    m_eps_rules.Add(rule);
                }
            }
        }

    }
}
