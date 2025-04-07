using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfacesGraficas
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public partial class Form1 : Form

        {

            // Vector para almacenar los puntos que formarán la figura 

            private Point[] puntos;

            // Matriz de ejemplo (podría representar una transformación) 

            private int[,] matriz;



            public Form1()

            {

                InicializarComponentesPersonalizados();

            }



            private void InicializarComponentesPersonalizados()

            {

                // Configuración básica del formulario 

                this.Text = "Aplicación Gráfica en .NET";

                this.Size = new Size(800, 600);



                // TextBox para ingresar el número de puntos (solo se permiten números) 

                TextBox txtNumeroPuntos = new TextBox();

                txtNumeroPuntos.Location = new Point(20, 20);

                txtNumeroPuntos.Width = 100;

                txtNumeroPuntos.Name = "txtNumeroPuntos";

                // Validación: solo se permiten dígitos 

                txtNumeroPuntos.KeyPress += TxtNumeroPuntos_KeyPress;

                this.Controls.Add(txtNumeroPuntos);



                // ComboBox para seleccionar el tipo de figura 

                ComboBox cmbFigura = new ComboBox();

                cmbFigura.Location = new Point(140, 20);

                cmbFigura.Width = 150;

                cmbFigura.Name = "cmbFigura";

                cmbFigura.Items.Add("Triángulo");

                cmbFigura.Items.Add("Cuadrado");

                cmbFigura.Items.Add("Círculo");

                // Aquí se podría agregar validación adicional o acciones al cambiar la selección 

                cmbFigura.SelectedIndexChanged += CmbFigura_SelectedIndexChanged;

                this.Controls.Add(cmbFigura);



                // Botón para generar y dibujar la figura 

                Button btnDibujar = new Button();

                btnDibujar.Location = new Point(310, 20);

                btnDibujar.Text = "Dibujar";

                btnDibujar.Click += (sender, e) =>

                {

                    // Validación: se debe ingresar un valor y seleccionar una figura 

                    if (string.IsNullOrEmpty(txtNumeroPuntos.Text))

                    {

                        MessageBox.Show("Ingrese el número de puntos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;

                    }

                    if (cmbFigura.SelectedIndex == -1)

                    {

                        MessageBox.Show("Seleccione una figura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;

                    }



                    // Conversión y validación del número ingresado 

                    if (!int.TryParse(txtNumeroPuntos.Text, out int numPuntos))

                    {

                        MessageBox.Show("El número de puntos debe ser numérico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;

                    }

                    if (numPuntos < 3)

                    {

                        MessageBox.Show("Se requieren al menos 3 puntos para formar una figura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;

                    }



                    // Creación del vector: se generan puntos aleatorios para formar la figura 

                    puntos = new Point[numPuntos];

                    Random rnd = new Random();

                    for (int i = 0; i < numPuntos; i++)

                    {

                        puntos[i] = new Point(rnd.Next(50, this.ClientSize.Width - 50),

                                              rnd.Next(50, this.ClientSize.Height - 50));

                    }



                    // Creación de una matriz de 3x3 (por ejemplo, una matriz identidad) 

                    matriz = new int[3, 3]

                    {

                    {1, 0, 0},

                    {0, 1, 0},

                    {0, 0, 1}

                    };



                    // Forzar el repintado del formulario para mostrar la figura 

                    this.Invalidate();

                };

                this.Controls.Add(btnDibujar);

            }



            // Validación en el TextBox: permite solo dígitos y teclas de control 

            private void TxtNumeroPuntos_KeyPress(object sender, KeyPressEventArgs e)

            {

                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))

                    e.Handled = true;

            }



            private void CmbFigura_SelectedIndexChanged(object sender, EventArgs e)

            {

                // Aquí se puede implementar lógica adicional si se requiere cambiar 

                // el comportamiento según la figura seleccionada. 

            }



            // Método para dibujar la figura en el formulario 

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                if (puntos != null && puntos.Length > 0)
                {
                    ComboBox cmbFigura = this.Controls.OfType<ComboBox>().FirstOrDefault(c => c.Name == "cmbFigura");

                    if (cmbFigura != null && cmbFigura.SelectedItem != null && cmbFigura.SelectedItem.ToString() == "Círculo")
                    {

                        int radio = 100;
                        foreach (var punto in puntos)
                        {
                            e.Graphics.DrawEllipse(Pens.Blue, punto.X - radio, punto.Y - radio, radio * 2, radio * 2);
                        }
                    }
                    else
                    {

                        e.Graphics.DrawPolygon(Pens.Blue, puntos);
                        foreach (var punto in puntos)
                        {
                            e.Graphics.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
                        }
                    }
                }
            }

        }

    }
}