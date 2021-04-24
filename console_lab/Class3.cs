using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace console_lab
{
    class IOHelper
    {

        public static Receipt createTransactionData(string name, string price,
                                                    string nameClient, string phoneClient,
                                                    string nameReceipt, string address,
                                                    string date)//дата
        {
            Receipt result = new Receipt();
            try
            {

                result.name = name;
                result.price = Convert.ToDouble(price.Replace('.', ','));
                result.nameClient = nameClient;
                result.phoneClient = phoneClient;
                result.nameReceipt = nameReceipt;
                result.address = address;
                result.date = Convert.ToDateTime(date);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return result;
        }

        public static DataBase createDataBaseFromFile(string path)
        {
            DataBase res = new DataBase(0);
            string correctPath = strValid(path);
            string line;
            try
            {
                StreamReader sr = new StreamReader(correctPath);
                res = new DataBase(sizeValid(sr.ReadLine()));
                line = sr.ReadLine();
                while (line != null && line !="")
                {
                    string[] words = line.Split(';');
                    String resu = "";
                    // if (words.Length != Receipt.memberSize)
                    // {   //можно эксепшн
                    //     return res;
                    // }

                     Receipt record = new Receipt(strValid(words[0]), Convert.ToInt32(priceValid(words[1])),
                        strValid(words[2]), phoneValid(words[3]), strValid(words[4]), strValid(words[5]),
                        dataValid(words[6]));
                    res.add(record);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return res;
        }

        public static Boolean createFileFromDataBase(DataBase dataBase, string path)
        {
            string correctPath = path;
            try
            {
                StreamWriter sw = new StreamWriter(correctPath);
                sw.WriteLine(dataBase.dataBaseSize());
                for (int i = 0; i < dataBase.dataBaseSize(); i++)
                {
                    Receipt record = dataBase.get(i);
                    sw.WriteLine(record.name + ";" + record.price + ";" + record.nameClient + ";" +
                            record.phoneClient + ";" +
                          record.nameReceipt + ";" + record.address + ";" + record.date);
                }
                sw.Close();
                return true;
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return false;
        }

        public static bool isReadFile(string path)
        {
            try
            {
                File.Open(path, FileMode.Open, FileAccess.Read).Dispose();
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
        }

        public static bool isWrittenFile(string path)
        {
            try
            {
                File.Open(path, FileMode.Open, FileAccess.Write).Dispose();
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
        }

        public static string strValid(string str)
        {
            if (str == null)
            {

                Console.WriteLine("строка пуста strVal");
            }
            else if (str.Length == 0)
            {

                Console.WriteLine("строка пуста strVal");
            }
            return str;
        }

        public static DateTime dataValid(string str)
        {
            DateTime res = new DateTime();
            try
            {
                if (str == null)
                {
                    Console.WriteLine("строка пуста dataVal");
                }
                else if (str.Length == 0)
                {
                    Console.WriteLine("строка пуста dataVal");
                }
                res = Convert.ToDateTime(str);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                //Exception.ExceptionDataType(str);
            }
            return res;
        }

        public static int priceValid(string str)
        {
            if (str == null)
            {
                Console.WriteLine("строка пуста price valid");
            }
            else if (str.Length == 0)
            {

                Console.WriteLine("строка пуста price valid");
            }
            else if (str[0] == '-')
            {

                Console.WriteLine("строка отриц price valid");
            }

            int startIndex = 0;
            for (; startIndex < str.Length && str[startIndex] == '0'; startIndex++) { }
            if (startIndex == str.Length) return 0;
            string trueStr = str.Substring(startIndex);

            if (trueStr.Length > Int32.MaxValue.ToString().Length)
            {

                Console.WriteLine("цена полна pr val");
            }
            if (trueStr.Length == Int32.MaxValue.ToString().Length &&
                string.Compare(trueStr, Int32.MaxValue.ToString()) == 1)
            {
                Console.WriteLine("цена полна pr val");
            }

            for (int i = 0; i < trueStr.Length; i++)
            {
                if (trueStr[i] != '0' && trueStr[i] != '1' && trueStr[i] != '2' && trueStr[i] != '3' &&
                   trueStr[i] != '4' && trueStr[i] != '5' && trueStr[i] != '6' && trueStr[i] != '7' &&
                   trueStr[i] != '8' && trueStr[i] != '9')
                {
                    Console.WriteLine("номер не корректен pr val");
                }
            }

            return Convert.ToInt32(trueStr);
        }

        public static int sizeValid(string str)
        {
            if (str == null)
            {
                Console.WriteLine("строка пуста");
            }
            else if (str.Length == 0)
            {
                Console.WriteLine("строка пуста");
            }
            else if (str[0] == '-')
            {
                Console.WriteLine("Размер < 0");
            }

            int startIndex = 0;
            for (; startIndex < str.Length && str[startIndex] == '0'; startIndex++) { }
            if (startIndex == str.Length) return 0;
            string trueStr = str.Substring(startIndex);

            if (trueStr.Length > Int32.MaxValue.ToString().Length)
            {
                Console.WriteLine("переполнение цены");
            }
            if (trueStr.Length == Int32.MaxValue.ToString().Length &&
                string.Compare(trueStr, Int32.MaxValue.ToString()) == 1)
            {
                Console.WriteLine("переполнение цены");
            }

            for (int i = 0; i < trueStr.Length; i++)
            {
                if (trueStr[i] != '0' && trueStr[i] != '1' && trueStr[i] != '2' && trueStr[i] != '3' &&
                   trueStr[i] != '4' && trueStr[i] != '5' && trueStr[i] != '6' && trueStr[i] != '7' &&
                   trueStr[i] != '8' && trueStr[i] != '9')
                {
                    Console.WriteLine("некорректность номера");
                }
            }

            return Convert.ToInt32(trueStr);
        }

        public static string phoneValid(string str)
        {
            if (str == null)
            {
                Console.WriteLine("номер пуст");
            }
            else if (str.Length == 0)
            {
                Console.WriteLine("номер пуст");
            }
            else if ((str[0] != '8' && str.Length != '+') ||
                     (str[0] == '+' && str.Length != 12) ||
                     (str[0] == '8' && str.Length != 11))
            {
                Console.WriteLine("некорректность номера");
            }

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] != '0' && str[i] != '1' && str[i] != '2' && str[i] != '3' &&
                   str[i] != '4' && str[i] != '5' && str[i] != '6' && str[i] != '7' &&
                   str[i] != '8' && str[i] != '9')
                {
                    Console.WriteLine("некорректность номера: букв.символ");
                }
            }

            return str;
        }
    }
}
