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
  public partial class VIEW : Form
  {
    ConectDB cdb = new ConectDB();
    public VIEW()
    {
      InitializeComponent();
    }

    private void CreatedColumns()//
    {
      dataGridView1.Columns.Add("predmet_name", "Название предмета");
      dataGridView1.Columns.Add("cafedra_name", "Название кафедры");
      dataGridView1.Columns.Add("IsNew", String.Empty);
      dataGridView1.Columns[2].Visible = false;
    }
    private void ReadSingleRow(DataGridView dgw, IDataRecord record)
    {
      dgw.Rows.Add(record.GetString(0), record.GetString(1));
    }

    private void RefreshDataGrid(DataGridView dgw)
    {
      dgw.Rows.Clear();
      string queryString = $"select * from TeachView";
      SqlCommand command = new SqlCommand(queryString, cdb.getConnection());
      cdb.openConnection();
      SqlDataReader reader = command.ExecuteReader();
      while (reader.Read())
      {
        ReadSingleRow(dgw, reader);
      }
      reader.Close();
    }

    private void button_create_Symp_Click(object sender, EventArgs e)
    {
      cdb.openConnection();
      var pred = textBox_predmet.Text;
      var kaf = textBox_name_kaf.Text;
      if (textBox_predmet.Text == "" && textBox_name_kaf.Text == "")
      {
        MessageBox.Show("Не все данные вставлены", "Провал!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      else
      {
        var query = $"INSERT INTO TeachView VALUES ('{pred}','{kaf}')";
        var com = new SqlCommand(query, cdb.getConnection());
        com.ExecuteNonQuery();
        MessageBox.Show("Запись создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      cdb.closeConnection();
    }

    private void VIEW_Load(object sender, EventArgs e)
    {
      CreatedColumns();
      RefreshDataGrid(dataGridView1);
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
      RefreshDataGrid(dataGridView1);
    }
  }
}
