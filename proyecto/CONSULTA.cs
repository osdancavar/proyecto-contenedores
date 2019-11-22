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
    public partial class CONSULTA : Form
    {
        private string usuario;

        public CONSULTA(string user)
        {
            InitializeComponent();
            this.usuario = user;
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            dataGridView3.ReadOnly = true;
            dataGridView4.ReadOnly = true;
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


        public string[,] Informacion()
        {
            ////////////////////////////////////////////////////////////////////
            char[] line = new char[500];
            List<string> buffer = new List<string>();
            string[,] informacion = new string[0, 8];
            Array.Clear(informacion, 0, informacion.Length);
            string linea;
            int contador = 0, alto = 0, ancho = 0, fila_info = 1;
            bool banderita = false;

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\DATA\\SubFolder\\info_contenedor.txt");
            while ((linea = file.ReadLine()) != null)
            {
                Array.Clear(line, 0, line.Length);
                line = linea.ToCharArray();

                if (banderita == false)
                {
                    cambiar_tamaño(ref informacion, fila_info, 8);
                    fila_info++;
                }
                else
                {
                    banderita = false;
                }

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '|')
                    {
                        linea = String.Join(String.Empty, buffer.ToArray());
                        buffer.Clear();
                        contador = 0;

                        for (int a = 0; a < informacion.GetLength(0); a++)
                        {
                            if (informacion[a, 0] == linea)
                            {
                                MessageBox.Show("id " + linea + " aparece mas de una vez en el formulario, has sido agregada unicamente la primera vez que aparece", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                banderita = true;
                                break;
                            }
                        }

                        if (banderita == false)
                        {
                            informacion[alto, ancho] = linea;
                            ancho++;
                        }

                    }
                    else if (ancho == 8)
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
                if (banderita == false)
                {
                    alto++;
                }
            }

            file.Close();
            return (informacion);
        }

        public string[,] peso_doc()
        {
            ////////////////////////////////////////////////////////////////////
            char[] line = new char[500];
            List<string> buffer = new List<string>();
            string[,] peso = new string[0, 7];
            Array.Clear(peso, 0, peso.Length);
            string linea;
            int contador = 0, alto = 0, ancho = 0, fila_info = 1;
            bool banderita = false;

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\DATA\\SubFolder\\peso.txt");
            while ((linea = file.ReadLine()) != null)
            {
                Array.Clear(line, 0, line.Length);
                line = linea.ToCharArray();

                if (banderita == false)
                {
                    cambiar_tamaño(ref peso, fila_info, 7);
                    fila_info++;
                }
                else
                {
                    banderita = false;
                }

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '|')
                    {
                        linea = String.Join(String.Empty, buffer.ToArray());
                        buffer.Clear();
                        contador = 0;

                        for (int a = 0; a < peso.GetLength(0); a++)
                        {
                            if (peso[a, 0] == linea)
                            {
                                MessageBox.Show("id " + linea + " aparece mas de una vez en el formulario, has sido agregada unicamente la primera vez que aparece", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                banderita = true;
                                break;
                            }
                        }

                        if (banderita == false)
                        {
                            peso[alto, ancho] = linea;
                            ancho++;
                        }

                    }
                    else if (ancho == 7)
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
                if (banderita == false)
                {
                    alto++;
                }
            }

            file.Close();
            return (peso);
        }
        public string[,] patio_doc()
        {
            ////////////////////////////////////////////////////////////////////
            char[] line = new char[500];
            List<string> buffer = new List<string>();
            string[,] peso = new string[0, 13];
            Array.Clear(peso, 0, peso.Length);
            string linea;
            int contador = 0, alto = 0, ancho = 0, fila_info = 1;
            bool banderita = false;

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\DATA\\SubFolder\\patio.txt");
            while ((linea = file.ReadLine()) != null)
            {
                Array.Clear(line, 0, line.Length);
                line = linea.ToCharArray();

                if (banderita == false)
                {
                    cambiar_tamaño(ref peso, fila_info, 13);
                    fila_info++;
                }
                else
                {
                    banderita = false;
                }

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '|')
                    {
                        linea = String.Join(String.Empty, buffer.ToArray());
                        buffer.Clear();
                        contador = 0;

                        for (int a = 0; a < peso.GetLength(0); a++)
                        {
                            if (peso[a, 0] == linea)
                            {
                                MessageBox.Show("id " + linea + " aparece mas de una vez en el formulario, has sido agregada unicamente la primera vez que aparece", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                banderita = true;
                                break;
                            }
                        }

                        if (banderita == false)
                        {
                            peso[alto, ancho] = linea;
                            ancho++;
                        }

                    }
                    else if (ancho == 13)
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
                if (banderita == false)
                {
                    alto++;
                }
            }

            file.Close();
            return (peso);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea regresar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                using (menu lanzar = new menu(this.usuario))
                {
                    lanzar.StartPosition = FormStartPosition.CenterScreen;
                    this.Hide();
                    lanzar.ShowDialog();
                    this.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text != "")
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dataGridView2.Rows.Clear();
                dataGridView2.Refresh();
                dataGridView3.Rows.Clear();
                dataGridView3.Refresh();
                dataGridView4.Rows.Clear();
                dataGridView4.Refresh();

                string[,] informacion = new string[0, 8];
                informacion = Informacion();

                string[,] bascula = new string[0, 7];
                bascula = peso_doc();

                string[,] patio = new string[0, 13];
                patio = patio_doc();

                bool banderita = false;
                for(int i=0; i< informacion.GetLength(0); i++)
                {
                    if (maskedTextBox1.Text == informacion[i, 0])
                    {
                      
                        dataGridView1.Rows.Add(informacion[i, 0], informacion[i, 1],
                                       informacion[i, 2], informacion[i, 3],
                                       informacion[i, 4], informacion[i, 5],
                                       informacion[i, 6], informacion[i, 7]);
                        banderita = true;
                        break;
                    }
                }

                if (banderita != true)
                {
                    MessageBox.Show("CONTENEDOR NO ENCONTRADO", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                banderita = false;

                for (int j = 0; j < bascula.GetLength(0); j++)
                {
                    if (maskedTextBox1.Text == bascula[j, 0])
                    {
                        dataGridView2.Rows.Add( bascula[j, 1],bascula[j, 2], bascula[j, 3], 
                            bascula[j, 4], bascula[j, 5], bascula[j, 6]);
                        banderita = true;
                        break;
                    }
                }

                if (banderita != true)
                {
                    MessageBox.Show("INFORMACION SOBRE EL PESO NO ENCONTRADA", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                banderita = false;

                banderita = false;

                for (int a = 0; a < patio.GetLength(0); a++)
                {
                    if (maskedTextBox1.Text == patio[a, 0])
                    {
                        dataGridView3.Rows.Add(patio[a, 0], patio[a, 1],
                                      patio[a, 2], patio[a, 3], patio[a, 4], patio[a, 5],
                                      patio[a, 6]);
                        dataGridView4.Rows.Add(patio[a, 7], patio[a, 8],
                         patio[a, 9], patio[a, 10], patio[a, 11], patio[a, 12]);
                        banderita = true;
                        break;
                    }
                }

                if (banderita != true)
                {
                    MessageBox.Show("INFORMACION SOBRE EL PATIO NO ENCONTRADA", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                banderita = false;

            }
            else
            {
                MessageBox.Show("INGRESE ID A BUSCAR", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
