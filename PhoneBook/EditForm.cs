using MetroFramework.Forms;
using Microsoft.Extensions.Configuration;
using PhoneBook.Models;
using System.Data;
using System.Data.SqlClient;

namespace PhoneBook
{
    public partial class EditForm : MetroForm
    {

        private int _id;
        PhoneBookContext _context;
        public EditForm(int Id)
        {
            _id = Id;
            InitializeComponent();
            _context = new PhoneBookContext();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            Person person = _context.Set<Person>().FirstOrDefault(x => x.Id == _id);

            txtFirstName.Text = person.FirstName;
            txtLastName.Text = person.LastName;
            txtPhone.Text = person.Phone;
            txtMail.Text = person.Mail;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Person person = _context.Set<Person>().Find(_id);
            person.FirstName = txtFirstName.Text;
            person.LastName = txtLastName.Text;
            person.Phone = txtPhone.Text;
            person.Mail = txtMail.Text;
            _context.Set<Person>().Update(person);
            bool result = _context.SaveChanges() > 0;

            if (result)
            {
                MessageBox.Show(
                  text: "Kayıt Duzenlendi",
                  caption: "Kayıt Eklleme Bildirimi",
                  buttons: MessageBoxButtons.OK,
                  icon: MessageBoxIcon.Asterisk
                );
                this.Close();
            }
        }
    }
}
