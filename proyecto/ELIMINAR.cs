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
    public partial class ELIMINAR : Form
    {
        private string usuario;
        public ELIMINAR(String user)
        {
            InitializeComponent();
            this.usuario = user;
            dataGridView1.ReadOnly = true;
            imprimir();
        }

        private void imprimir()
        {
            string[,] informacion = new string[0, 2];
            informacion = contraseñaCalc();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (informacion.GetLength(0) == 0)
            {
                dataGridView1.Rows.Add("vacio", "vacio");
            }
            else
            {
                for (int i = 0; i < informacion.GetLength(0); i++)
                {

                    dataGridView1.Rows.Add(informacion[i, 0], informacion[i, 1]);

                }
            }
        }


        private void cambiar_tamaño(ref string[,] original, int filas, int col)
        {
            //create a new 2 dimensional array with
            //the size we want
            string[,] newArray = new string[filas, col];
            //copy the contents of the old array to the new one
            Array.Copy(original, newArray, original.Length);
            //set the original to the new array
            original = newArray;
        }


        public void guardar(string[,] informacion)
        {
            try
            {//Pass the filepath and filename to the StreamWriter 
                System.IO.StreamWriter sw = new System.IO.StreamWriter("c:\\DATA\\SubFolder\\Contraseña.txt");
                //Write a line of text
                for (int i = 0; i < informacion.GetLength(0); i++)
                {
                    if (informacion[i, 0] != "")
                    {
                        sw.WriteLine(informacion[i, 0] + "|" + informacion[i, 1] + "|");
                    }

                }

                //Close the file
                sw.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al guardar la info en el archivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public string[,] contraseñaCalc()
        {

            ////////////////////////////////////////////////////////////////////
            char[] line = new char[100];
            List<string> buffer = new List<string>();
            string[,] contraseñas = new string[0, 2];
            Array.Clear(contraseñas, 0, contraseñas.Length);
            string linea;
            int contador = 0, alto = 0, ancho = 0, fila_info = 1;

            System.IO.StreamReader file = new System.IO.StreamReader(@"c:\DATA\SubFolder\Contraseña.txt");

            while ((linea = file.ReadLine()) != null)
            {
                Array.Clear(line, 0, line.Length);
                line = linea.ToCharArray();

                cambiar_tamaño(ref contraseñas, fila_info, 2);
                fila_info++;

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '|')
                    {
                        linea = String.Join(String.Empty, buffer.ToArray());
                        buffer.Clear();
                        contador = 0;
                        contraseñas[alto, ancho] = linea;
                        ancho++;

                    }
                    else if (ancho == 2)
                    {
                        break;
                    }
                    else
                    {
                        buffer.Add("" + line[i] + "");
                        contador++;
                    }
                }
                ancho = 0;
                alto++;
            }
            file.Close();
            return (contraseñas);

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void REGRESAR_Click(object sender, EventArgs e)
        {
            using (AÑADIR lanzar = new AÑADIR(this.usuario))
            {
                lanzar.StartPosition = FormStartPosition.CenterScreen;
                this.Hide();
                lanzar.ShowDialog();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[,] contraseñas = new string[0, 2];
            contraseñas = contraseñaCalc();
            bool pase = false;


            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("usuario vacio, intente nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@"c:\DATA\SubFolder\Contraseña.txt");
                if (file.ReadLine() == null)
                {
                    file.Close();
                    pase = true;
                }
                else
                {
                    file.Close();
                    for (int i = 0; i < contraseñas.GetLength(0); i++)
                    {
                        if (textBox1.Text == contraseñas[i, 0])
                        {
                            if (MessageBox.Show("¿desea eliminar el usuario "+ textBox1.Text + " ?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                            {
                                MessageBox.Show("usuario eliminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                contraseñas[i, 0] = "";
                                contraseñas[i, 1] = "";
                                textBox1.Text = "";
                                guardar(contraseñas);
                                imprimir();
                            }
                            pase = false;
                            break;
                        }
                        else
                        {
                            pase = true;
                        }
                    }
                }               
            }

            if (pase == true)
            {
                MessageBox.Show("usuario  no encontrado o ya no hay nada que eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
            }
        }
    }
}
