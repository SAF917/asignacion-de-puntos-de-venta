
using System;
using System.Drawing;
using System.Windows.Forms;
using AsignacionPuestosUni.Dominio;
using AsignacionPuestosUni.Logica;
using AsignacionPuestosUni.Datos;
using System.Collections.Generic;

namespace AsignacionPuestosUni
{
    public partial class Form1 : Form
    {
        private SistemaAsignacion sistema;
        private GestorArchivos gestor;

        private DataGridView tablaEstudiantes = new DataGridView();
        private DataGridView tablaPuestos = new DataGridView();
        private DataGridView tablaSolicitudes = new DataGridView();

        private ComboBox comboEstudiantes = new ComboBox();
        private ComboBox comboPuestos = new ComboBox();

        private List<PuestoVenta> puestosDisponibles = new List<PuestoVenta>();

        public Form1()
        {
            InitializeComponent();

            sistema = new SistemaAsignacion();
            gestor = new GestorArchivos();

            gestor.CargarDatos(sistema);

            CrearInterfaz();
            ActualizarTablas();
        }

        private void CrearInterfaz()
        {
            this.Text = "Sistema de Asignacion de Puestos de Venta";
            this.Size = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            TabControl pestañas = new TabControl();
            pestañas.Dock = DockStyle.Fill;

            TabPage paginaEstudiantes = new TabPage("Estudiantes");
            TabPage paginaPuestos = new TabPage("Puestos de Venta");
            TabPage paginaSolicitudes = new TabPage("Asignaciones");

            pestañas.TabPages.Add(paginaEstudiantes);
            pestañas.TabPages.Add(paginaPuestos);
            pestañas.TabPages.Add(paginaSolicitudes);

            this.Controls.Add(pestañas);

            CrearPantallaEstudiantes(paginaEstudiantes);
            CrearPantallaPuestos(paginaPuestos);
            CrearPantallaSolicitudes(paginaSolicitudes);
        }

        private void CrearPantallaEstudiantes(TabPage pagina)
        {
            Label titulo = new Label();
            titulo.Text = "REGISTRO DE ESTUDIANTES";
            titulo.Location = new Point(20, 15);
            titulo.AutoSize = true;
            titulo.Font = new Font("Arial", 14, FontStyle.Bold);

            Label lblIdentificacion = new Label();
            lblIdentificacion.Text = "Identificacion:";
            lblIdentificacion.Location = new Point(20, 65);
            lblIdentificacion.AutoSize = true;

            TextBox txtIdentificacion = new TextBox();
            txtIdentificacion.Location = new Point(130, 62);
            txtIdentificacion.Size = new Size(180, 25);

            Label lblNombre = new Label();
            lblNombre.Text = "Nombre:";
            lblNombre.Location = new Point(350, 65);
            lblNombre.AutoSize = true;

            TextBox txtNombre = new TextBox();
            txtNombre.Location = new Point(420, 62);
            txtNombre.Size = new Size(180, 25);

            Label lblApellido = new Label();
            lblApellido.Text = "Apellido:";
            lblApellido.Location = new Point(20, 110);
            lblApellido.AutoSize = true;

            TextBox txtApellido = new TextBox();
            txtApellido.Location = new Point(130, 107);
            txtApellido.Size = new Size(180, 25);

            Label lblPrograma = new Label();
            lblPrograma.Text = "Programa academico:";
            lblPrograma.Location = new Point(350, 110);
            lblPrograma.AutoSize = true;

            TextBox txtPrograma = new TextBox();
            txtPrograma.Location = new Point(490, 107);
            txtPrograma.Size = new Size(180, 25);

            Button btnRegistrar = new Button();
            btnRegistrar.Text = "Registrar estudiante";
            btnRegistrar.Location = new Point(700, 80);
            btnRegistrar.Size = new Size(170, 35);

            tablaEstudiantes.Location = new Point(20, 170);
            tablaEstudiantes.Size = new Size(880, 320);
            tablaEstudiantes.AllowUserToAddRows = false;
            tablaEstudiantes.ReadOnly = true;
            tablaEstudiantes.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            tablaEstudiantes.RowHeadersVisible = false;

            tablaEstudiantes.Columns.Add("Identificacion", "Identificacion");
            tablaEstudiantes.Columns.Add("Nombre", "Nombre");
            tablaEstudiantes.Columns.Add("Apellido", "Apellido");
            tablaEstudiantes.Columns.Add("Programa", "Programa Academico");

            btnRegistrar.Click += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtIdentificacion.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtPrograma.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                Estudiante nuevo = new Estudiante(
                    txtIdentificacion.Text,
                    txtNombre.Text,
                    txtApellido.Text,
                    txtPrograma.Text
                );

                if (sistema.AgregarEstudiante(nuevo))
                {
                    gestor.GuardarDatos(sistema);
                    ActualizarTablas();

                    MessageBox.Show("Estudiante registrado correctamente.");

                    txtIdentificacion.Clear();
                    txtNombre.Clear();
                    txtApellido.Clear();
                    txtPrograma.Clear();
                }
                else
                {
                    MessageBox.Show("Esta identificacion ya esta registrada.");
                }
            };

