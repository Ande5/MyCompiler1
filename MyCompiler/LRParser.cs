using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCompiler
{
    public class LRParser
    {
        public LRParser(Rule[] ruleses, Word[] words, int[,] ruleTable, int countOfRules)
        {
            Rules = ruleses;
            Words = words;
            RuleRuleTable = ruleTable;
            CountOfRules = countOfRules;
        }

        public LRParser(LRParserLoading loading)
        {
            Words = loading.Words;
            Rules = loading.Rules;
            RuleRuleTable = loading.ControlTable;
        }
        /// <summary>
        /// Коллекция правил, для компиляции
        /// </summary>
        public Rule[] Rules;

        /// <summary>
        /// Коллекция символов грамматики, для компиляции
        /// </summary>
        public Word[] Words;

        /// <summary>
        /// Управляющая таблица восходящего разбора
        /// </summary>
        public int[,] RuleRuleTable;

        /// <summary>
        /// Количество правил
        /// </summary>
        public int CountOfRules;

        public void PrintInfo(LinkedList<Word> arrS, List<int> rulesFounded, Queue<Word> splittedWords)
        {
            string s1 = "";
            foreach (var word in splittedWords)
            {
                s1 += string.Format("{0} ", Words[word.Number].Value);
            }
            CompilerEvent.PrintCompileInfoLRParser.Invoke("\nСтрока:" + s1 + '\n');
            string s2 = "";

            foreach (var arrVal in arrS)
            {
                s2 += string.Format("{0} ", arrVal.Value); 
            }
            CompilerEvent.PrintCompileInfoLRParser.Invoke(@"Магазин:" + s2 + '\n');
            string s3 = "";

            foreach (var rule in rulesFounded)
            {
                s3 = s3 + (rule + 1) + " ";
            }
            CompilerEvent.PrintCompileInfoLRParser.Invoke(@"Правила:" + s3 + '\n');
        }

        /// <summary>
        /// Производит компиляцию, на основе цепочки данных в стеке
        /// </summary>
        /// <param name="ruleNumber">Номер правила</param>
        /// <param name="arrSNode">Коллекция символов в стеке</param>
        public void MyCompil(int ruleNumber, LinkedListNode<Word> arrSNode)
        {
            if (ruleNumber == 0)
            {
                string condition = arrSNode.Next.Next.Next.Next.Next.Value.Temp;
                string then =  arrSNode.Next.Next.Next.Value.Temp;
                string ifelse = arrSNode.Next.Value.Temp;
                arrSNode.Value.Temp = string.Format("if ( {2} ){{ {1}; }} else {{ {0}; }}",ifelse,then, condition);
            }
            if (ruleNumber == 1)
            {
                 string condition = arrSNode.Next.Next.Next.Next.Next.Value.Temp;
                string then =  arrSNode.Next.Next.Next.Value.Temp;
                string ifelse = arrSNode.Next.Value.Temp;
                arrSNode.Value.Temp = string.Format("if ( {2} ){{ {1}; }} else {{ {0}; }} {3}", ifelse, then, condition, arrSNode.Next.Next.Next.Next.Next.Next.Value.Temp);
            }
            if (ruleNumber == 2)
                arrSNode.Value.Temp = string.Format("{0} =", arrSNode.Next.Value.Temp);
            if (ruleNumber == 3)
                arrSNode.Value.Temp = string.Format("{0} [ {1} ] =", arrSNode.Next.Value.Temp, arrSNode.Next.Next.Next.Value.Temp);
            if (ruleNumber == 4)
                arrSNode.Value.Temp = string.Format("{0}", arrSNode.Value.Temp);
            if (ruleNumber == 5)
                arrSNode.Value.Temp = string.Format("{0} {1}", arrSNode.Value.Temp, arrSNode.Next.Value.Temp);
            if (ruleNumber == 7)
                arrSNode.Value.Temp = string.Format("{0}", arrSNode.Value.Temp);
            if (ruleNumber == 8)
                arrSNode.Value.Temp = string.Format("{0}", arrSNode.Value.Temp);
            if (ruleNumber == 9)
                arrSNode.Value.Temp = string.Format("{0} [ {1} ]", arrSNode.Value.Temp, arrSNode.Next.Next.Value.Temp);
            if (ruleNumber == 10)
                arrSNode.Value.Temp = string.Format("{0} {1}", arrSNode.Value.Temp, arrSNode.Next.Value.Temp);
            if (ruleNumber == 11)
                arrSNode.Value.Temp = string.Format("{0} *", arrSNode.Next.Value.Temp);
            if (ruleNumber == 12)
                arrSNode.Value.Temp = string.Format("{0} &", arrSNode.Next.Value.Temp);
            if (ruleNumber == 13)
                arrSNode.Value.Temp = string.Format("{0} +", arrSNode.Next.Value.Temp);
            if (ruleNumber == 14)
                arrSNode.Value.Temp = string.Format("{0} -", arrSNode.Next.Value.Temp);
            if (ruleNumber == 15)
                arrSNode.Value.Temp = string.Format("{0} |", arrSNode.Next.Value.Temp);
            if (ruleNumber == 16)
                arrSNode.Value.Temp = string.Format("{0} >", arrSNode.Next.Value.Temp);
        }

        public void Algorithm(Queue<Word> splittedWords)
        {
            // Коллекций номеров правил
            List<int> rulesFound = new List<int>();
            //TODO: XML док
            LinkedList<Word> stack = new LinkedList<Word>();

            //TODO: Избавиться от зависимости последнего символа
            stack.AddLast(Words.Last());

            PrintInfo(stack, rulesFound, splittedWords);

            while (splittedWords.Count > 0)
            {
                CompileActions action = CompileActions.Start;
                
                // Серем слово, для просмотра
                Word word = splittedWords.Peek();

                // Проверяем, является ли последний символ символом конца цепочки
                if (word.Number == Words.Last().Number)
                {
                    if (stack.Count == 2)
                    {
                        // Если последний символ в стеке - символ цепочки S, а первый - символ клнца цепочки
                        // Тогда разбор окончен успешно
                        // TODO: Убрать первый сивол, заменить
                        if ((stack.Last.Value.Number == Words.First().Number) && (stack.First.Value.Number == Words.Last().Number))
                        {
                            CompilerEvent.PrintCompileResult.Invoke(stack.Last.Value.Temp);
                            return;
                        }
                    }
                }

                int row = stack.Last.Value.Number;
                int col = word.Number;

                // Правило, для переноса
                if ((RuleRuleTable[row, col] == 1 && action != CompileActions.Error) || (RuleRuleTable[row, col] == 2 && action != CompileActions.Error))
                {
                    // Вытаскиваем слово из входной цепочки и заносим в стек
                    Word tmpWord = splittedWords.Dequeue();
                    stack.AddLast(tmpWord);
                    action = CompileActions.Next;
                }

                // Правило, для свертки
                if ((RuleRuleTable[row, col] == 3) && action != CompileActions.Next && action != CompileActions.Error)
                {
                    // Ищем количество слов, для свертки
                    // Изначально, текуший символ не может иметь правило, для сдвига => cnt = 1
                    int cntWordsInStack = 1;

                    // Начинаем поиск с конца цепочки
                    LinkedListNode<Word> node = stack.Last;
                    
                    // Если правило не равняется сдвигу, то сдвигаем цепочку. Увеличиваем число найденных слов
                    while(RuleRuleTable[node.Previous.Value.Number, node.Value.Number] != 1)
                    {
                        node = node.Previous;
                        cntWordsInStack++;
                    }

                    // Ищем подходящее правило
                    for (var ruleNumber = 0; ruleNumber < Rules.Length; ruleNumber++)
                    {
                        // Ищем подходящие по длене правило из списка правил
                        if (Rules[ruleNumber].CountOfWords == cntWordsInStack)
                        {
                            // Количество символов, которые последовательно совпадают в цепочке и правиле
                            int cntWordsInRule = 0;

                            // Присваиваем начальный символ цепочки, для свертки
                            LinkedListNode<Word> srchNode = node;
                            // Можно изменить способ сверки
                            for (var i = 0; i < cntWordsInStack; i++)
                            {
                                // Сверяет последовательность слов в выбранном правиле с цепочкой правил в стеке
                                if (Rules[ruleNumber].RuleList[i] == srchNode.Value.Number)
                                {
                                    cntWordsInRule++;
                                    srchNode = srchNode.Next;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            
                            // Если количество сошлось, то это то правило
                            if (cntWordsInRule == cntWordsInStack)
                            {
                                // Загружаем последний элемент стека
                                srchNode = stack.Last;

                                // СмещаемЮ до первого элемента, в цепочке правил
                                for (int i = 1; i < cntWordsInRule; i++)
                                {
                                    srchNode = srchNode.Previous;
                                }
                                
                                // Производим покпиляцию по правилу
                                   MyCompil(ruleNumber, srchNode);

                                // Присваиваем номер и символ правила, которое использовали
                                int collapsedRuleNumber = Rules[ruleNumber].RuleNumber;

                                node.Value.Number = collapsedRuleNumber;
                                node.Value.Value = Words[collapsedRuleNumber].Value;

                                // Добаляем использованное правило в коллекцию правил
                                rulesFound.Add(ruleNumber);

                                // Очищаем ненужные элементы до конца списка
                                while (node.Next != null)
                                {
                                    stack.Remove(node.Next);
                                }

                                action = CompileActions.Next;
                                //PrintCompileResult.Invoke($"Value: {node.Value.Value} \nRule: {node.Value.Number} \nTemp: {node.Value.Temp}");
                                CompilerEvent.PrintCompileResult.Invoke(string.Format("Value: {0} \nRule: {1} \nTemp: {2}", node.Value.Value, node.Value.Number, node.Value.Temp));
                                break;
                                
                            }
                        }
                    }
                }
                if (action != CompileActions.Next)
                {
                    CompilerEvent.PrintCompileInfoLRParser.Invoke(@"Ошибка при выполнении восходящего разбора!");
                    CompilerEvent.PrintCompileResult.Invoke("");
                    return;
                }
                PrintInfo(stack, rulesFound, splittedWords);
            }
        }

        public bool IsNumber(string str2, string dopstr, int k)
        {
            int pr2 = 0;
            for (var i = 0; i < dopstr.Length; i++)
            {
                if (str2[k + 1] == i) pr2 = pr2 + 1;
            }
            return pr2 != 0;
        }

        /// <summary>
        /// Проверяет значение на тип
        /// </summary>
        /// <param name="str">Строка, в которой осуществляется поиск</param>
        /// <param name="startPos">Начальная позиция, для поиска</param>
        /// <param name="endPos">Конечная позиция, для поиска</param>
        /// <param name="splittedWords"></param>
        /// <returns></returns>
        public int IsThisNumber(string str, int startPos, int endPos, Queue<Word> splittedWords)
        {
            //Проверка булевых переменных
            if (str.Substring(startPos, endPos + 1 - startPos) == " true" ||
                str.Substring(startPos, endPos + 1 - startPos) == " false")
            {
                splittedWords.Enqueue(new Word(17, str.Substring(startPos, endPos + 1 - startPos)));
                return 1;
            }
            //Проверка на число
            if (IsNumber(str, "0123456789", startPos))
            {
                int srchLength = 0;
                for (var i = startPos; i < endPos; i++)
                {
                    if (IsNumber(str, "0123456789.Ee-", i))
                        srchLength = srchLength + 1;
                }
                if (srchLength == endPos - startPos)
                {
                    splittedWords.Enqueue(new Word(17, str.Substring(startPos, endPos + 1 - startPos)));

                    return 1;
                }
                CompilerEvent.PrintMessageLRParser.Invoke(@"Нужно вводить вещественные" + '\n' + @" числа с порядком!" + '\r' +
                                                          @"Ошибка --> " + str.Substring(startPos, endPos + 1 - startPos));
                
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// Метод поиска (парсинга) входной строки
        /// Заполняется массив <see cref="Words"/> нашими символами грамматики (:=, if, и т.д.)
        /// </summary>
        /// <param name="str">Строка, в которой осуществляется поиск</param>
        /// <param name="startPos">Начальная позиция, для поиска</param>
        /// <param name="endPos">Конечная позиция, для поиска</param>
        /// <param name="splittedWords"></param>
        /// <returns> Возвращяется номер символа в грамматике </returns>
        public int IsThisOperator(string str, int startPos, int endPos, Queue<Word> splittedWords)
        {
            if (str[startPos] == ' ') startPos += 1;
            for (var i = CountOfRules; i < Rules.Length; i++)
            {
                if (endPos - startPos + 1 == (Words[i].Value).Length)
                {
                    int srchLength = 0;
                    for (var j = 1; j <= (Words[i].Value).Length; j++)
                    {
                        if (str[startPos + j - 1] == Words[i].Value[j - 1]) srchLength = srchLength + 1;
                    }
                    if (endPos - startPos + 1 == srchLength)
                    {
                        splittedWords.Enqueue(new Word(i, Words[i].Value));
                        return i;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Парсит входную строку
        /// </summary>
        /// <param name="str">Строка, для парсинга</param>
        /// <returns>Очередь распарщенных слов</returns>
        public Queue<Word> Up(string str)
        {

            string[] list = str.Split(' ');



            Queue<string> words = new Queue<string>(list);

            //if (words.Contains("const"))
            //{
            //    throw new Exception("const - служебное слово");
            //}

            // Коллекция строк, которые были получены в результате парсинга строки
            Queue<Word> splittedWords = new Queue<Word>();

            while (words.Count > 0)
            {
                string word = words.Dequeue();

                Word foundWord = Words.FirstOrDefault(item => item.Value == word && item.Number >= CountOfRules && item.Value != "const" && item.Value != "id");

                if (foundWord != null)
                {
                    splittedWords.Enqueue(new Word(foundWord.Number, foundWord.Value));
                }
                else
                {
                    int nuber;
                    //TODO: true или false в качестве const
                    if (int.TryParse(word, out nuber))
                    {
                        nuber = Convert.ToInt32(word, 16);
                        splittedWords.Enqueue(new Word(13, "const") {Temp = nuber.ToString()});
                    }
                    else
                    {
                        splittedWords.Enqueue(new Word(10, "id") { Temp = word });
                    }
                }

                

            }
            splittedWords.Enqueue(new Word(20, "$"));
            //TODO: Не забыть поставить символ конца цепочки
            return splittedWords;
        }

        public void Run(string str)
        {
            Queue<Word> words = Up(str);
            if (words != null)
            {
                Algorithm(words);
            }
        }

        public void Run(object obj)
        {
             string str = obj as string;
            if ( str != null )
            {
                Run(str);
            }
        }
    }
}
