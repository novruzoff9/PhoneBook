using MetroFramework.Forms;
using Microsoft.Extensions.Configuration;
using PhoneBook.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace PhoneBook
{
    public partial class ListForm : MetroForm
    {
        PhoneBookContext _context;
        public ListForm()
        {
            InitializeComponent();
            _context = new PhoneBookContext();
        }

        #region Load Function
        void Refresh()
        {
            lstPeople.Items.Clear();

            
            List<Person> people = _context.Set<Person>().ToList();

            foreach (Person person in people) 
            {
                ListViewItem item = new(person.Id.ToString());
                item.SubItems.Add(person.FirstName);
                item.SubItems.Add(person.LastName);
                item.SubItems.Add(person.Phone);
                item.SubItems.Add(person.Mail);

                lstPeople.Items.Add(item);
            }
        }
        #endregion

        //string connection = Program.Configuration.GetConnectionString("default");
        string connection = Properties.PhoneBook.Default.Connection;
        private void ListForm_Load(object sender, EventArgs e)
        {
            Refresh();
        }
        private void cmsRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }


        private void cmsSil_Click(object sender, EventArgs e)
        {
            if (lstPeople.SelectedItems.Count == 0)
            {
                MessageBox.Show(
                    "Please select a record to delete.",
                    "Delete Item",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            DialogResult dialogResult = MessageBox.Show(
                "Are you sure you want to delete the selected record?",
                "Delete Item",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);


            if (dialogResult == DialogResult.No)
            {
                return;
            }

            int selectedId = Convert.ToInt32(lstPeople.SelectedItems[0].Text);
            Person person = _context.Set<Person>().FirstOrDefault(x=>x.Id == selectedId);
            _context.Set<Person>().Remove(person);
            bool result = _context.SaveChanges() > 0;
            if (result)
            {
                lstPeople.SelectedItems[0].Remove();
            }

            MessageBox.Show(
                text: result ? "Kayıt Silindi" : "İşlem Hatası",
                caption: "Kayıt Silme Bildirimi",
                buttons: MessageBoxButtons.OK,
                icon: result ? MessageBoxIcon.Asterisk : MessageBoxIcon.Error
            );
        }

        private void ListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.MainFormInstance.Show();
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            if (lstPeople.SelectedItems.Count == 0)
            {
                MessageBox.Show(
                    "Please select a record to update.",
                    "Update Item",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }
             
            int selectedId = Convert.ToInt32(lstPeople.SelectedItems[0].Text);
            EditForm frm = new(selectedId); 
            frm.ShowDialog();
        }
    }
}
