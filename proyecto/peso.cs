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
    public partial class PESO : Form
    {
        private string usuario;
       
        public PESO(string user)
        {
            InitializeComponent();
            this.usuario = user;
            dataGridView1.ReadOnly = true;
        }

        private void cambiar_tamaño(ref string[,] original, int filas,int col)
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
                    cambiar_tamaño(ref peso, fila_info,7);
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

        public void guardar(string [,] informacion)
        {
            try
            {//Pass the filepath and filename to the StreamWriter 
                System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\DATA\\SubFolder\\peso.txt");
                //Write a line of text
                for (int i = 0; i < informacion.GetLength(0); i++)
                {
                    if(informacion[i, 0] != "")
                    {
                        sw.WriteLine(informacion[i, 0] + "|" + informacion[i, 1] + "|" +
                                     informacion[i, 2] + "|" + informacion[i, 3] + "|" +
                                     informacion[i, 4] + "|" + informacion[i, 5] + "|" +
                                     informacion[i, 6] + "|");
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




        string contenedor="";
        private void button3_Click(object sender, EventArgs e)
        {

            string[,] informacion = new string[0, 8];
            informacion = Informacion();

            string[,] bascula = new string[0, 7];
            bascula = peso_doc();

            bool banderita = true,banderita1=false;

            if (informacion.GetLength(0) == 0)
            {
                MessageBox.Show("BASE DE DATOS VACÍA, INGRESE CONTENEDORES", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else
            {
                for (int i = 0; i < informacion.GetLength(0); i++)
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(@"c:\DATA\SubFolder\peso.txt");
                    if (file.ReadLine() == null)
                    {
                        banderita = false;
                        file.Close();
                        dataGridView1.Rows.Clear();
                        dataGridView1.Refresh();
                        cambiar_tamaño(ref bascula, bascula.GetLength(0) + 1, 7);
                        bascula[bascula.GetLength(0) - 1, 0] = maskedTextBox1.Text;
                        for (int a = 1; a < 7; a++)
                        {
                            bascula[bascula.GetLength(0) - 1, a] = "vacio";
                        }

                        dataGridView1.Rows.Add(bascula[bascula.GetLength(0) - 1, 0], bascula[bascula.GetLength(0) - 1, 1],
                             bascula[bascula.GetLength(0) - 1, 2], bascula[bascula.GetLength(0) - 1, 3], bascula[bascula.GetLength(0) - 1, 4],
                              bascula[bascula.GetLength(0) - 1, 5], bascula[bascula.GetLength(0) - 1, 6]);
                        this.contenedor = maskedTextBox1.Text;
                        maskedTextBox1.Text = "";
                        banderita1 = false;
                        guardar(bascula);
                        break;
                    }
                    else
                    {
                        if (maskedTextBox1.Text == informacion[i, 0])
                        {
                            banderita = false;
                            for (int j = 0; j < bascula.GetLength(0); j++)
                            {
                                if (maskedTextBox1.Text == bascula[j, 0])
                                {
                                    banderita1 = false;
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Refresh();
                                    this.contenedor = maskedTextBox1.Text;
                                    maskedTextBox1.Text = "";
                                    dataGridView1.Rows.Add(bascula[j, 0], bascula[j, 1],
                                     bascula[j, 2], bascula[j, 3], bascula[j, 4], bascula[j, 5],
                                     bascula[j, 6]);
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
                                cambiar_tamaño(ref bascula, bascula.GetLength(0) + 1, 7);
                                bascula[bascula.GetLength(0) - 1, 0] = maskedTextBox1.Text;
                                for (int a = 1; a < 7; a++)
                                {
                                    bascula[bascula.GetLength(0) - 1, a] = "vacio";
                                }

                                dataGridView1.Rows.Add(bascula[bascula.GetLength(0) - 1, 0], bascula[bascula.GetLength(0) - 1, 1],
                                     bascula[bascula.GetLength(0) - 1, 2], bascula[bascula.GetLength(0) - 1, 3], bascula[bascula.GetLength(0) - 1, 4],
                                      bascula[bascula.GetLength(0) - 1, 5], bascula[bascula.GetLength(0) - 1, 6]);
                                this.contenedor = maskedTextBox1.Text;
                                maskedTextBox1.Text = "";
                                banderita1 = false;
                                guardar(bascula);
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
                MessageBox.Show("contenedor "+ maskedTextBox1.Text + " no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBox1.Text = "";
                this.contenedor = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.contenedor != "")
            {
                string[,] bascula = new string[0, 7];
                bascula = peso_doc();
                string combo = " ";

                if (comboBox1.SelectedItem != null)
                {
                    if (textBox2.Text != "")
                    {
                     
                        for (int i = 0; i < bascula.GetLength(0); i++)
                        {
                            combo= comboBox1.SelectedItem.ToString();
                            if (bascula[i, 0] == this.contenedor)
                            {
                                if ((combo == "BASCULA1") && (bascula[i, 1] == "vacio"))
                                {
                                    bascula[i, 1] = textBox2.Text;
                                    bascula[i, 2] = this.usuario;
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Refresh();
                                    dataGridView1.Rows.Add(bascula[i, 0], bascula[i, 1],
                                 bascula[i, 2], bascula[i, 3], bascula[i, 4],
                                  bascula[i, 5], bascula[i, 6]);
                                    guardar(bascula);
                                    textBox2.Text = "";



                                }
                                else if ((combo == "BASCULA2") && (bascula[i, 3] == "vacio"))
                                {
                                    bascula[i, 3] = textBox2.Text;
                                    bascula[i, 4] = this.usuario;
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Refresh();
                                    dataGridView1.Rows.Add(bascula[i, 0], bascula[i, 1],
                        bascula[i, 2], bascula[i, 3], bascula[i, 4],
                         bascula[i, 5], bascula[i, 6]);
                                    guardar(bascula);
                                    textBox2.Text = "";


                                }
                                else if ((combo == "BASCULA3") && (bascula[i, 5] == "vacio"))
                                {
                                    bascula[i, 5] = textBox2.Text;
                                    bascula[i, 6] = this.usuario;
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Refresh();
                                    dataGridView1.Rows.Add(bascula[i, 0], bascula[i, 1],
                           bascula[i, 2], bascula[i, 3], bascula[i, 4],
                            bascula[i, 5], bascula[i, 6]);
                                    guardar(bascula);
                                    textBox2.Text = "";


                                }
                                else
                                {
                                    MessageBox.Show("Ya ha sido agregado el peso de esta bascula, seleccione otra ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("PORFAVOR INGRESE EL PESO ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                else
                {
                    MessageBox.Show("seleccione una bascula ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("seleccione un contenedor a agregar este peso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