            pagina.Controls.Add(titulo);
            pagina.Controls.Add(lblIdentificacion);
            pagina.Controls.Add(txtIdentificacion);
            pagina.Controls.Add(lblNombre);
            pagina.Controls.Add(txtNombre);
            pagina.Controls.Add(lblApellido);
            pagina.Controls.Add(txtApellido);
            pagina.Controls.Add(lblPrograma);
            pagina.Controls.Add(txtPrograma);
            pagina.Controls.Add(btnRegistrar);
            pagina.Controls.Add(tablaEstudiantes);
        }

        private void CrearPantallaPuestos(TabPage pagina)
        {
            Label titulo = new Label();
            titulo.Text = "REGISTRO DE PUESTOS DE VENTA";
            titulo.Location = new Point(20, 15);
            titulo.AutoSize = true;
            titulo.Font = new Font("Arial", 14, FontStyle.Bold);

            Label lblCodigo = new Label();
            lblCodigo.Text = "Codigo:";
            lblCodigo.Location = new Point(20, 65);
            lblCodigo.AutoSize = true;

            TextBox txtCodigo = new TextBox();
            txtCodigo.Location = new Point(100, 62);
            txtCodigo.Size = new Size(150, 25);

            Label lblUbicacion = new Label();
            lblUbicacion.Text = "Ubicacion:";
            lblUbicacion.Location = new Point(280, 65);
            lblUbicacion.AutoSize = true;

            TextBox txtUbicacion = new TextBox();
            txtUbicacion.Location = new Point(360, 62);
            txtUbicacion.Size = new Size(180, 25);

            Label lblDescripcion = new Label();
            lblDescripcion.Text = "Descripcion:";
            lblDescripcion.Location = new Point(20, 110);
            lblDescripcion.AutoSize = true;

            TextBox txtDescripcion = new TextBox();
            txtDescripcion.Location = new Point(110, 107);
            txtDescripcion.Size = new Size(300, 25);

            Button btnRegistrar = new Button();
            btnRegistrar.Text = "Registrar puesto";
            btnRegistrar.Location = new Point(570, 75);
            btnRegistrar.Size = new Size(160, 35);

            tablaPuestos.Location = new Point(20, 170);
            tablaPuestos.Size = new Size(880, 320);
            tablaPuestos.AllowUserToAddRows = false;
            tablaPuestos.ReadOnly = true;
            tablaPuestos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            tablaPuestos.RowHeadersVisible = false;

            tablaPuestos.Columns.Add("Codigo", "Codigo");
            tablaPuestos.Columns.Add("Ubicacion", "Ubicacion");
            tablaPuestos.Columns.Add("Descripcion", "Descripcion");
            tablaPuestos.Columns.Add("Estado", "Estado");

            btnRegistrar.Click += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                    string.IsNullOrWhiteSpace(txtUbicacion.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.");
                    return;
                }

                PuestoVenta nuevo = new PuestoVenta(
                    txtCodigo.Text,
                    txtUbicacion.Text,
                    txtDescripcion.Text
                );

                if (sistema.AgregarPuesto(nuevo))
                {
                    gestor.GuardarDatos(sistema);
                    ActualizarTablas();

                    MessageBox.Show("Puesto registrado correctamente.");

                    txtCodigo.Clear();
                    txtUbicacion.Clear();
                    txtDescripcion.Clear();
                }
                else
                {
                    MessageBox.Show("Este codigo de puesto ya existe.");
                }
            };

