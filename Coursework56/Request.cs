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
  public partial class Request : Form
  {
    ConectDB cdb = new ConectDB();
    public Request()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      cdb.openConnection();
      SqlDataAdapter adapter = new SqlDataAdapter(
        "SELECT c.cafedra_name AS 'Название кафедры',CASE WHEN c.ID_cafedra = 3 THEN (SELECT predmet_name FROM predmet p INNER JOIN cafedra c  ON p.ID_predmet = c.predmet_name_id WHERE(predmet_name = 'Технологии искусственного интеллекта')) WHEN c.ID_cafedra <> 3 THEN '0' WHEN c.ID_cafedra IS NULL THEN 'Кафедра не указана' END 'Дисциплина' FROM cafedra c INNER JOIN predmet p  ON c.predmet_name_id = p.ID_predmet",
        cdb.getConnection());
      DataSet dataSet = new DataSet();
      adapter.Fill(dataSet);
      dataGridView1.DataSource = dataSet.Tables[0];
    }

    private void button2_Click(object sender, EventArgs e)
    {
      cdb.openConnection();
      SqlDataAdapter adapter = new SqlDataAdapter(
        "SELECT t.last_name AS 'Фамилия',t.first_name AS 'Имя',t.patronymic AS 'Отчество',(SELECT cafedra_name  FROM cafedra WHERE cafedra_name = 'Кафедра вычислительной техники') AS 'Кафедра' FROM teacher t INNER JOIN cafedra c ON t.cafedra_id = c.ID_cafedra WHERE god_prepod = 10",
        cdb.getConnection());
      DataSet dataSet = new DataSet();
      adapter.Fill(dataSet);
      dataGridView1.DataSource = dataSet.Tables[0];
    }

    private void button3_Click(object sender, EventArgs e)
    {
      cdb.openConnection();
      SqlDataAdapter adapter = new SqlDataAdapter(
        "SELECT t1.last_name AS 'Фамилия',t1.first_name AS 'Имя',t1.patronymic AS 'Отчество' FROM(SELECT t.last_name, t.first_name, t.patronymic FROM teacher t WHERE t.god_prepod > 5)t1",
        cdb.getConnection());
      DataSet dataSet = new DataSet();
      adapter.Fill(dataSet);
      dataGridView1.DataSource = dataSet.Tables[0];
    }

    private void button4_Click(object sender, EventArgs e)
    {
      cdb.openConnection();
      SqlDataAdapter adapter = new SqlDataAdapter(
        "SELECT c.cafedra_name AS ' Название кафедры' FROM cafedra c INNER JOIN predmet p ON p.ID_predmet = c.predmet_name_id WHERE EXISTS(SELECT p.predmet_name  FROM predmet WHERE p.predmet_name = 'Компьютерная безопасность')",
        cdb.getConnection());
      DataSet dataSet = new DataSet();
      adapter.Fill(dataSet);
      dataGridView1.DataSource = dataSet.Tables[0];
    }

    private void button5_Click(object sender, EventArgs e)
    {
      cdb.openConnection();
      SqlDataAdapter adapter = new SqlDataAdapter(
        "SELECT a.ID_audit_nagruz  AS 'Тип аудиторной нагрузки', a.time_kolvo  AS 'Кол-во часов',(SELECT c.homework FROM control_form c WHERE a.control_form_id = c.ID_control_form)  AS 'Домашняя работа' FROM audit_nagruz a",
        cdb.getConnection());
      DataSet dataSet = new DataSet();
      adapter.Fill(dataSet);
      dataGridView1.DataSource = dataSet.Tables[0];
    }

    private void button6_Click(object sender, EventArgs e)
    {
      cdb.openConnection();
      SqlDataAdapter adapter = new SqlDataAdapter(
        "SELECT c.cafedra_name AS 'Название кафедры' FROM cafedra c WHERE EXISTS(SELECT email FROM teacher t WHERE c.ID_cafedra = t.cafedra_id  AND t.email = 'sergeev@mail.ru')",
        cdb.getConnection());
      DataSet dataSet = new DataSet();
      adapter.Fill(dataSet);
      dataGridView1.DataSource = dataSet.Tables[0];
    }

    private void button7_Click(object sender, EventArgs e)
    {
      cdb.openConnection();
      SqlDataAdapter adapter = new SqlDataAdapter(
        "SELECT c.cafedra_name AS 'Название кафедры' FROM cafedra c WHERE EXISTS(SELECT p.predmet_name FROM predmet p WHERE p.ID_predmet = c.predmet_name_id AND(p.predmet_name = 'Информатика'  OR  p.predmet_name = 'Компьютерная безопасность'))",
        cdb.getConnection());
      DataSet dataSet = new DataSet();
      adapter.Fill(dataSet);
      dataGridView1.DataSource = dataSet.Tables[0];
    }

    private void button8_Click(object sender, EventArgs e)
    {
      cdb.openConnection();
      SqlDataAdapter adapter = new SqlDataAdapter(
        "SELECT t.last_name AS 'Фамилия преподавателя', c.cafedra_name AS 'Кафедра',max(t.god_prepod) AS 'Опыт преподавания (макс)' FROM teacher t  INNER JOIN cafedra c ON t.cafedra_id = c.ID_cafedra GROUP BY t.last_name, c.cafedra_name HAVING max(t.god_prepod) < 10",
        cdb.getConnection());
      DataSet dataSet = new DataSet();
      adapter.Fill(dataSet);
      dataGridView1.DataSource = dataSet.Tables[0];
    }

    private void button9_Click(object sender, EventArgs e)
    {
      cdb.openConnection();
      SqlDataAdapter adapter = new SqlDataAdapter(
        "SELECT last_name AS 'Фамилия', first_name AS 'Имя', patronymic AS 'Отчество', email AS 'Почта' FROM teacher WHERE cafedra_id = 4 AND god_prepod > ANY(SELECT god_prepod FROM teacher WHERE cafedra_id = 6)",
        cdb.getConnection());
      DataSet dataSet = new DataSet();
      adapter.Fill(dataSet);
      dataGridView1.DataSource = dataSet.Tables[0];
    }

    private void button11_Click(object sender, EventArgs e)
    {
      using (SqlCommand command = new SqlCommand("select DISTINCT * from Scalar_Funk(@scalfunk)", cdb.getConnection()))
      {
        command.Parameters.Add(new SqlParameter("@scalfunk", SqlDbType.VarChar, 100));
        command.Parameters["@scalfunk"].Value = textBox1.Text;
        try
        {
          SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
          cdb.openConnection();
          command.ExecuteScalar();
          DataSet dataSet = new DataSet();
          dataAdapter.Fill(dataSet);
          dataGridView1.DataSource = dataSet.Tables[0];
        }
        catch (Exception)
        {
          throw;
        }
      }
    }

    private void button12_Click(object sender, EventArgs e)
    {
      using (SqlCommand command = new SqlCommand("select DISTINCT * from VekFunk(@god_prepod)", cdb.getConnection()))
      {
        command.Parameters.Add(new SqlParameter("@god_prepod", SqlDbType.VarChar, 100));
        command.Parameters["@god_prepod"].Value = textBox2.Text;
        try
        {
          SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
          cdb.openConnection();
          command.ExecuteScalar();
          DataSet dataSet = new DataSet();
          dataAdapter.Fill(dataSet);
          dataGridView1.DataSource = dataSet.Tables[0];
        }
        catch (Exception)
        {
          throw;
        }
      }
    }

    private void button10_Click(object sender, EventArgs e)
    {
      VIEW view = new VIEW();
      view.ShowDialog();
      this.Show();
    }
  }
}
