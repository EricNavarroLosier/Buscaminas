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
                    boton.Tag = "0";
                    matrizBotones[i, j] = boton;
                    panel1.Controls.Add(boton);
                }
            poneMinas();
            cuentaMinas();
        }

        private void cuentaMinas()
        {
            //cambio los tag para que indiquen el nº de minas que hay alrededor
            //los dos for anidados externos recorren uno por uno los elementos de la matriz
            //los dos for anidados interiores recorren los 8 botones alrededor de una casilla 
            //y suman el número de minas
            for (int i = 0; i < filas; i++)
                for (int j = 0; j < columnas; j++)
                {
                    int numeroMinas = 0;
                    for (int m = -1; m < 2; m++)
                        for (int n = -1; n < 2; n++)
                        {
                            int f = i + m;
                            int c = j + n;
                            if ((c < columnas) && (c >= 0) && (f < filas) && (f >= 0))
                            {
                                if (matrizBotones[c, f].Tag == "B")
                                {
                                    numeroMinas++;
                                }
                            }
                        }
                    if ((numeroMinas > 0) && (matrizBotones[j, i].Tag != "B"))
                    {
                        matrizBotones[j, i].Tag = numeroMinas;
                        matrizBotones[j, i].Text = numeroMinas.ToString();
                    }
                }
        }// fin de cuentMminas


        /*
-         * chequeaBoton es un METODO RECURSIVO
-         * esto significa que se llama a sí mismo
-         * Las precauciones con los métodos recursivos pasan por:
-         * 1º entender qué hace el método y porqué se hace recursivo
-         * 2º siempre tiene que tener una salida con un if - else porque sino se cuelga el programa
-         */

        private void poneMinas()
        {
            Random aleatorio = new Random();
            int x, y = 0;
            for (int i = 0; i < minas; i++)
            {
                x = aleatorio.Next(filas);
                y = aleatorio.Next(columnas);
                while (!matrizBotones[y, x].Tag.Equals("0"))
                {
                    x = aleatorio.Next(filas);
                    y = aleatorio.Next(columnas);
                }
                matrizBotones[y, x].Tag = "B";
                matrizBotones[y, x].Text = "B";
                matrizBotones[y, x].BackColor = Color.Orange;
            }

        }


        private void chequeaBoton(object sender, EventArgs e)
        {
            //chequeaBoton mira en las 8 posiciones alrededor de un boton buscando las celdas vacías
            Button b = (sender as Button);
            int columna = b.Location.X / anchoBoton;
            int fila = b.Location.Y / anchoBoton;
            int numeroMinasAlrededor = 0;

            //si el Tag es 0 es porque no hay ninguna mina alrededor. 
            //si fuera distinto de cero, no tengo que chequear nada mas
            if (matrizBotones[columna, fila].Tag == "0")
            {
                b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        int f = fila + i;
                        int c = columna + j;
                        if ((c < columnas) && (c >= 0) && (f < filas) && (f >= 0))
                        {
                            if (matrizBotones[c, f].FlatStyle != System.Windows.Forms.FlatStyle.Flat)
                            {
                                if (numeroMinasAlrededor == 0)
                                {
                                    chequeaBoton(matrizBotones[c, f], e);
                                }
                                else
                                {
                                    numeroMinasAlrededor++;
                                }
                            }

                        }
                    }
                }
            }
        } //fin de chequeaBoton

    }
}