using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_lab
{
    class DataBase
    {
        public Receipt[] m_transactionDataArray;
        private int m_size;
        private bool m_isDiffRAM2CSV;
        private int maxSize;

        public bool neededSave()
        {
            return m_isDiffRAM2CSV;
        }


        //---Инициализация базы данных
        public DataBase(int maxSize)
        {
            m_isDiffRAM2CSV = false;
            m_size = 0;
            this.maxSize = maxSize;
            m_transactionDataArray = new Receipt[maxSize];
        }

        public int dataBaseSize()
        {
            return m_size;
        }


        //---Добавление записи в базу данных 
        public bool add(Receipt data)
        {
            if (m_size >= m_transactionDataArray.Length)
            {
                Receipt[] tmp = new Receipt[m_size * 2];
                for (int i = 0; i < m_size; i++)
                {
                    tmp[i] = m_transactionDataArray[i];
                }
                m_transactionDataArray = tmp;
            }
            m_transactionDataArray[m_size] = data;
            m_size++;

            return true;
        }

        //---Поиск записи в базе данных по записи 
        public int find(Receipt data)
        {
            for (int i = 0; i < m_size; i++)
            {
                if (m_transactionDataArray[i] == data) return i;
            }
            return -1;
        }
        // для удаления по индексу
        public Receipt find(int index)
        {
            if (index >= 0 || index <= m_transactionDataArray.Length - 1)
            {
                return m_transactionDataArray[index];
            }
            return null;
        }

        public int findByClientName(string name, int start = 0)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i].nameClient == name) return i;
            }
            return -1;
        }

        // Кол-во записей в БД
        public int length()
        {
            return m_size;
        }

        //---Поиск записи по имени клиента
        public int findByName(string nameClient)
        {
            for (int i = 0; i < m_size; i++)
            {
                if (m_transactionDataArray[i].nameClient == nameClient) return i;
            }
            return -1;
        }

        //---Поиск записи по адресу ломбарда
        public int findByAddress(string address)
        {
            for (int i = 0; i < m_size; i++)
            {
                if (m_transactionDataArray[i].address == address) return i;
            }
            return -1;
        }

        //---Поиск записи по дате
        public int findByDate(DateTime date)
        {
            for (int i = 0; i < m_size; i++)
            {
                if (m_transactionDataArray[i].date == date) return i;
            }
            return -1;
        }
        public int findByDate(DateTime startTime, DateTime endTime, int start = 0)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i].date >= startTime &&
                    m_transactionDataArray[i].date <= endTime) return i;
            }
            return -1;
        }

        //---Поиск записи по цене
        public int findByPrice(double price)
        {
            for (int i = 0; i < m_size; i++)
            {
                if (m_transactionDataArray[i].price == price) return i;
            }
            return -1;
        }

        //---Поиск записи в рамках определенной цены
        public int findByPrice(double min, double max)
        {
            for (int i = 0; i < m_size; i++)
            {
                if (m_transactionDataArray[i].price >= min &&
                    m_transactionDataArray[i].price <= max) return i;
            }
            return -1;
        }

        public int findNext(Receipt data, int start)
        {
            if (start < 0 || start >= m_size) start = 0;
            for (int i = start; i < m_size; i++)
            {
                if (m_transactionDataArray[i] == data) return i;
            }
            return -1;
        }

        //---Замена записи в базе данных 
        public bool replace(int index, Receipt data)
        {
            if (index >= m_size || index < 0)
            {
                Console.WriteLine("Incorrect index.");
                return false;
            }
            m_transactionDataArray[index] = data;
            return true;
        }
        // замена всех вхожд стар объекта на новый
        public bool replace(Receipt oldObj, Receipt newObj)
        {
            for (int i = 0; i < m_transactionDataArray.Length; i++)
            {
                if (m_transactionDataArray[i] == oldObj)
                {
                    m_transactionDataArray[i] = newObj;
                }
            }
            return true;
        }

        //---Взять запись из базы данных (ссылочка)
        //стильно,модно,молодежн
        public ref Receipt get(int index)
        {
            return ref m_transactionDataArray[index];
        }

        //---Удалить запись из базы данных по записи
        public int delete(Receipt data)
        {
            bool hasDelete = true;
            int count = 0;
            while (hasDelete)
            {
                int index = find(data);
                if (index == -1)
                {
                    hasDelete = false;
                }
                else
                {
                    m_transactionDataArray[index] = null;
                    count++;
                }
            }

            List<Receipt> temp = new List<Receipt>();
            for (int i = 0; i < m_transactionDataArray.Length; i++)
            {
                if (m_transactionDataArray[i] != null)
                {
                    temp.Add(m_transactionDataArray[i]);
                }
            }

            m_transactionDataArray = new Receipt[maxSize];
            /*
            if (temp.Count() == 0)
            {
                m_transactionDataArray = new Receipt[maxSize];
            }
            else
            {
                m_transactionDataArray = temp.ToArray();
            } */

            Receipt[] arrRec = temp.ToArray();

            for (int i = 0; i < arrRec.Length; i++)
            {
                m_transactionDataArray[i] = arrRec[i];
            }
            return count;
        }


        //---Удалить запись из базы данных по индексу 
        public bool delete(int index)
        {
            Receipt searchObj = find(index);
            if (searchObj == null)
            {
                return false;
            }
            delete(searchObj);
            return true;
        }
        //          Полная очистка бд
        public bool clear()
        {
            m_transactionDataArray = new Receipt[0];
            m_size = 0;
            return true;
        }

        //---Сортировка базы данных по имени 
        public void sortingByName(bool ascending = true)
        {
            int temp = 0;
            while (temp != m_size)
            {
                for (int i = 0; i < m_size - 1; i++)
                {
                    int result = m_transactionDataArray[i].nameClient.CompareTo(m_transactionDataArray[i + 1].nameClient);
                    if (result == 1)
                    {
                        Receipt.swap(ref m_transactionDataArray[i + 1], ref m_transactionDataArray[i]);
                    }
                }
                temp++;
            }
            /*
            for (int i = 0; i < m_size - 1; i++)
            {
                for (int j = 0; j < m_size - i -1; j++)
                {
                    if (m_transactionDataArray[j].nameClient.CompareTo(m_transactionDataArray[j + 1].name) == 1)
                    {
                        Receipt.swap(ref m_transactionDataArray[j + 1], ref m_transactionDataArray[j]);
                    }
                }
            }
            */
        }


        //---Сортировка базы данных по номеру телефона клиента
        public void sortingByPhone(bool ascending = true)
        {
            int temp = 0;
            while (temp != m_size)
            {
                for (int i = 0; i < m_size - 1; i++)
                {
                    int result = m_transactionDataArray[i].phoneClient.CompareTo(m_transactionDataArray[i + 1].phoneClient);
                    if (result == 1)
                    {
                        Receipt.swap(ref m_transactionDataArray[i + 1], ref m_transactionDataArray[i]);
                    }
                }
                temp++;
            }
            /*
            for (int i = 0; i < m_size - 1; i++)
            {
                for (int j = 0; j < m_size - i - 1; j++)
                {
                    if (ascending ^ (m_transactionDataArray[j].phoneClient.CompareTo(m_transactionDataArray[j + 1].phoneClient) == 1))
                    {
                        Receipt.swap(ref m_transactionDataArray[j], ref m_transactionDataArray[j + 1]);
                    }
                }
            } */
        }

        //---Сортировка базы данных по дате
        public void sortingByPropertyDate(bool ascending = true)
        {
            int temp = 0;
            while (temp != m_size)
            {
                for (int i = 0; i < m_size - 1; i++)
                {
                    int result = m_transactionDataArray[i].date.CompareTo(m_transactionDataArray[i + 1].date);
                    if (result == 1)
                    {
                        Receipt.swap(ref m_transactionDataArray[i + 1], ref m_transactionDataArray[i]);
                    }
                }
                temp++;
            }
            /*
            for (int i = 0; i < m_size - 1; i++)
            {
                for (int j = 0; j < m_size - i - 1; j++)
                {
                    if (ascending ^ (m_transactionDataArray[j].date.CompareTo(m_transactionDataArray[j + 1].date) == 1))
                    {
                        Receipt.swap(ref m_transactionDataArray[j], ref m_transactionDataArray[j + 1]);
                    }
                }
            }   */
        }

        //---Сортировка базы данных по цене
        public void sortingByPrice(bool ascending = true)
        {
            int temp = 0;
            while (temp != m_size)
            {
                for (int i = 0; i < m_size - 1; i++)
                {
                    int result = m_transactionDataArray[i].price.CompareTo(m_transactionDataArray[i + 1].price);
                    if (result == 1)
                    {
                        Receipt.swap(ref m_transactionDataArray[i + 1], ref m_transactionDataArray[i]);
                    }
                }
                temp++;
            }
            /*
            for (int i = 0; i < m_size - 1; i++)
            {
                for (int j = 0; j < m_size - i - 1; j++)
                {
                    if (ascending ^ (m_transactionDataArray[j].price > m_transactionDataArray[j + 1].price))
                    {
                        Receipt.swap(ref m_transactionDataArray[j], ref m_transactionDataArray[j + 1]);
                    }
                }
            } */
        }

    }
}
