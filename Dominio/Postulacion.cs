
using System;

namespace AsignacionPuestosUni.Dominio
{
    public class Postulacion
    {
        public Estudiante Estudiante;
        public PuestoVenta Puesto;
        public string Estado;
        public DateTime FechaPostulacion;

        public Postulacion(Estudiante estudiante, PuestoVenta puesto)
        {
            Estudiante = estudiante;
            Puesto = puesto;
            Estado = "Pendiente";
            FechaPostulacion = DateTime.Now;
        }

        public bool Aprobar()
        {
            if (Puesto.EstaDisponible())
            {
                Puesto.Asignar();
                Estado = "Aprobada";
                return true;
            }
            else
            {
                Estado = "Rechazada";
                return false;
            }
        }

        public void Rechazar()
        {
            Estado = "Rechazada";
        }

        public string MostrarDatos()
        {
            return Estudiante.Nombre + " " + Estudiante.Apellido +
                   " | Puesto: " + Puesto.Codigo +
                   " | Estado: " + Estado;
        }
    }
}
