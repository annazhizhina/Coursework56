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
  enum RowState
  {
    Existed,
    New,
    Modified,
    ModifiedNew,
    Deleted
  }
  public partial class PrepodForm : Form
  {

    ConectDB cdb = new ConectDB();
    int selectedRow;

    public PrepodForm()
    {
      InitializeComponent();
      //this.Size = new Size(1730,780);
    }
    private void CreatedColumns()//
    {
      dataGridView1.Columns.Add("ID_predmet", "ID_predmet");//0 int
      dataGridView1.Columns.Add("predmet_name", "Название предмета");//string
      dataGridView1.Columns.Add("ID_cafedra", "ID_cafedra");//int
      dataGridView1.Columns.Add("cafedra_name", "Название кафедры");//string
      dataGridView1.Columns.Add("predmet_name_id", "predmet_name_id");//int
      dataGridView1.Columns.Add("ID_teacher", "ID_teacher");//5 int
      dataGridView1.Columns.Add("last_name", "Фамилия");//string
      dataGridView1.Columns.Add("first_name", "Имя");//string
      dataGridView1.Columns.Add("patronymic", "Отчество");//string
      dataGridView1.Columns.Add("god_prepod ", "Преподавательский стаж");//int
      dataGridView1.Columns.Add("email", "Email");//10 string
      dataGridView1.Columns.Add("cafedra_id ", "cafedra_id");//int
      dataGridView1.Columns.Add("ID_groupp", "ID_groupp");//int
      dataGridView1.Columns.Add("groupp_name", "Название группы");//string
      dataGridView1.Columns.Add("ID_control_form", "ID_control_form");//int
      dataGridView1.Columns.Add("homework", "Домашняя работа");//15 bool
      dataGridView1.Columns.Add("control_work", "Контрольная работа");//bool
      dataGridView1.Columns.Add("zachet", "Зачет");//bool
      dataGridView1.Columns.Add("exam", "Экзамен");//bool
      dataGridView1.Columns.Add("ID_audit_nagruz", "ID_audit_nagruz");//int
      dataGridView1.Columns.Add("time_kolvo ", "Аудиторная нагрузка(в часах)");//20 int
      dataGridView1.Columns.Add("control_form_id", "control_form_id");//int
      dataGridView1.Columns.Add("ID_study_process", "ID_study_process");//int
      dataGridView1.Columns.Add("teacher_id", "teacher_id");//int
      dataGridView1.Columns.Add("groupp_id", "groupp_id");//int
      dataGridView1.Columns.Add("audit_nagruz_id", "audit_nagruz_id");//25 int
      dataGridView1.Columns.Add("IsNew", String.Empty);
      dataGridView1.Columns[0].Visible = false;
      dataGridView1.Columns[2].Visible = false;
      dataGridView1.Columns[4].Visible = false;
      dataGridView1.Columns[5].Visible = false;
      dataGridView1.Columns[11].Visible = false;
      dataGridView1.Columns[12].Visible = false;
      dataGridView1.Columns[14].Visible = false;
      dataGridView1.Columns[19].Visible = false;
      dataGridView1.Columns[21].Visible = false;
      dataGridView1.Columns[22].Visible = false;
      dataGridView1.Columns[23].Visible = false;
      dataGridView1.Columns[24].Visible = false;
      dataGridView1.Columns[25].Visible = false;
    }
    public class ComboboxItem
    {
      public string Text { get; set; }
      public string Name { get; set; }
      public object Value { get; set; }
      public override string ToString()
      {
        return Text;
      }
    }

    private void ReadSingleRow(DataGridView dgw, IDataRecord record)
    {
      dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetString(3), record.GetInt32(4), record.GetInt32(5), record.GetString(6), record.GetString(7),record.GetString(8), record.GetInt32(9), record.GetString(10), record.GetInt32(11), record.GetInt32(12), record.GetString(13), record.GetInt32(14), record.GetBoolean(15),record.GetBoolean(16), record.GetBoolean(17), record.GetBoolean(18), record.GetInt32(19), record.GetInt32(20), record.GetInt32(21), record.GetInt32(22),record.GetInt32(23), record.GetInt32(24), record.GetInt32(25), RowState.ModifiedNew);
    }

    private void RefreshDataGrid(DataGridView dgw)
    {
      dgw.Rows.Clear();
      string queryString = $"select pr.ID_predmet,pr.predmet_name,c.ID_cafedra,c.cafedra_name,c.predmet_name_id,t.ID_teacher,t.last_name,t.first_name,t.patronymic,t.god_prepod,t.email,t.cafedra_id, g.ID_groupp,g.groupp_name,cf.ID_control_form,cf.homework,cf.control_work,cf.zachet,cf.exam,an.ID_audit_nagruz,an.time_kolvo,an.control_form_id,sp.ID_study_process,sp.teacher_id,sp.groupp_id,sp.audit_nagruz_id from predmet as pr,cafedra as c,teacher as t,groupp as g,control_form as cf,audit_nagruz as an,study_process as sp where pr.ID_predmet = c.predmet_name_id and c.ID_cafedra = t.cafedra_id and t.ID_teacher = sp.teacher_id and g.ID_groupp = sp.groupp_id and cf.ID_control_form = an.control_form_id and an.ID_audit_nagruz = sp.audit_nagruz_id ";

      SqlCommand command = new SqlCommand(queryString, cdb.getConnection());

      cdb.openConnection();

      SqlDataReader reader = command.ExecuteReader();

      while (reader.Read())
      {
        ReadSingleRow(dgw, reader);
      }
      reader.Close();
    }

    private void PrepodForm_Load(object sender, EventArgs e)
    {
      CreatedColumns();
      RefreshDataGrid(dataGridView1);
      LoadPredmet();
      LoadCafedra();
      LoadPrepod();
      LoadGroup();
      LoadAuditor();
      LoadChas();
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      selectedRow = e.RowIndex;
      if (e.RowIndex >= 0)
      {

        DataGridViewRow row = dataGridView1.Rows[selectedRow];
        textBox_pr_id.Text = row.Cells[0].Value.ToString();
        textBox_pred.Text = row.Cells[1].Value.ToString();
        textBox_kaf_id.Text = row.Cells[2].Value.ToString();
        textBox_kaf.Text = row.Cells[3].Value.ToString();
        comboBox_Pr.Text = row.Cells[1].Value.ToString();
        textBox_prep_id.Text = row.Cells[5].Value.ToString();
        textBox_pr_last.Text = row.Cells[6].Value.ToString();
        textBox_pr_name.Text = row.Cells[7].Value.ToString();
        textBox_pr_pat.Text = row.Cells[8].Value.ToString();
        textBox_time_stag.Text = row.Cells[9].Value.ToString();
        textBox_email.Text = row.Cells[10].Value.ToString();
        comboBox_kaf.Text = row.Cells[3].Value.ToString();
        textBox_gr_id.Text = row.Cells[12].Value.ToString();
        textBox_group.Text = row.Cells[13].Value.ToString();
        textBox_formcont_id.Text = row.Cells[14].Value.ToString();
        if ((bool)dataGridView1.Rows[e.RowIndex].Cells[15].Value == true)
        {
          checkBox1.Checked = true;
        }
        else
        {
          checkBox1.Checked = false;
        }

        if ((bool)dataGridView1.Rows[e.RowIndex].Cells[16].Value == true)
        {
          checkBox2.Checked = true;
        }
        else
        {
          checkBox2.Checked = false;
        }

        if ((bool)dataGridView1.Rows[e.RowIndex].Cells[17].Value == true)
        {
          checkBox3.Checked = true;
        }
        else
        {
          checkBox3.Checked = false;
        }

        if ((bool)dataGridView1.Rows[e.RowIndex].Cells[18].Value == true)
        {
          checkBox4.Checked = true;
        }
        else
        {
          checkBox4.Checked = false;
        }

        textBox_audit_nagr_id.Text = row.Cells[19].Value.ToString();
        textBox_ay.Text = row.Cells[20].Value.ToString();
        comboBox_kaf.Text = row.Cells[3].Value.ToString();
        textBox_yp_id.Text = row.Cells[22].Value.ToString();
        comboBox_fam.Text = row.Cells[6].Value.ToString();
        comboBox_gr.Text = row.Cells[13].Value.ToString();
        comboBox_chas.Text = row.Cells[20].Value.ToString();
      }
    }

    private void LoadPredmet()
    {
      string MyString = "select DISTINCT predmet_name from predmet";
      cdb.openConnection();
      SqlCommand cmd = new SqlCommand(MyString, cdb.getConnection());
      cmd.CommandType = CommandType.Text;
      DataTable table = new DataTable();
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      adapter.Fill(table);
      comboBox_Pr.DisplayMember = "predmet_name";
      comboBox_Pr.ValueMember = "ID_predmet";
      comboBox_Pr.DataSource = table;
    }

    private void LoadCafedra()
    {
      string MyString = "select DISTINCT cafedra_name from cafedra";
      cdb.openConnection();
      SqlCommand cmd = new SqlCommand(MyString, cdb.getConnection());
      cmd.CommandType = CommandType.Text;
      DataTable table = new DataTable();
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      adapter.Fill(table);
      comboBox_kaf.DisplayMember = "cafedra_name";
      comboBox_kaf.ValueMember = "ID_cafedra";
      comboBox_kaf.DataSource = table;
    }

    private void LoadAuditor()
    {
      SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-GP9Q6VJG\SQLEXPRESS;Initial Catalog=UNIVERSITY;Integrated Security=True");
      con.Open();
      SqlCommand cmd = new SqlCommand("select cf.homework,cf.control_work,cf.zachet,cf.exam,an.control_form_id from control_form as cf,audit_nagruz as an where cf.ID_control_form = an.control_form_id", con);
      SqlDataReader reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        string value = "|Домашняя работа: "+ reader.GetBoolean(0) + "|" + " Контрольная работа: " + reader.GetBoolean(1) + "|" + " Зачет: " + reader.GetBoolean(2) + "|" + " Экзамен: " + reader.GetBoolean(3) + "|" /*+ " - " + reader.GetInt32(4)*/;
        ComboboxItem item = new ComboboxItem();
        item.Text = value;
        item.Name = reader.GetBoolean(0).ToString();
        item.Value = reader.GetInt32(4).ToString();
        comboBox_fk.Items.Add(item);
      }
      reader.Close();
    }

    private void LoadPrepod()
    {
      string MyString = "select DISTINCT last_name from teacher";
      cdb.openConnection();
      SqlCommand cmd = new SqlCommand(MyString, cdb.getConnection());
      cmd.CommandType = CommandType.Text;
      DataTable table = new DataTable();
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      adapter.Fill(table);
      comboBox_fam.DisplayMember = "last_name";
      comboBox_fam.ValueMember = "ID_teacher";
      comboBox_fam.DataSource = table;
    }

    private void LoadGroup()
    {
      string MyString = "select DISTINCT groupp_name from groupp";
      cdb.openConnection();
      SqlCommand cmd = new SqlCommand(MyString, cdb.getConnection());
      cmd.CommandType = CommandType.Text;
      DataTable table = new DataTable();
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      adapter.Fill(table);
      comboBox_gr.DisplayMember = "groupp_name";
      comboBox_gr.ValueMember = "ID_groupp";
      comboBox_gr.DataSource = table;
    }

    private void LoadChas()
    {
      string MyString = "select DISTINCT time_kolvo from audit_nagruz";
      cdb.openConnection();
      SqlCommand cmd = new SqlCommand(MyString, cdb.getConnection());
      cmd.CommandType = CommandType.Text;
      DataTable table = new DataTable();
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      adapter.Fill(table);
      comboBox_chas.DisplayMember = "time_kolvo";
      comboBox_chas.ValueMember = "ID_audit_nagruz";
      comboBox_chas.DataSource = table;
    }

    private void btnAddPr_Click(object sender, EventArgs e)
    {
      var pred = textBox_pred.Text;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Insertpredmet '{pred}'"));
    }

    private void btnUPPr_Click(object sender, EventArgs e)
    {
      var id = textBox_pr_id.Text;
      var pred = textBox_pred.Text;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Updatepredmet '{id}','{pred}'"));
    }

    private void btnDelPr_Click(object sender, EventArgs e)
    {
      var id = textBox_pr_id.Text;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Deletepredmet '{id}'"));
    }

    private void btnAddKaf_Click(object sender, EventArgs e)
    {
      var kaf = textBox_kaf.Text;
      var cpr = comboBox_Pr.Text;
      SqlCommand sqlCommand = new SqlCommand(
      $"SELECT ID_predmet FROM predmet WHERE predmet_name='{cpr}'", cdb.getConnection());
      int result = (int)sqlCommand.ExecuteScalar();
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Insertcafedra '{kaf}','{result}'"));
    }

    private void btnUpKaf_Click(object sender, EventArgs e)
    {
      var id = textBox_kaf_id.Text;
      var kaf = textBox_kaf.Text;
      var cpr = comboBox_Pr.Text;
      SqlCommand sqlCommand = new SqlCommand(
      $"SELECT ID_predmet FROM predmet WHERE predmet_name='{cpr}'", cdb.getConnection());
      int result = (int)sqlCommand.ExecuteScalar();
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Updatecafedra '{id}','{kaf}','{result}'"));
    }

    private void btnDelKaf_Click(object sender, EventArgs e)
    {
      var id = textBox_kaf_id.Text;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Deletecafedra '{id}'"));
    }

    private void btnAddPrep_Click(object sender, EventArgs e)
    {
      var last = textBox_pr_last.Text;
      var name = textBox_pr_name.Text;
      var pat = textBox_pr_pat.Text;
      var stag = textBox_time_stag.Text;
      var email = textBox_email.Text;
      var kaf = comboBox_kaf.Text;
      SqlCommand sqlCommand = new SqlCommand(
      $"SELECT ID_cafedra FROM cafedra WHERE cafedra_name='{kaf}'", cdb.getConnection());
      int result = (int)sqlCommand.ExecuteScalar();
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Insertteacher '{last}','{name}','{pat}','{stag}','{email}','{result}'"));
    }

    private void btnUpPrep_Click(object sender, EventArgs e)
    {
      var id = textBox_prep_id.Text;
      var last = textBox_pr_last.Text;
      var name = textBox_pr_name.Text;
      var pat = textBox_pr_pat.Text;
      var stag = textBox_time_stag.Text;
      var email = textBox_email.Text;
      var kaf = comboBox_kaf.Text;
      SqlCommand sqlCommand = new SqlCommand(
      $"SELECT ID_cafedra FROM cafedra WHERE cafedra_name='{kaf}'", cdb.getConnection());
      int result = (int)sqlCommand.ExecuteScalar();
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Updateteacher '{id}','{last}','{name}','{pat}','{stag}','{email}','{result}'"));
    }

    private void btnDelPrep_Click(object sender, EventArgs e)
    {
      var id = textBox_prep_id.Text;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Deleteteacher '{id}'"));
    }

    private void btnAddGR_Click(object sender, EventArgs e)
    {
      var gro = textBox_group.Text;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Insertgroupp '{gro}'"));
    }

    private void btnUpGR_Click(object sender, EventArgs e)
    {
      var id = textBox_gr_id.Text;
      var gro = textBox_group.Text;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Updategroupp '{id}','{gro}'"));
    }

    private void btnDelGR_Click(object sender, EventArgs e)
    {
      var id = textBox_gr_id.Text;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Deletegroupp '{id}'"));
    }

    private void btnAddFK_Click(object sender, EventArgs e)
    {
      bool cBox1 = checkBox1.Checked;
      bool cBox2 = checkBox2.Checked;
      bool cBox3 = checkBox3.Checked;
      bool cBox4 = checkBox4.Checked;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Insertcontrol_form '{cBox1}','{cBox2}','{cBox3}','{cBox4}'"));
    }

    private void btnUpFK_Click(object sender, EventArgs e)
    {
      var id = textBox_formcont_id.Text;
      bool cBox1 = checkBox1.Checked;
      bool cBox2 = checkBox2.Checked;
      bool cBox3 = checkBox3.Checked;
      bool cBox4 = checkBox4.Checked;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Updatecontrol_form '{id}','{cBox1}','{cBox2}','{cBox3}','{cBox4}'"));
    }

    private void btnDelFK_Click(object sender, EventArgs e)
    {
      var id = textBox_formcont_id.Text;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Deletecontrol_form '{id}'"));
    }

    private void btnAddAN_Click(object sender, EventArgs e)
    {
      var ay = textBox_ay.Text;
      var fk = (comboBox_fk.SelectedItem as ComboboxItem).Name;
      SqlCommand sqlCommand = new SqlCommand(
      $"SELECT ID_control_form FROM control_form as cf, audit_nagruz as an WHERE cf.homework = '{fk}' and cf.ID_control_form = an.control_form_id ", cdb.getConnection());
      int result = (int)sqlCommand.ExecuteScalar();
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Insertaudit_nagruz '{ay}','{result}'"));
    }

    private void btnUpAN_Click(object sender, EventArgs e)
    {
      var id = textBox_audit_nagr_id.Text;
      var ay = textBox_ay.Text;
      var fk = (comboBox_fk.SelectedItem as ComboboxItem).Name;
      SqlCommand sqlCommand = new SqlCommand(
      $"SELECT ID_control_form FROM control_form as cf, audit_nagruz as an WHERE cf.homework = '{fk}' and cf.ID_control_form = an.control_form_id ", cdb.getConnection());
      int result = (int)sqlCommand.ExecuteScalar();
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Updateaudit_nagruz '{id}','{ay}','{result}'"));
    }

    private void btnDelAN_Click(object sender, EventArgs e)
    {
      var id = textBox_audit_nagr_id.Text;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Deleteaudit_nagruz '{id}'"));
    }

    private void btnAddYP_Click(object sender, EventArgs e)
    {
      var cfam = comboBox_fam.Text;
      var cgr = comboBox_gr.Text;
      var cchas = comboBox_chas.Text;
      SqlCommand sqlCommand1 = new SqlCommand(
      $"SELECT ID_teacher FROM teacher WHERE last_name='{cfam}'", cdb.getConnection());
      int result1 = (int)sqlCommand1.ExecuteScalar();
      SqlCommand sqlCommand2 = new SqlCommand(
      $"SELECT ID_groupp FROM groupp WHERE groupp_name='{cgr}'", cdb.getConnection());
      int result2 = (int)sqlCommand2.ExecuteScalar();
      SqlCommand sqlCommand3 = new SqlCommand(
      $"SELECT ID_audit_nagruz FROM audit_nagruz WHERE time_kolvo='{cchas}'", cdb.getConnection());
      int result3 = (int)sqlCommand3.ExecuteScalar();
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Insertstudy_process '{result1}','{result2}','{result3}'"));
    }

    private void btnUpYP_Click(object sender, EventArgs e)
    {
      var id = textBox_yp_id.Text;
      var cfam = comboBox_fam.Text;
      var cgr = comboBox_gr.Text;
      var cchas = comboBox_chas.Text;
      SqlCommand sqlCommand1 = new SqlCommand(
      $"SELECT ID_teacher FROM teacher WHERE last_name='{cfam}'", cdb.getConnection());
      int result1 = (int)sqlCommand1.ExecuteScalar();
      SqlCommand sqlCommand2 = new SqlCommand(
      $"SELECT ID_groupp FROM groupp WHERE groupp_name='{cgr}'", cdb.getConnection());
      int result2 = (int)sqlCommand2.ExecuteScalar();
      SqlCommand sqlCommand3 = new SqlCommand(
      $"SELECT ID_audit_nagruz FROM audit_nagruz WHERE time_kolvo='{cchas}'", cdb.getConnection());
      int result3 = (int)sqlCommand3.ExecuteScalar();
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Updatestudy_process '{id}','{result1}','{result2}','{result3}'"));
    }

    private void btnDelYP_Click(object sender, EventArgs e)
    {
      var id = textBox_yp_id.Text;
      MessageBox.Show(DataHandler.ExecuteProc($"EXEC Deletestudy_process '{id}'"));
    }

    private void button22_Click(object sender, EventArgs e)
    {
      Request request = new Request();
      request.ShowDialog();
      this.Show();
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
      RefreshDataGrid(dataGridView1);
    }
  }
}
