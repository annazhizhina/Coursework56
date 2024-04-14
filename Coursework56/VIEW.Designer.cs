
namespace Coursework56
{
  partial class VIEW
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VIEW));
      this.button_create_Symp = new System.Windows.Forms.Button();
      this.textBox_predmet = new System.Windows.Forms.TextBox();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.textBox_name_kaf = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // button_create_Symp
      // 
      this.button_create_Symp.Location = new System.Drawing.Point(314, 543);
      this.button_create_Symp.Name = "button_create_Symp";
      this.button_create_Symp.Size = new System.Drawing.Size(137, 29);
      this.button_create_Symp.TabIndex = 92;
      this.button_create_Symp.Text = "Добавить";
      this.button_create_Symp.UseVisualStyleBackColor = true;
      this.button_create_Symp.Click += new System.EventHandler(this.button_create_Symp_Click);
      // 
      // textBox_predmet
      // 
      this.textBox_predmet.Location = new System.Drawing.Point(12, 480);
      this.textBox_predmet.Name = "textBox_predmet";
      this.textBox_predmet.Size = new System.Drawing.Size(331, 22);
      this.textBox_predmet.TabIndex = 91;
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Location = new System.Drawing.Point(-1, -1);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.RowHeadersWidth = 51;
      this.dataGridView1.RowTemplate.Height = 24;
      this.dataGridView1.Size = new System.Drawing.Size(796, 448);
      this.dataGridView1.TabIndex = 87;
      // 
      // textBox_name_kaf
      // 
      this.textBox_name_kaf.Location = new System.Drawing.Point(403, 480);
      this.textBox_name_kaf.Name = "textBox_name_kaf";
      this.textBox_name_kaf.Size = new System.Drawing.Size(332, 22);
      this.textBox_name_kaf.TabIndex = 89;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(511, 460);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(136, 17);
      this.label5.TabIndex = 88;
      this.label5.Text = "Название кафедры";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(96, 460);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(140, 17);
      this.label1.TabIndex = 93;
      this.label1.Text = "Название предмета";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(741, 453);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(47, 46);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 219;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
      // 
      // VIEW
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(792, 584);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.button_create_Symp);
      this.Controls.Add(this.textBox_predmet);
      this.Controls.Add(this.textBox_name_kaf);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.dataGridView1);
      this.Name = "VIEW";
      this.Text = "VIEW";
      this.Load += new System.EventHandler(this.VIEW_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button_create_Symp;
    private System.Windows.Forms.TextBox textBox_predmet;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.TextBox textBox_name_kaf;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pictureBox1;
  }
}