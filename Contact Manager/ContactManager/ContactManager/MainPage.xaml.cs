using ContactManager.Enum;
using ContactManager.Model;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ContactManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Contact contact = new Contact();
        private ObservableCollection<Contact> contacts = new ObservableCollection<Contact>()
        {
            new Contact(){FirstName= "Jacky", LastName="Luo", ContactGroup = "Family", Emails = new List<Email>(), PhoneNumbers = new List<PhoneNumber>()},
            new Contact(){FirstName= "Jacky", LastName="Luo", ContactGroup = "Family", Emails = new List<Email>(), PhoneNumbers = new List<PhoneNumber>()},
            new Contact(){FirstName= "Jacky", LastName="Luo", ContactGroup = "Family", Emails = new List<Email>(), PhoneNumbers = new List<PhoneNumber>()}

        };

        private int selectedIndex = 1;
        private string mruToken;
        public MainPage()
        {
            this.InitializeComponent();
            selectedIndex = contacts.Count + 1;
            ContactDisplay.ItemsSource = contacts;
        }

        private async void AddEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Regex reg = new Regex("^[a-zA-Z]+@[a-zA-Z]+\\.[a-zA-Z]+$");
            if (!reg.IsMatch(EmailField.Text))
            {
                await (new MessageDialog("Please enter a email in the right format: string@email.com")).ShowAsync();

            }
            else if (EmailTypeField.SelectedValue == null)
            {
                await (new MessageDialog("Please select a email type.")).ShowAsync();

            }
            else
            {
                Email email = new Email
                {
                    EmailContact = EmailField.Text,
                    EmailType = EmailTypeField.SelectedValue.ToString()
                };
                contact.Emails.Add(email);
                TextBlock tb = new TextBlock
                {
                    Text = email.ToString(),
                    Margin = new Thickness(5, 0, 0, 0)
                };
                tb.Tapped += RemoveEmail_Tapped;
                EmailDisplay.Children.Add(tb);
            }
        }


        private async void AddPhone_Tapped(object sender, TappedRoutedEventArgs e)
        {

            Regex reg = new Regex("[1-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]");
            if (!reg.IsMatch(PhoneNumberField.Text))
            {
                await (new MessageDialog("Please enter a phone number in the right format: 123-45-7586")).ShowAsync();
            }
            else if (PhoneTypeField.SelectedValue == null)
            {
                await (new MessageDialog("Please select a phone number type")).ShowAsync();
            }
            else
            {
                PhoneNumber phone = new PhoneNumber
                {
                    Number = PhoneNumberField.Text,
                    PhoneType = PhoneTypeField.SelectedValue.ToString()
                };
                contact.PhoneNumbers.Add(phone);
                TextBlock tb = new TextBlock
                {
                    Text = phone.ToString(),
                    Margin = new Thickness(5, 0, 0, 0)

                };
                tb.Tapped += RemovePhone_Tapped;
                PhoneDisplay.Children.Add(tb);
            }
        }

        private async void SaveContact_Click(object sender, RoutedEventArgs e)
        {
            Regex reg = new Regex("^[A-Z][a-zA-Z]+$");
            if (FirstNameField.Text == null || FirstNameField.Text == "" || !reg.IsMatch(FirstNameField.Text))
            {
                await (new MessageDialog("Please enter a first name in proper format: Fname.")).ShowAsync();

            }
            else if (LastNameField.Text == null || LastNameField.Text == "" || !reg.IsMatch(LastNameField.Text))
            {
                await (new MessageDialog("Please enter a last name in proper format: Lname.")).ShowAsync();

            }
            else if (GroupField.SelectedValue == null)
            {
                await (new MessageDialog("Please select a contact group.")).ShowAsync();

            }
            else
            {
                contact.FirstName = FirstNameField.Text;
                contact.LastName = LastNameField.Text;
                contact.ContactGroup = GroupField.SelectedValue.ToString();
                if (selectedIndex > ContactDisplay.Items.Count)
                {
                    contacts.Add(contact);
                    //ContactDisplay.Items.Add(contacts.Last().ToString());
                }
                else
                {
                    contacts[selectedIndex] = contact;
                    if (ContactDisplay.Items[selectedIndex].ToString() != contacts[selectedIndex].ToString())
                    {
                        ContactDisplay.Items[selectedIndex] = contacts[selectedIndex].ToString();
                    }
                    else
                    {
                        ContactDisplay.SelectedIndex = -1;
                    }
                }
                contact = new Contact();
                FirstNameField.Text = "";
                LastNameField.Text = "";
                PhoneNumberField.Text = "";
                EmailField.Text = "";
                PhoneDisplay.Children.Clear();
                EmailDisplay.Children.Clear();
                selectedIndex = contacts.Count + 1;
            }
        }

        private void ContactDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContactDisplay.SelectedIndex >= 0)
            {
                selectedIndex = ContactDisplay.SelectedIndex;
                contact = contacts[selectedIndex];
                PhoneDisplay.Children.Clear();
                EmailDisplay.Children.Clear();
                FirstNameField.Text = contact.FirstName;
                LastNameField.Text = contact.LastName;
                GroupField.SelectedValue = contact.ContactGroup;
                foreach (PhoneNumber phone in contact.PhoneNumbers)
                {
                    TextBlock tb = new TextBlock
                    {
                        Text = phone.ToString(),
                        Margin = new Thickness(5, 0, 0, 0)

                    };
                    tb.Tapped += RemovePhone_Tapped;
                    PhoneDisplay.Children.Add(tb);
                }
                foreach (Email email in contact.Emails)
                {
                    TextBlock tb = new TextBlock
                    {
                        Text = email.ToString(),
                        Margin = new Thickness(5, 0, 0, 0)
                    };
                    tb.Tapped += RemoveEmail_Tapped;
                    EmailDisplay.Children.Add(tb);
                }
            }
        }

        private void RemoveEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is TextBlock)
            {
                TextBlock tb = sender as TextBlock;
                for (int i = 0; i < contact.Emails.Count; i++)
                {
                    if (contact.Emails[i].ToString().Equals(tb.Text))
                    {
                        contact.Emails.RemoveAt(i);
                    }
                }
                EmailDisplay.Children.Remove(tb);
            }
        }

        private void RemovePhone_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is TextBlock)
            {
                TextBlock tb = sender as TextBlock;
                for (int i = 0; i < contact.PhoneNumbers.Count; i++)
                {
                    if (contact.PhoneNumbers[i].ToString().Equals(tb.Text))
                    {
                        contact.PhoneNumbers.RemoveAt(i);
                    }
                }
                PhoneDisplay.Children.Remove(tb);
            }
        }

        private async void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.FileTypeChoices.Add("Contact Document", new List<string>() { ".cscontact" });
            fileSavePicker.SuggestedFileName = "New Contacts";
            StorageFile file = await fileSavePicker.PickSaveFileAsync();
            if (file != null)
            {
                using (Stream fs = await file.OpenStreamForWriteAsync())
                {
                    CachedFileManager.DeferUpdates(file);
                    Serializer.Serialize(fs, contacts);
                    var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
                    mruToken = mru.Add(file, "contacts");
                }
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
            if (mruToken != null || mruToken != "")
            {
                StorageFile file = await mru.GetFileAsync(mruToken);
                if (file != null)
                {
                    using (Stream fs = await file.OpenStreamForWriteAsync())
                    {
                        CachedFileManager.DeferUpdates(file);
                        Serializer.Serialize(fs, contacts);
                    }
                }
                else
                {
                    SaveAsButton_Click(sender, e);
                }
            }
            else
            {
                SaveAsButton_Click(sender, e);
            }
        }

        private async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Add(".cscontact");
            StorageFile file = await fileOpenPicker.PickSingleFileAsync();
            if (file != null)
            {
                using (Stream fs = await file.OpenStreamForReadAsync())
                {
                    contacts.Clear();
                    foreach(Contact conta in Serializer.Deserialize<ObservableCollection<Contact>>(fs))
                    {
                        contacts.Add(conta);
                    }
                    //contacts = Serializer.Deserialize<ObservableCollection<Contact>>(fs);
                    //ContactDisplay.Items.Clear();
                    FirstNameField.Text = "";
                    LastNameField.Text = "";
                    PhoneNumberField.Text = "";
                    EmailField.Text = "";
                    PhoneDisplay.Children.Clear();
                    EmailDisplay.Children.Clear();
                    //foreach (Contact c in contacts)
                    //{
                    //    ContactDisplay.Items.Add(c.ToString());
                    //}
                    var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
                    mruToken = mru.Add(file, "contacts");
                    selectedIndex = ContactDisplay.Items.Count + 1;
                }
            }
        }

        private async void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            if (ContactDisplay.Items.Count != 0 && ContactDisplay.SelectedIndex > 0)
            {
                contacts.RemoveAt(ContactDisplay.SelectedIndex);
                //ContactDisplay.Items.RemoveAt(ContactDisplay.SelectedIndex);
                contact = new Contact();
                FirstNameField.Text = "";
                LastNameField.Text = "";
                PhoneNumberField.Text = "";
                EmailField.Text = "";
                PhoneDisplay.Children.Clear();
                EmailDisplay.Children.Clear();
                selectedIndex = ContactDisplay.Items.Count + 1;
            }
            else
            {
                await (new MessageDialog("Please select a contact to delete.")).ShowAsync();

            }
        }


    }
}
