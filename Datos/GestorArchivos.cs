
using System;
using System.Collections.Generic;
using System.IO;
using AsignacionPuestosUni.Dominio;
using AsignacionPuestosUni.Logica;

namespace AsignacionPuestosUni.Datos
{
    public class GestorArchivos
    {
        private string rutaArchivo = "DatosAsignacion.txt";

        public void GuardarDatos(SistemaAsignacion sistema)
        {
            List<string> lineas = new List<string>();

            foreach (Estudiante e in sistema.Estudiantes)
            {
                lineas.Add("E|" + e.Identificacion + "|" +
                    e.Nombre + "|" + e.Apellido + "|" +
                    e.ProgramaAcademico);
            }

            foreach (PuestoVenta p in sistema.Puestos)
            {
                lineas.Add("P|" + p.Codigo + "|" +
                    p.Ubicacion + "|" + p.Descripcion + "|" +
                    p.Estado);
            }

            foreach (Postulacion p in sistema.Postulaciones)
            {
                lineas.Add("S|" + p.Estudiante.Identificacion + "|" +
                    p.Puesto.Codigo + "|" + p.Estado + "|" +
                    p.FechaPostulacion.ToString("o"));
            }

            File.WriteAllLines(rutaArchivo, lineas);
        }

        public void CargarDatos(SistemaAsignacion sistema)
        {
            if (!File.Exists(rutaArchivo))
            {
                return;
            }

            sistema.Estudiantes.Clear();
            sistema.Puestos.Clear();
            sistema.Postulaciones.Clear();

            string[] lineas = File.ReadAllLines(rutaArchivo);

            foreach (string linea in lineas)
            {
                string[] datos = linea.Split('|');

                if (datos.Length < 2)
                {
                    continue;
                }

                if (datos[0] == "E" && datos.Length == 5)
                {
                    Estudiante estudiante = new Estudiante(
                        datos[1],
                        datos[2],
                        datos[3],
                        datos[4]
                    );

                    sistema.AgregarEstudiante(estudiante);
                }
                else if (datos[0] == "P" && datos.Length == 5)
                {
                    PuestoVenta puesto = new PuestoVenta(
                        datos[1],
                        datos[2],
                        datos[3]
                    );

                    puesto.Estado = datos[4];

                    sistema.AgregarPuesto(puesto);
                }
                else if (datos[0] == "S" && datos.Length == 5)
                {
                    Estudiante? estudianteEncontrado = null;
                    PuestoVenta? puestoEncontrado = null;

                    foreach (Estudiante e in sistema.Estudiantes)
                    {
                        if (e.Identificacion == datos[1])
                        {
                            estudianteEncontrado = e;
                            break;
                        }
                    }

                    foreach (PuestoVenta p in sistema.Puestos)
                    {
                        if (p.Codigo == datos[2])
                        {
                            puestoEncontrado = p;
                            break;
                        }
                    }

                    if (estudianteEncontrado != null &&
                        puestoEncontrado != null)
                    {
                        Postulacion postulacion = new Postulacion(
                            estudianteEncontrado,
                            puestoEncontrado
                        );

                        postulacion.Estado = datos[3];
                        postulacion.FechaPostulacion =
                            DateTime.Parse(datos[4]);

                        sistema.Postulaciones.Add(postulacion);
                    }
                }
            }
        }
    }
}
