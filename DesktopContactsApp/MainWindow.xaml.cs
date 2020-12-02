using DesktopContactsApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        public MainWindow()
        {
            this.WindowStartupLocation= WindowStartupLocation.CenterScreen;
            InitializeComponent();
            contacts = new List<Contact>();
            ReadDatabase();
        }

        private void NewContactButton_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newContactWindow.ShowDialog();
            ReadDatabase();//Update Contact List
        }

        void ReadDatabase()
        {
             contacts= Repository.GetAllContacts();

            if (contacts != null)
            {

                contactListView.ItemsSource = contacts;
            }


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();

            var filteresList2 = (from c2 in contacts
                               where c2.Name.ToLower().Contains(searchTextBox.Text.ToLower())
                               orderby c2.Email
                               select c2).ToList();

            contactListView.ItemsSource = filteredList;
        }

        private void contactListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = (Contact)contactListView.SelectedItem;

            if(selectedContact !=null)
            {
                ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(selectedContact);
                contactDetailsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                contactDetailsWindow.ShowDialog();
                ReadDatabase();//Update Contact List
            }
        }
    }
}