            pagina.Controls.Add(titulo);
            pagina.Controls.Add(lblCodigo);
            pagina.Controls.Add(txtCodigo);
            pagina.Controls.Add(lblUbicacion);
            pagina.Controls.Add(txtUbicacion);
            pagina.Controls.Add(lblDescripcion);
            pagina.Controls.Add(txtDescripcion);
            pagina.Controls.Add(btnRegistrar);
            pagina.Controls.Add(tablaPuestos);
        }

        private void CrearPantallaSolicitudes(TabPage pagina)
        {
            Label titulo = new Label();
            titulo.Text = "ASIGNACION DE PUESTOS";
            titulo.Location = new Point(20, 15);
            titulo.AutoSize = true;
            titulo.Font = new Font("Arial", 14, FontStyle.Bold);

            Label lblEstudiante = new Label();
            lblEstudiante.Text = "Estudiante:";
            lblEstudiante.Location = new Point(20, 70);
            lblEstudiante.AutoSize = true;

            comboEstudiantes.Location = new Point(110, 67);
            comboEstudiantes.Size = new Size(250, 25);
            comboEstudiantes.DropDownStyle = ComboBoxStyle.DropDownList;

            Label lblPuesto = new Label();
            lblPuesto.Text = "Puesto:";
            lblPuesto.Location = new Point(390, 70);
            lblPuesto.AutoSize = true;

            comboPuestos.Location = new Point(450, 67);
            comboPuestos.Size = new Size(200, 25);
            comboPuestos.DropDownStyle = ComboBoxStyle.DropDownList;

            Button btnSolicitar = new Button();
            btnSolicitar.Text = "Solicitar puesto";
            btnSolicitar.Location = new Point(680, 62);
            btnSolicitar.Size = new Size(170, 35);

            Button btnProcesar = new Button();
            btnProcesar.Text = "Procesar solicitudes";
            btnProcesar.Location = new Point(20, 115);
            btnProcesar.Size = new Size(180, 35);

            Button btnInforme = new Button();
            btnInforme.Text = "Ver informe";
            btnInforme.Location = new Point(220, 115);
            btnInforme.Size = new Size(150, 35);

            tablaSolicitudes.Location = new Point(20, 170);
            tablaSolicitudes.Size = new Size(880, 320);
            tablaSolicitudes.AllowUserToAddRows = false;
            tablaSolicitudes.ReadOnly = true;
            tablaSolicitudes.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            tablaSolicitudes.RowHeadersVisible = false;

            tablaSolicitudes.Columns.Add("Estudiante", "Estudiante");
            tablaSolicitudes.Columns.Add("Puesto", "Puesto");
            tablaSolicitudes.Columns.Add("Estado", "Estado");
            tablaSolicitudes.Columns.Add("Fecha", "Fecha");

            btnSolicitar.Click += (sender, e) =>
            {
                if (comboEstudiantes.SelectedIndex == -1 ||
                    comboPuestos.SelectedIndex == -1)
                {
                    MessageBox.Show("Selecciona un estudiante y un puesto.");
                    return;
                }

                Estudiante estudiante =
                    sistema.Estudiantes[comboEstudiantes.SelectedIndex];

                PuestoVenta puesto =
                    puestosDisponibles[comboPuestos.SelectedIndex];

                if (sistema.RegistrarPostulacion(estudiante, puesto))
                {
                    gestor.GuardarDatos(sistema);
                    ActualizarTablas();

                    MessageBox.Show("Solicitud registrada correctamente.");
                }
                else
                {
                    MessageBox.Show("No se pudo registrar la solicitud. Verifica que el estudiante no tenga otra solicitud pendiente o aprobada y que el puesto este disponible.");
                }
            };

            btnProcesar.Click += (sender, e) =>
            {
                sistema.ProcesarSolicitudes();

                gestor.GuardarDatos(sistema);
                ActualizarTablas();

                MessageBox.Show("Solicitudes procesadas correctamente.");
            };

            btnInforme.Click += (sender, e) =>
            {
                string informe = sistema.GenerarInforme();

                MessageBox.Show(
                    informe,
                    "Informe del sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            };

            pagina.Controls.Add(titulo);
            pagina.Controls.Add(lblEstudiante);
            pagina.Controls.Add(comboEstudiantes);
            pagina.Controls.Add(lblPuesto);
            pagina.Controls.Add(comboPuestos);
            pagina.Controls.Add(btnSolicitar);
            pagina.Controls.Add(btnProcesar);
            pagina.Controls.Add(btnInforme);
            pagina.Controls.Add(tablaSolicitudes);
        }

        private void ActualizarTablas()
        {
            tablaEstudiantes.Rows.Clear();

            foreach (Estudiante e in sistema.Estudiantes)
            {
                tablaEstudiantes.Rows.Add(
                    e.Identificacion,
                    e.Nombre,
                    e.Apellido,
                    e.ProgramaAcademico
                );
            }

            tablaPuestos.Rows.Clear();

            foreach (PuestoVenta p in sistema.Puestos)
            {
                tablaPuestos.Rows.Add(
                    p.Codigo,
                    p.Ubicacion,
                    p.Descripcion,
                    p.Estado
                );
            }

            tablaSolicitudes.Rows.Clear();

            foreach (Postulacion p in sistema.Postulaciones)
            {
                tablaSolicitudes.Rows.Add(
                    p.Estudiante.Nombre + " " + p.Estudiante.Apellido,
                    p.Puesto.Codigo,
                    p.Estado,
                    p.FechaPostulacion.ToString("dd/MM/yyyy HH:mm")
                );
            }

            comboEstudiantes.Items.Clear();

            foreach (Estudiante e in sistema.Estudiantes)
            {
                comboEstudiantes.Items.Add(
                    e.Identificacion + " - " + e.Nombre + " " + e.Apellido
                );
            }

            comboPuestos.Items.Clear();
            puestosDisponibles.Clear();

            foreach (PuestoVenta p in sistema.Puestos)
            {
                if (p.EstaDisponible())
                {
                    puestosDisponibles.Add(p);

                    comboPuestos.Items.Add(
                        p.Codigo + " - " + p.Ubicacion
                    );
                }
            }

            comboEstudiantes.SelectedIndex = -1;
            comboPuestos.SelectedIndex = -1;
        }
    }
}
