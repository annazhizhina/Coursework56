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
  public partial class StudUser : Form
  {
    ConectDB cdb = new ConectDB();
    private string gr_num;
    private SqlConnection getConnection;
    public StudUser(SqlConnection con, string g)
    {
      InitializeComponent();
      getConnection = con;
      gr_num = g;
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

    private void ReadSingleRow(DataGridView dgw, IDataRecord record)
    {
      dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetString(3), record.GetInt32(4), record.GetInt32(5), record.GetString(6), record.GetString(7), record.GetString(8), record.GetInt32(9), record.GetString(10), record.GetInt32(11), record.GetInt32(12), record.GetString(13), record.GetInt32(14), record.GetBoolean(15), record.GetBoolean(16), record.GetBoolean(17), record.GetBoolean(18), record.GetInt32(19), record.GetInt32(20), record.GetInt32(21), record.GetInt32(22), record.GetInt32(23), record.GetInt32(24), record.GetInt32(25), RowState.ModifiedNew);
    }

    private void RefreshDataGrid(DataGridView dgw)
    {
      dgw.Rows.Clear();
      string queryString = $"select pr.ID_predmet,pr.predmet_name,c.ID_cafedra,c.cafedra_name,c.predmet_name_id,t.ID_teacher,t.last_name,t.first_name,t.patronymic,t.god_prepod,t.email,t.cafedra_id, g.ID_groupp,g.groupp_name,cf.ID_control_form,cf.homework,cf.control_work,cf.zachet,cf.exam,an.ID_audit_nagruz,an.time_kolvo,an.control_form_id,sp.ID_study_process,sp.teacher_id,sp.groupp_id,sp.audit_nagruz_id from predmet as pr,cafedra as c,teacher as t,groupp as g,control_form as cf,audit_nagruz as an,study_process as sp where pr.ID_predmet = c.predmet_name_id and c.ID_cafedra = t.cafedra_id and t.ID_teacher = sp.teacher_id and g.ID_groupp = sp.groupp_id and cf.ID_control_form = an.control_form_id and an.ID_audit_nagruz = sp.audit_nagruz_id and g.groupp_name = '{gr_num}'";

      SqlCommand command = new SqlCommand(queryString, cdb.getConnection());

      cdb.openConnection();

      SqlDataReader reader = command.ExecuteReader();

      while (reader.Read())
      {
        ReadSingleRow(dgw, reader);
      }
      reader.Close();
    }

    private void StudUser_Load(object sender, EventArgs e)
    {
      CreatedColumns();
      RefreshDataGrid(dataGridView1);
    }
  }
}
