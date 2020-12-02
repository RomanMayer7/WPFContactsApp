using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopContactsApp.Data
{
    class Repository
    {
        static string databaseName = "Contacts.db";
        static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string databasePath = System.IO.Path.Combine(folderPath, databaseName);

        public static List<Contact> GetAllContacts()
        {

            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.CreateTable<Contact>();
                var contacts = connection.Table<Contact>().OrderBy(c => c.Name).ToList();//Casting IENumerable to List
                return contacts;
            }

        }

        public static void AddContact(Contact contact)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Insert(contact);
            }
            //connection.Close();
        }

        public static void UpdateContact(Contact contact)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.CreateTable<Contact>();//Ensure the Table Exist,If Not Create it
                connection.Update(contact);//Only possible update the corresponding record in DB if contact have the Id property

            }
        }

        public static void DeleteContact(Contact contact)
        {

            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.CreateTable<Contact>();//Ensure the Table Exist,If Not Create it
                connection.Delete(contact);//Only possible delete the corresponding record in DB if contact have the Id property

            }
        }

    }
}
