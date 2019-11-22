using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class menu : Form
    {
        private string usuario;

        public menu(string user)
        {
            InitializeComponent();
            this.usuario = user;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                INICIO lanzar = new INICIO();
                lanzar.StartPosition = FormStartPosition.CenterScreen;
                this.Hide();
                lanzar.ShowDialog();
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (ingreso lanzar = new ingreso(this.usuario))
            {
                lanzar.StartPosition = FormStartPosition.CenterScreen;
                this.Hide();
                lanzar.ShowDialog();
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (PATIO lanzar = new PATIO(this.usuario))
            {
                lanzar.StartPosition = FormStartPosition.CenterScreen;
                this.Hide();
                lanzar.ShowDialog();
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
             using (PESO lanzar = new PESO(this.usuario))
            {
                lanzar.StartPosition = FormStartPosition.CenterScreen;
                this.Hide();
                lanzar.ShowDialog();
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (CONSULTA lanzar = new CONSULTA(this.usuario))
            {
                lanzar.StartPosition = FormStartPosition.CenterScreen;
                this.Hide();
                lanzar.ShowDialog();
                this.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (AÑADIR lanzar = new AÑADIR(this.usuario))
            {
                lanzar.StartPosition = FormStartPosition.CenterScreen;
                this.Hide();
                lanzar.ShowDialog();
                this.Close();
            }
        }
    }
}
