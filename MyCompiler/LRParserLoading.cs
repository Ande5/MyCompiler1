using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompiler
{
     public class LRParserLoading
    {
        private List<Word> m_WordsList = new List<Word>();
        private List<Rule> m_RulesList = new List<Rule>();
        private int [,] m_Tabel = {
                                      {0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,3},
                                      {0,0,0,0,2,1,0,0,0,0,1,0,0,1,1,1,1,1,1,1,0},
                                      {2,0,0,0,0,0,1,3,3,0,0,0,2,0,0,0,0,0,0,0,3},
                                      {0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0},
                                      {0,0,0,0,0,0,3,3,3,0,3,0,3,3,3,3,3,3,3,3,3},
                                      {0,0,0,0,2,1,0,0,0,0,1,0,0,1,1,1,1,1,1,1,0},
                                      {1,1,0,2,0,0,1,3,0,1,0,0,0,0,0,0,0,0,0,0,0},
                                      {1,1,0,2,0,0,1,0,3,1,0,0,0,0,0,0,0,0,0,0,0},
                                      {0,0,2,0,1,1,0,0,0,0,1,0,0,1,1,1,1,1,1,1,0},
                                      {0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0},
                                      {0,0,0,0,0,0,3,3,3,0,3,2,3,3,3,3,3,3,3,3,3},
                                      {0,0,2,0,1,1,0,0,0,0,1,0,0,1,1,1,1,1,1,1,0},
                                      {0,0,0,0,0,0,3,3,3,0,3,0,3,3,3,3,3,3,3,3,3},
                                      {0,0,0,0,0,0,3,3,3,0,3,0,3,3,3,3,3,3,3,3,3},
                                      {0,0,0,0,2,1,0,0,0,0,1,0,0,1,1,1,1,1,1,1,0},
                                      {0,0,0,0,2,1,0,0,0,0,1,0,0,1,1,1,1,1,1,1,0},
                                      {0,0,0,0,2,1,0,0,0,0,1,0,0,1,1,1,1,1,1,1,0},
                                      {0,0,0,0,2,1,0,0,0,0,1,0,0,1,1,1,1,1,1,1,0},
                                      {0,0,0,0,2,1,0,0,0,0,1,0,0,1,1,1,1,1,1,1,0},
                                      {0,0,0,0,2,1,0,0,0,0,1,0,0,1,1,1,1,1,1,1,0},
                                      {0,1,0,0,0,0,1,0,0,0,0,0,0,0,1,1,1,1,1,1,0}};

        private string [] m_rule = { "S -> else W then W if Y",
                                     "S -> else W then W if Y S",
                                     "X -> := id",
                                     "X -> := id [ Y ]",
                                     "Y -> A",
                                     "W -> X A",
                                     "W -> S",
                                     "A -> id",
                                     "A -> const",
                                     "A -> id [ Y ]",
                                     "A -> R A",
                                     "R -> * A",
                                     "R -> & A",
                                     "R -> + A",
                                     "R -> - A",
                                     "R -> | A",
                                     "R -> > A" };

        private string[] m_words = { "S", "X", "Y", "W", "A", "R", "else", "then", "if", ":=", "id", "[", "]", "const", "*", "&", "+", "-", "|", ">", "$" };
        
        public LRParserLoading()
        {
            foreach(var word in m_words)
            {
                SetWord(word);
            }
            foreach(var rule in m_rule)
            {
                SetRule(rule);
            }
        }
         
        /// <summary>
        /// Задает правило в формате [Слово правила] -> [Слово 1] [Слово 2] [Слово N]
        /// </summary>
        /// <param name="word">Строка правила. Задается по формату, с разделением пробелами между словами</param>
        /// <returns></returns>
        public virtual LRParserLoading SetRule(string word)
        {
            string[] words = word.Split();

            if (words.Length > 2 && words[1] == "->")
            {
                // Принимаем в качестве имени правила первый символ
                Word ruleWord = GetWord(words[0]);
                if (ruleWord == null)
                {
                    // Если такого символа не нашлось, то загружаем его
                    SetWord(words[0]);
                    // И получаем ссылку на него
                    ruleWord = GetWord(words[0]);
                }

                List<int> wordNumbers = new List<int>();

                // Создаем список правил
                for (int i = 2; i < words.Length; i++)
                {
                    Word tmpWord = GetWord(words[i]);
                    if (tmpWord == null)
                    {
                        SetWord(words[i]);
                        tmpWord = GetWord(words[i]);
                    }
                    wordNumbers.Add(tmpWord.Number);
                }
                // Задаем правило
                m_RulesList.Add(new Rule(ruleWord.Number, wordNumbers.ToArray()));
            }
            else
            {
                throw new Exception("Правило не соотвествует формату");
            }
            return this;
        }
        /// <summary>
        /// Задает слово грамматики
        /// </summary>
        /// <param name="word">Задает терминал или нетерминал грамматики</param>
        /// <returns></returns>
        public virtual LRParserLoading SetWord(string word)
        {
            if (GetWord(word) == null)
            {
                m_WordsList.Add(new Word(m_WordsList.Count, word));
            }
            return this;
        }

        /// <summary>
        /// Возвращает слово грамматики
        /// </summary>
        /// <param name="word">Символьное представление слова грамматики</param>
        /// <returns>Возвращает слово грамматики типа <see cref="Word"/> Word</returns>
        public virtual Word GetWord(string word)
        {
            return Words.FirstOrDefault(item => item.Value == word);
        }

        /// <summary>
        /// Коллекция слов грамматики
        /// </summary>
        public Word[] Words
        {
            get { return m_WordsList.ToArray(); }
        }
        /// <summary>
        /// Коллекция правил грамматики
        /// </summary>
        public Rule[] Rules
        {
            get { return m_RulesList.ToArray(); }
        }
        /// <summary>
        /// Управляющая таблица грамматики
        /// </summary>
        public int[,] ControlTable
        {
            get { return m_Tabel; }
        }
    }
}
