
using System.Collections.Generic;
using AsignacionPuestosUni.Dominio;

namespace AsignacionPuestosUni.Logica
{
    public class SistemaAsignacion
    {
        public List<Estudiante> Estudiantes;
        public List<PuestoVenta> Puestos;
        public List<Postulacion> Postulaciones;

        public SistemaAsignacion()
        {
            Estudiantes = new List<Estudiante>();
            Puestos = new List<PuestoVenta>();
            Postulaciones = new List<Postulacion>();
        }

        public bool AgregarEstudiante(Estudiante estudiante)
        {
            foreach (Estudiante e in Estudiantes)
            {
                if (e.Identificacion == estudiante.Identificacion)
                {
                    return false;
                }
            }

            Estudiantes.Add(estudiante);
            return true;
        }

        public bool AgregarPuesto(PuestoVenta puesto)
        {
            foreach (PuestoVenta p in Puestos)
            {
                if (p.Codigo == puesto.Codigo)
                {
                    return false;
                }
            }

            Puestos.Add(puesto);
            return true;
        }

        public bool RegistrarPostulacion(Estudiante estudiante, PuestoVenta puesto)
        {
            if (estudiante == null || puesto == null)
            {
                return false;
            }

            if (!Estudiantes.Contains(estudiante) ||
                !Puestos.Contains(puesto))
            {
                return false;
            }

            if (!puesto.EstaDisponible())
            {
                return false;
            }

            foreach (Postulacion p in Postulaciones)
            {
                if (p.Estudiante.Identificacion == estudiante.Identificacion &&
                    p.Estado == "Aprobada")
                {
                    return false;
                }

                if (p.Estudiante.Identificacion == estudiante.Identificacion &&
                    p.Estado == "Pendiente")
                {
                    return false;
                }
            }

            Postulaciones.Add(new Postulacion(estudiante, puesto));

            return true;
        }

        public void ProcesarSolicitudes()
        {
            foreach (Postulacion p in Postulaciones)
            {
                if (p.Estado == "Pendiente")
                {
                    p.Aprobar();
                }
            }
        }

        public List<PuestoVenta> ObtenerPuestos()
        {
            return Puestos;
        }

        public List<Postulacion> ObtenerPostulaciones()
        {
            return Postulaciones;
        }

        public string GenerarInforme()
        {
            int disponibles = 0;
            int ocupados = 0;

            int pendientes = 0;
            int aprobadas = 0;
            int rechazadas = 0;

            bool hayPuestosDisponibles = false;

            foreach (PuestoVenta p in Puestos)
            {
                if (p.Estado == "Disponible")
                {
                    disponibles++;
                    hayPuestosDisponibles = true;
                }
                else
                {
                    ocupados++;
                }
            }

            foreach (Postulacion p in Postulaciones)
            {
                if (p.Estado == "Pendiente")
                {
                    pendientes++;
                }
                else if (p.Estado == "Aprobada")
                {
                    aprobadas++;
                }
                else if (p.Estado == "Rechazada")
                {
                    rechazadas++;
                }
            }

            string informe = "INFORME DEL SISTEMA\n\n";

            informe += "Total de puestos: " + Puestos.Count + "\n";
            informe += "Puestos disponibles: " + disponibles + "\n";
            informe += "Puestos ocupados: " + ocupados + "\n\n";

            informe += "Total de solicitudes: " + Postulaciones.Count + "\n";
            informe += "Solicitudes pendientes: " + pendientes + "\n";
            informe += "Solicitudes aprobadas: " + aprobadas + "\n";
            informe += "Solicitudes rechazadas: " + rechazadas + "\n\n";

            if (hayPuestosDisponibles)
            {
                informe += "Hay puestos disponibles para asignar.";
            }
            else
            {
                informe += "No hay puestos disponibles actualmente.";
            }

            return informe;
        }
    }
}
