using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Coursework56
{
  public partial class EnterForm : Form
  {
    ConectDB cdb = new ConectDB();
    public EnterForm()
    {
      InitializeComponent();
    }

   
    private void button1_Click(object sender, EventArgs e)
    {
      string gr_num = textBox1.Text;

      SqlDataAdapter adapter = new SqlDataAdapter();
      DataTable table = new DataTable();

      string querystring = $"select groupp_name from groupp where groupp_name = '{gr_num}'";
      SqlCommand command = new SqlCommand(querystring, cdb.getConnection());
      adapter.SelectCommand = command;
      adapter.Fill(table);
      if (table.Rows.Count == 1)
      {
        MessageBox.Show("Вы успешно вошли в аккаунт", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        StudUser studentform = new StudUser(cdb.getConnection(), gr_num);
        this.Hide();
        studentform.ShowDialog();
        this.Show();
      }
      else MessageBox.Show("Вход не выполнен", "Такого аккаунта нет", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
  }
}
