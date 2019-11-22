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
    public partial class ingreso : Form
    {
         private string usuario;

        public ingreso(string user)
        {
            InitializeComponent();
            this.usuario = user;

            dataGridView1.ReadOnly = true;
            string[,] informacion = new string[0,0];
            informacion = Informacion();

            if (informacion.GetLength(0)==0)
            {
                dataGridView1.Rows.Add("vacio", "vacio", "vacio", "vacio", "vacio", "vacio", "vacio", "vacio");
            }
            else
            {
                for (int i = 0; i < informacion.GetLength(0); i++)
                {
                  
                        dataGridView1.Rows.Add(informacion[i, 0], informacion[i, 1],
                                        informacion[i, 2], informacion[i, 3],
                                        informacion[i, 4], informacion[i, 5],
                                        informacion[i, 6], informacion[i, 7]);
                  
                        
                }
            }
        }

        private void cambiar_tamaño(ref string[,] original, int filas)
	{
	    //create a new 2 dimensional array with
	    //the size we want
	    string[,] newArray = new string[filas,8];
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
            int contador = 0, alto = 0, ancho = 0,fila_info=1;
            bool banderita = false;

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\DATA\\SubFolder\\info_contenedor.txt");
            while ((linea = file.ReadLine()) != null)
            {
                Array.Clear(line, 0, line.Length);
                line = linea.ToCharArray();

                if (banderita == false)
                {
                    cambiar_tamaño(ref informacion, fila_info);
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

                        for (int a=0; a < informacion.GetLength(0); a++)
                        {
                            if (informacion[a, 0] == linea)
                            {
                                MessageBox.Show("id "+linea+" aparece mas de una vez en el formulario, has sido agregada unicamente la primera vez que aparece", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                banderita = true;
                                break;
                            }                     
                        }

                        if (banderita==false)
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

        private void button2_Click(object sender, EventArgs e)
        {
            string[,] informacion = new string[0,0];
            informacion = Informacion();
            bool banderita = false;

            if ((string.IsNullOrEmpty(maskedTextBox1.Text)) || (string.IsNullOrEmpty(maskedTextBox2.Text)) ||
               (string.IsNullOrEmpty(maskedTextBox3.Text)) || (string.IsNullOrEmpty(maskedTextBox4.Text)) ||
               (string.IsNullOrEmpty(maskedTextBox5.Text)) || (string.IsNullOrEmpty(maskedTextBox6.Text)) ||
               (string.IsNullOrEmpty(maskedTextBox7.Text)))
               {
                MessageBox.Show("No puede dejar campos vacíos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int a = 0; a < informacion.GetLength(0); a++)
                {
                    if (informacion[a, 0] == maskedTextBox1.Text)
                    {
                        MessageBox.Show("id " + maskedTextBox1.Text + " ya se encuentra en uso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        banderita = true;
                        break;
                    }
                }
                
                if (banderita == false)
                {
                    if (informacion.GetLength(0) == 0)
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Refresh();
                    }

                    string[,] newArray = new string[informacion.GetLength(0) + 1, 8];
                    Array.Copy(informacion, newArray, informacion.Length);
                    informacion = newArray;

                    int alto = informacion.GetLength(0) - 1;

                    informacion[alto, 0]= maskedTextBox1.Text; informacion[alto, 1] = maskedTextBox2.Text;
                    informacion[alto, 2]= maskedTextBox3.Text; informacion[alto, 3] = maskedTextBox4.Text;
                    informacion[alto, 4]= maskedTextBox5.Text; informacion[alto, 5] = this.usuario;
                    informacion[alto, 6]= maskedTextBox6.Text; informacion[alto, 7]= maskedTextBox7.Text;

                   
                        maskedTextBox1.Text = ""; maskedTextBox2.Text = ""; maskedTextBox3.Text = "";
                        maskedTextBox4.Text = ""; maskedTextBox5.Text = ""; maskedTextBox6.Text = "";
                        maskedTextBox7.Text = "";


                    dataGridView1.Rows.Add(informacion[alto, 0], informacion[alto, 1],
                                        informacion[alto, 2], informacion[alto, 3],
                                        informacion[alto, 4], informacion[alto, 5],
                                        informacion[alto, 6], informacion[alto, 7]);
                    try
                    {//Pass the filepath and filename to the StreamWriter 
                        System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\DATA\\SubFolder\\info_contenedor.txt");
                        //Write a line of text
                        for(int i=0; i < informacion.GetLength(0); i++)
                        {
                           sw.WriteLine(informacion[i, 0] + "|" + informacion[i, 1] + "|" +
                                        informacion[i, 2] + "|" + informacion[i, 3] + "|" +
                                        informacion[i, 4] + "|" + informacion[i, 5] + "|" +
                                        informacion[i, 6] + "|" + informacion[i, 7] + "|" );
                        }
                        
                        //Close the file
                        sw.Close();
                    }catch (Exception)
                    {
                        MessageBox.Show("Error al guardar la info en el archivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    }

            }
        }


        


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ingreso_Load(object sender, EventArgs e)
        {

        }
    }
}
