using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_lab
{
    class Receipt
    {

        public static int memberSize = 7;
        public String name;
        public double price;
        //          Client
        public String nameClient;
        public String phoneClient;
        //          Receipt
        public String nameReceipt;
        public String address;

        public DateTime date;

        public Receipt() { }
        public Receipt(string name, double price, string nameClient, string phoneClient, string nameReceipt, string address, DateTime date)
        {
            this.name = name;
            this.price = price;
            this.nameClient = nameClient;
            this.phoneClient = phoneClient;
            this.nameReceipt = nameReceipt;
            this.address = address;
            this.date = date;
        }

        public Receipt(Receipt receipt)
        {
            this.name = receipt.name;
            this.price = receipt.price;
            this.nameClient = receipt.nameClient;
            this.phoneClient = receipt.phoneClient;
            this.nameReceipt = receipt.nameReceipt;
            this.address = receipt.address;
            this.date = receipt.date;
        }

        public static bool operator !=(Receipt leftObject, Receipt rightObject)
        {
            if (leftObject is null)
            {
                if (rightObject is null) return false;
                else return true;
            }
            else if (rightObject is null)
            {
                return true;
            }
            return leftObject.name != rightObject.name ||
                leftObject.price != rightObject.price ||
                leftObject.nameClient != rightObject.nameClient ||
                leftObject.phoneClient != rightObject.phoneClient ||
                leftObject.nameReceipt != rightObject.nameReceipt ||
                leftObject.address != rightObject.address ||
                leftObject.date != rightObject.date;
        }
        public static bool operator ==(Receipt leftObject, Receipt rightObject)
        {
            return !(leftObject != rightObject);
        }

        public int validate()
        {
            if (this.name == null || this.name.Length == 0)
                return 0;
            if (this.price <= 0)
                return 0;
            if (this.nameClient == null || this.nameClient.Length == 0)
                return 0;
            if (this.phoneClient == null || this.phoneClient.Length == 0)
                return 0;
            if (this.nameReceipt == null || this.nameReceipt.Length == 0)
                return 0;
            if (this.address == null || this.address.Length == 0)
                return 0;
            return 1;
        }

        public override bool Equals(object obj)
        {
            return obj is Receipt receipt &&
                   name == receipt.name &&
                   price == receipt.price &&
                   nameClient == receipt.nameClient &&
                   phoneClient == receipt.phoneClient &&
                   nameReceipt == receipt.nameReceipt &&
                   address == receipt.address &&
                   date == receipt.date;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, price, nameClient, phoneClient, nameReceipt, address, date);
        }

        public static void swap(ref Receipt lhs, ref Receipt rhs)
        {
            Receipt tmp = new Receipt(lhs);
            lhs = new Receipt(rhs);
            rhs = new Receipt(tmp);
        }

        public  String toString()
        { 
            return "Record: Name " + name + " ,price " + price + " ,nameClient " + nameClient + "," +
                "phoneClient " + phoneClient + " ,nameReceipt " + nameReceipt + " ,address " + address + "," +
                " data " + date;
        }
    }
}
