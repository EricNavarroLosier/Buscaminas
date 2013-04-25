using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 *  NOMBRE: Eric   
 *  APELLIDOS: Navarro Losier
 *  ESTO ES UNA PRUEBA DE GITHUB 
 * 
 */

namespace Buscaminas
{
    public partial class Form1 : Form
    {
        //declaro el array de botones
        Button[,] matrizBotones;
        int columnas = 20;
        int filas = 20;
        int anchoBoton = 20;
        int minas = 20;

        // Si el tag = 1 >> No hay bomba
        // Si el tag = 2 >> Sí hay bomba

        public Form1()
        {
            InitializeComponent();            

            this.Height = filas * anchoBoton + 40;
            this.Width = columnas * anchoBoton + 18;

            matrizBotones = new Button[filas, columnas];

            for (int i = 0; i < filas; i++)
                for (int j = 0; j < columnas; j++)
                {
                    Button boton = new Button();
                    //boton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    boton.Width = anchoBoton;
                    boton.Height = anchoBoton;
                    boton.Location = new Point(i * anchoBoton, j * anchoBoton);
                    boton.Click += chequeaBoton;
                    boton.Tag = "1";
                    matrizBotones[i, j] = boton;
                    panel1.Controls.Add(boton);
                }
            poneMinas();
        }

         /*
-         * chequeaBoton es un METODO RECURSIVO
-         * esto significa que se llama a sí mismo
-         * Las precauciones con los métodos recursivos pasan por:
-         * 1º entender qué hace el método y porqué se hace recursivo
-         * 2º siempre tiene que tener una salida con un if - else porque sino se cuelga el programa
-         */

        private void poneMinas() { 
            Random aleatorio = new Random();
            int x, y = 0;
            for (int i = 0; i < minas; i++)
            {
                x = aleatorio.Next(filas);
                y = aleatorio.Next(columnas);
                while (!matrizBotones[y, x].Tag.Equals("1")) 
                {
                    x = aleatorio.Next(filas);
                    y = aleatorio.Next(columnas);
                }
                matrizBotones[y, x].Tag = "2";
                matrizBotones[y, x].Text = "B";
            }

        }

        
        private void chequeaBoton(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            Button b = (sender as Button);
            int columna  = b.Location.X /anchoBoton;
            int fila = b.Location.Y / anchoBoton;

            // Este "for anidado" ayudará a hacer el código más práctico y más corto
            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {              
                    if (
                        (columna + j < columnas) &&
                        (columna + j >= 0) &&
                        (fila + i < filas) &&
                        (fila + i >= 0)
                        )
                    {
                        if (matrizBotones[columna + j, fila + i].BackColor != Color.ForestGreen)
                        {
                            matrizBotones[columna + j, fila + i].BackColor = Color.ForestGreen;
                            chequeaBoton(matrizBotones[columna + j, fila + i], e);
                        }
                        
                        }
                }
            }
                           

        }
    }
}
