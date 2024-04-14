using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework56
{
  public partial class LoginUser : Form
  {
    ConectDB cdb = new ConectDB();
    public LoginUser()
    {
      InitializeComponent();
    }

    private void LoginUser_Load(object sender, EventArgs e)
    {
      textBox2.PasswordChar = '*';
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var login = textBox1.Text;
      var password = textBox2.Text;

      if (login.Equals("prepod") && password.Equals("prepod"))
      {
        cdb.openConnection();
        MessageBox.Show("Вход выполнен", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        PrepodForm prepod = new PrepodForm();
        this.Hide();
        prepod.ShowDialog();
        this.Show();
        cdb.closeConnection();
      }
      if (login.Equals("student") && password.Equals("student"))
      {
        cdb.openConnection();
        MessageBox.Show("Вход выполнен", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        EnterForm enterform = new EnterForm();
        this.Hide();
        enterform.ShowDialog();
        this.Show();
        cdb.closeConnection();
      }
      else
      {
        MessageBox.Show("Вход не выполнен", "Такого аккаунта нет", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }
  }
}
