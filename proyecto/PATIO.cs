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
    public partial class PATIO : Form
    {
        private string usuario;
        public PATIO(string user)
        {
            InitializeComponent();
            this.usuario = user;
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
        }

        private void PATIO_Load(object sender, EventArgs e)
        {

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

        public void guardar(string[,] informacion)
        {
            try
            {//Pass the filepath and filename to the StreamWriter 
                System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\DATA\\SubFolder\\patio.txt");
                //Write a line of text
                for (int i = 0; i < informacion.GetLength(0); i++)
                {
                    if (informacion[i, 0] != "")
                    {
                        sw.WriteLine(informacion[i, 0] + "|" + informacion[i, 1] + "|" +
                                     informacion[i, 2] + "|" + informacion[i, 3] + "|" +
                                     informacion[i, 4] + "|" + informacion[i, 5] + "|" +
                                     informacion[i, 6] + "|" + informacion[i, 7] + "|" +
                                     informacion[i, 8] + "|" + informacion[i, 9] + "|" +
                                     informacion[i, 10] + "|" + informacion[i, 11] + "|" +
                                     informacion[i, 12] + "|");
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


        string contenedor = "";
        private void button3_Click(object sender, EventArgs e)
        {


            string[,] informacion = new string[0, 8];
            informacion = Informacion();

            string[,] patio = new string[0, 13];
            patio = patio_doc();

            bool banderita = true, banderita1 = false;

            if (informacion.GetLength(0) == 0)
            {
                MessageBox.Show("BASE DE DATOS VACÍA, INGRESE CONTENEDORES", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < informacion.GetLength(0); i++)
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(@"c:\DATA\SubFolder\patio.txt");
                    if (file.ReadLine() == null)
                    {
                        banderita = false;
                        file.Close();
                        dataGridView1.Rows.Clear();
                        dataGridView1.Refresh();
                        dataGridView2.Rows.Clear();
                        dataGridView2.Refresh();
                        cambiar_tamaño(ref patio, patio.GetLength(0) + 1, 13);
                        patio[patio.GetLength(0) - 1, 0] = maskedTextBox1.Text;
                        for (int a = 1; a < 13; a++)
                        {
                            patio[patio.GetLength(0) - 1, a] = "vacio";
                        }

                        dataGridView1.Rows.Add(patio[patio.GetLength(0) - 1, 0], patio[patio.GetLength(0) - 1, 1],
                             patio[patio.GetLength(0) - 1, 2], patio[patio.GetLength(0) - 1, 3], patio[patio.GetLength(0) - 1, 4],
                              patio[patio.GetLength(0) - 1, 5], patio[patio.GetLength(0) - 1, 6]);
                        dataGridView2.Rows.Add(patio[patio.GetLength(0) - 1, 7], patio[patio.GetLength(0) - 1, 8],
                             patio[patio.GetLength(0) - 1, 9], patio[patio.GetLength(0) - 1, 10], patio[patio.GetLength(0) - 1, 11],
                              patio[patio.GetLength(0) - 1, 12]);

                        this.contenedor = maskedTextBox1.Text;
                        maskedTextBox1.Text = "";
                        banderita1 = false;
                        guardar(patio);
                        break;
                    }
                    else
                    {
                        if (maskedTextBox1.Text == informacion[i, 0])
                        {
                            banderita = false;
                            for (int j = 0; j < patio.GetLength(0); j++)
                            {
                                if (maskedTextBox1.Text == patio[j, 0])
                                {
                                    banderita1 = false;
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Refresh();
                                    dataGridView2.Rows.Clear();
                                    dataGridView2.Refresh();
                                    this.contenedor = maskedTextBox1.Text;
                                    maskedTextBox1.Text = "";
                                    dataGridView1.Rows.Add(patio[j, 0], patio[j, 1],
                                     patio[j, 2], patio[j, 3], patio[j, 4], patio[j, 5],
                                     patio[j, 6]);
                                    dataGridView2.Rows.Add(patio[j, 7], patio[j, 8],
                                     patio[j, 9], patio[j, 10], patio[j, 11], patio[j, 12]);
                                    break;
                                }
                                else
                                {
                                    banderita1 = true;
                                }

                            }

                            if (banderita1 == true)
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Refresh();
                                dataGridView2.Rows.Clear();
                                dataGridView2.Refresh();
                                cambiar_tamaño(ref patio, patio.GetLength(0) + 1, 13);
                                patio[patio.GetLength(0) - 1, 0] = maskedTextBox1.Text;
                                for (int a = 1; a < 13; a++)
                                {
                                    patio[patio.GetLength(0) - 1, a] = "vacio";
                                }

                                dataGridView1.Rows.Add(patio[patio.GetLength(0) - 1, 0], patio[patio.GetLength(0) - 1, 1],
                                     patio[patio.GetLength(0) - 1, 2], patio[patio.GetLength(0) - 1, 3], patio[patio.GetLength(0) - 1, 4],
                                      patio[patio.GetLength(0) - 1, 5], patio[patio.GetLength(0) - 1, 6]);
                                dataGridView2.Rows.Add(patio[patio.GetLength(0) - 1, 7], patio[patio.GetLength(0) - 1, 8],
                                     patio[patio.GetLength(0) - 1, 9], patio[patio.GetLength(0) - 1, 10], patio[patio.GetLength(0) - 1, 11],
                                      patio[patio.GetLength(0) - 1, 12]);

                                this.contenedor = maskedTextBox1.Text;
                                maskedTextBox1.Text = "";
                                banderita1 = false;
                                guardar(patio);
                                break;
                            }

                        }
                        else if (banderita != false)
                        {
                            banderita = true;
                        }
                    }
                }
            }

            if (banderita == true)
            {
                MessageBox.Show("contenedor " + maskedTextBox1.Text + " no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBox1.Text = "";
                this.contenedor = "";
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.contenedor != "")
            {
                string[,] patio = new string[0, 13];
                patio = patio_doc();
                string combo = " ";

                if (comboBox1.SelectedItem != null)
                {
                    if (textBox2.Text != "")
                    {

                        for (int i = 0; i < patio.GetLength(0); i++)
                        {
                            combo = comboBox1.SelectedItem.ToString();
                            if (patio[i, 0] == this.contenedor)
                            {
                                if ((combo == "PATIO1") && (patio[i, 1] == "vacio")&& (radioButton1.Checked))
                                {

                                    patio[i, 1] = textBox2.Text;
                                    patio[i, 2] = this.usuario;
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Refresh();
                                    dataGridView2.Rows.Clear();
                                    dataGridView2.Refresh();
                                    dataGridView1.Rows.Add(patio[i, 0], patio[i, 1],
                                   patio[i, 2], patio[i, 3], patio[i, 4], patio[i, 5],
                                   patio[i, 6]);
                                    dataGridView2.Rows.Add(patio[i, 7], patio[i, 8],
                                     patio[i, 9], patio[i, 10], patio[i, 11], patio[i, 12]);
                                    guardar(patio);
                                    textBox2.Text = "";
                                    radioButton1.Checked = false;
                                    radioButton2.Checked = false;



                                }
                                else if((combo == "PATIO1") && (patio[i, 3] == "vacio") && (radioButton2.Checked))
                                {
                                    if (patio[i, 1] != "vacio") {
                                        patio[i, 3] = textBox2.Text;
                                        patio[i, 4] = this.usuario;
                                        dataGridView1.Rows.Clear();
                                        dataGridView1.Refresh();
                                        dataGridView2.Rows.Clear();
                                        dataGridView2.Refresh();
                                        dataGridView1.Rows.Add(patio[i, 0], patio[i, 1],
                                       patio[i, 2], patio[i, 3], patio[i, 4], patio[i, 5],
                                       patio[i, 6]);
                                        dataGridView2.Rows.Add(patio[i, 7], patio[i, 8],
                                         patio[i, 9], patio[i, 10], patio[i, 11], patio[i, 12]);
                                        guardar(patio);
                                        textBox2.Text = "";
                                        radioButton1.Checked = false;
                                        radioButton2.Checked = false;
                                    }
                                    else
                                    {
                                        MessageBox.Show("NO PUEDE INGRESAR UNA SALIDA SIN UNA ENTRADA ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                   



                                }
                                else if ((combo == "PATIO2") && (patio[i, 5] == "vacio") && (radioButton1.Checked))
                                {
                                    patio[i, 5] = textBox2.Text;
                                    patio[i, 6] = this.usuario;
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Refresh();
                                    dataGridView2.Rows.Clear();
                                    dataGridView2.Refresh();
                                    dataGridView1.Rows.Add(patio[i, 0], patio[i, 1],
                                   patio[i, 2], patio[i, 3], patio[i, 4], patio[i, 5],
                                   patio[i, 6]);
                                    dataGridView2.Rows.Add(patio[i, 7], patio[i, 8],
                                     patio[i, 9], patio[i, 10], patio[i, 11], patio[i, 12]);
                                    guardar(patio);
                                    textBox2.Text = "";
                                    radioButton1.Checked = false;
                                    radioButton2.Checked = false;



                                }
                                else if ((combo == "PATIO2") && (patio[i, 7] == "vacio") && (radioButton2.Checked))
                                {
                                    if (patio[i, 5] != "vacio")
                                    {
                                        patio[i, 7] = textBox2.Text;
                                        patio[i, 8] = this.usuario;
                                        dataGridView1.Rows.Clear();
                                        dataGridView1.Refresh();
                                               dataGridView2.Rows.Clear();
                                dataGridView2.Refresh();
                                        dataGridView1.Rows.Add(patio[i, 0], patio[i, 1],
                                       patio[i, 2], patio[i, 3], patio[i, 4], patio[i, 5],
                                       patio[i, 6]);
                                        dataGridView2.Rows.Add(patio[i, 7], patio[i, 8],
                                         patio[i, 9], patio[i, 10], patio[i, 11], patio[i, 12]);
                                        guardar(patio);
                                        textBox2.Text = "";
                                        radioButton1.Checked = false;
                                        radioButton2.Checked = false;
                                    }
                                    else
                                    {
                                        MessageBox.Show("NO PUEDE INGRESAR UNA SALIDA SIN UNA ENTRADA ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else if ((combo == "PATIO3") && (patio[i, 9] == "vacio") && (radioButton1.Checked))
                                {
                                    patio[i, 9] = textBox2.Text;
                                    patio[i, 10] = this.usuario;
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Refresh();
                                    dataGridView2.Rows.Clear();
                                    dataGridView2.Refresh();
                                    dataGridView1.Rows.Add(patio[i, 0], patio[i, 1],
                                   patio[i, 2], patio[i, 3], patio[i, 4], patio[i, 5],
                                   patio[i, 6]);
                                    dataGridView2.Rows.Add(patio[i, 7], patio[i, 8],
                                     patio[i, 9], patio[i, 10], patio[i, 11], patio[i, 12]);
                                    guardar(patio);
                                    textBox2.Text = "";
                                    radioButton1.Checked = false;
                                    radioButton2.Checked = false;



                                }
                                else if ((combo == "PATIO3") && (patio[i, 11] == "vacio") && (radioButton2.Checked))
                                {
                                    if (patio[i, 9] != "vacio")
                                    {
                                        patio[i, 11] = textBox2.Text;
                                        patio[i, 12] = this.usuario;
                                        dataGridView1.Rows.Clear();
                                        dataGridView1.Refresh();
                                        dataGridView2.Rows.Clear();
                                        dataGridView2.Refresh();
                                        dataGridView1.Rows.Add(patio[i, 0], patio[i, 1],
                                       patio[i, 2], patio[i, 3], patio[i, 4], patio[i, 5],
                                       patio[i, 6]);
                                        dataGridView2.Rows.Add(patio[i, 7], patio[i, 8],
                                         patio[i, 9], patio[i, 10], patio[i, 11], patio[i, 12]);
                                        guardar(patio);
                                        textBox2.Text = "";
                                        radioButton1.Checked = false;
                                        radioButton2.Checked = false;
                                    }
                                    else
                                    {
                                        MessageBox.Show("NO PUEDE INGRESAR UNA SALIDA SIN UNA ENTRADA ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Ya ha sido agregado una fecha a este campo, seleccione otro ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("PORFAVOR INGRESE EL patio ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                else
                {
                    MessageBox.Show("seleccione una fecha ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("seleccione un contenedor a agregar esta fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
