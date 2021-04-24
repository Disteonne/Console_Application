using System;
using System.IO;

namespace console_lab
{
    class Program
    {
        private static DataBase data;
        private static String path;
        static void Main(string[] args)
        {
            mainMenu();
        }


        private static void createFunction()
        {
            data = new DataBase(1000);

            //Console.WriteLine("Base is filled \n");
            Console.WriteLine("Next possible step: \n" +
                "1. Select all info without sort \n " +
                "2. Select all info with sort by client Name (ASC) \n " +
                "3. Select all info with sort by client Phone (ASC) \n " +
                "4. Select all info with sort by price (ASC) \n " +
                "5. Select all info with sort by data (ASC) \n " +
                "6. Search object from database \n " +
                "7. Create objects from database \n " +
                "8. Replace object from database \n " +
                "9. Delete object from database \n " +
                "10. Exit from application \n " +
                "11. Clear database \n " +
                "12. Exit to main menu \n " +
                "13. Save as...");
            functionActions();

        }

        private static void functionActions()
        {
            Console.WriteLine("Input action: ");
            if (data.dataBaseSize() == 0)
            {
                Console.WriteLine("Recomended input records ,because db is empty");
            }
            switch (Console.ReadLine())
            {
                case "1":
                    selectAll();
                    functionActions();
                    break;

                case "2":
                    {
                        data.sortingByName();
                        selectAll();
                        functionActions();
                    }
                    break;

                case "3":
                    {
                        data.sortingByPhone();
                        selectAll();
                        functionActions();
                    }
                    break;

                case "4":
                    {
                        data.sortingByPrice();
                        selectAll();
                        functionActions();
                    }
                    break;

                case "5":
                    {
                        data.sortingByPropertyDate();
                        selectAll();
                        functionActions();
                    }
                    break;
                case "6":
                    {
                        Console.WriteLine("Input info about search record.");
                        int index = data.find(input());
                        if (index == -1)
                        {
                            Console.WriteLine("Object is not founded");
                        }
                        else
                        {
                            Receipt result = data.m_transactionDataArray[index];
                            Console.WriteLine(result.toString());
                        }
                        functionActions();
                    }
                    break;

                case "7":
                    {
                        createRecords();
                        Console.WriteLine("Done");
                        functionActions();
                    }
                    break;
                case "8":
                    {
                        Console.WriteLine("Input old  info about record: ");
                        Receipt oldInfo = input();
                        Console.WriteLine("Input new info about record: ");
                        Receipt newInfo = input();
                        int index = data.find(oldInfo);
                        if (index == -1)
                        {
                            Console.WriteLine("Object is not founded");
                        }
                        else {
                            data.replace(index, newInfo);
                        }
                        functionActions();
                    }
                    break;
                case "9":
                    {
                        Console.WriteLine("Input info about object which will be deleted");
                        Receipt delete = input();
                        int index = data.find(delete);
                        if (index == -1)
                        {
                            Console.WriteLine("Object is not founded");
                        }
                        else
                        {
                            data.delete(index);
                            Console.WriteLine("Done");
                        }
                        functionActions();
                    }
                    break;
                case "10":
                    break;
                case "11":
                    {
                        data = new DataBase(1000);
                    }
                    break;
                case "12":
                    {
                        mainMenu();
                    }
                    break;
                case "13":
                    {
                        Console.WriteLine("Input path to file for create DB.txt");
                        String pathToDirectoryForCreate = Console.ReadLine();
                        if (!Directory.Exists(pathToDirectoryForCreate))
                        {
                          Directory.CreateDirectory(pathToDirectoryForCreate);
                          
                        }
                            IOHelper.createFileFromDataBase(data, pathToDirectoryForCreate+"\\DB.txt");
                            path = Path.GetFileName(path);
                        functionActions();
                    }
                    break;
            }
        }

        private static void selectAll()
        {
            for (int i = 0; i < data.dataBaseSize(); i++)
            {
                Console.WriteLine(data.m_transactionDataArray[i].toString());
            }
        }

        private static Receipt input()
        {
            Console.WriteLine("Name");
            String name = Console.ReadLine();
            Console.WriteLine("Price");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("nameClient");
            String nameClient = Console.ReadLine();
            Console.WriteLine("phoneClient");
            String phoneClient = Console.ReadLine();
            Console.WriteLine("nameReceipt");
            String nameReceipt = Console.ReadLine();
            Console.WriteLine("address");
            String address = Console.ReadLine();
            Console.WriteLine("date");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            return new Receipt(name, price, nameClient, phoneClient, nameReceipt, address, date);
        }

        private static void createRecords()
        {
            //создание объектов
            Console.WriteLine("Create object. Input next fields and if the end of input then input word 'end'");
            Console.WriteLine("Input ok");
            while (!Console.ReadLine().Equals("end"))
            {
                
                data.add(input());

                Console.WriteLine("input whitespace");
            }
        }

        private static void mainMenu()
        {
            Console.WriteLine("Input choice: 1 - create new , 2 - open db");
            String inputChoice = Console.ReadLine();
            if (inputChoice.Equals("1"))
            {
                createFunction();
            }else if (inputChoice.Equals("2"))
            {
                createFromBd();
            }
            else
            {
                Console.WriteLine("Input error.Please,restart application");
            }
        }

        private static void createFromBd()
        {
            Console.WriteLine("Input path of document,please.");
            String pathForOpen = Console.ReadLine();
            if (IOHelper.isReadFile(pathForOpen))
            {
                data = IOHelper.createDataBaseFromFile(pathForOpen);
            }
            functionActions();
        }
    }


}
