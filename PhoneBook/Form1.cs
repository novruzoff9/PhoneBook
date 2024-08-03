using MetroFramework.Forms;
using Microsoft.Extensions.Configuration;
using PhoneBook.Models;
using System.Data;
using System.Data.SqlClient;

namespace PhoneBook
{
    public partial class Form1 : MetroForm
    {
        PhoneBookContext _context;
        public Form1()
        {
            InitializeComponent();
            _context = new PhoneBookContext();
        }

        void Clear(Control ctrl)
        {
            foreach (Control item in ctrl.Controls)
            {
                if (item is TextBox txt)
                {
                    txt.Clear();
                }
                else if (item is NumericUpDown nmr)
                {
                    nmr.Value = nmr.Minimum;
                }
                else if (item is ComboBox cmb)
                {
                    cmb.SelectedIndex = -1;
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            Person person = new Person();
            person.FirstName = txtFirstName.Text;
            person.LastName = txtLastName.Text;
            person.Phone = txtPhone.Text;
            person.Mail = txtMail.Text;

            _context.Set<Person>().Add(person);
            bool result = _context.SaveChanges() > 0;

            //MessageBox.Show(result ? "Kayıt Eklendi" : "İşlem Hatası");
            MessageBox.Show(
                  text: result ? "Kayıt Eklendi" : "İşlem Hatası",
                  caption: "Kayıt Eklleme Bildirimi",
                  buttons: MessageBoxButtons.OK,
                  icon: result ? MessageBoxIcon.Asterisk : MessageBoxIcon.Error
            );

            //txtFirstName.Text = txtLastName.Text = txtMail.Text = txtPhone.Text = string.Empty; 
            //txtFirstName.Text = "";
            //txtLastName.Clear();
            //txtMail.Text = string.Empty;
            Clear(grbSavePerson);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           Program.MainFormInstance.Show();
        } 
    }
}
