using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class INICIO : Form
    {
        public INICIO()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        public static void CreaDirectorio(string strRuta, string strArchivo)
        {
            string pathString = strRuta;
            System.IO.Directory.CreateDirectory(strRuta);
            pathString = System.IO.Path.Combine(pathString, strArchivo);
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



        public string [,] contraseñaCalc() {
        
            ////////////////////////////////////////////////////////////////////
            char[] line = new char[100];
          List <string> buffer = new List <string>();
            string[,] contraseñas = new string[0, 2];
            Array.Clear(contraseñas, 0, contraseñas.Length);
            string linea;
            int contador = 0, alto = 0, ancho = 0, fila_info = 1;
          
            System.IO.StreamReader file = new System.IO.StreamReader(@"c:\DATA\SubFolder\Contraseña.txt");
          
            if ((file.ReadLine()) == null)
            {
                cambiar_tamaño(ref contraseñas, fila_info, 2);
                file.Close();
             contraseñas[0,0]="admin";
             contraseñas[0,1] ="admin";
                guardar(contraseñas);
            }
            else
            {
                file.Close();
                file = new System.IO.StreamReader(@"c:\DATA\SubFolder\Contraseña.txt");

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

                
            }
            file.Close();
            return (contraseñas);
          
        }


        private void button1_Click(object sender, EventArgs e)
        {
            bool pase = false;
            CreaDirectorio("C:\\DATA\\SubFolder\\", "Contraseña.txt");
            System.IO.StreamWriter fichero;
            fichero = File.AppendText("C:\\DATA\\SubFolder\\Contraseña.txt");
            fichero.Close();

            CreaDirectorio("C:\\DATA\\SubFolder\\", "info_contenedor.txt");
            fichero = File.AppendText("C:\\DATA\\SubFolder\\info_contenedor.txt");
            fichero.Close();

            CreaDirectorio("C:\\DATA\\SubFolder\\", "patio.txt");
            fichero = File.AppendText("C:\\DATA\\SubFolder\\patio.txt");
            fichero.Close();

            CreaDirectorio("C:\\DATA\\SubFolder\\", "peso.txt");
            fichero = File.AppendText("C:\\DATA\\SubFolder\\peso.txt");
            fichero.Close();







            if ((string.IsNullOrEmpty(user.Text)) || (string.IsNullOrEmpty(pass.Text)))
            {
                MessageBox.Show("usuario o contraseña vacia, intente nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                user.Text = "";
                pass.Text = "";
            }
            else
            {
                string[,] contraseñas = new string[0, 2];
                contraseñas = contraseñaCalc();

                for (int i = 0; i < contraseñas.GetLength(0); i++)
                {
                    if ((user.Text == contraseñas[i, 0]) && (pass.Text == contraseñas[i, 1]))
                    {
                        pase = true;
                    }
                }

                if (pase == true)
                {
                    using (menu lanzar = new menu(user.Text))
                    {
                        lanzar.StartPosition = FormStartPosition.CenterScreen;
                        this.Hide();
                        lanzar.ShowDialog();
                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("usuario denegado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    user.Text = "";
                    pass.Text = "";
                }
            }

           
             
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

    

       
    }
}
