
namespace AsignacionPuestosUni.Dominio
{
    public class PuestoVenta
    {
        public string Codigo;
        public string Ubicacion;
        public string Descripcion;
        public string Estado;

        public PuestoVenta(string codigo, string ubicacion, string descripcion)
        {
            Codigo = codigo;
            Ubicacion = ubicacion;
            Descripcion = descripcion;
            Estado = "Disponible";
        }

        public bool EstaDisponible()
        {
            if (Estado == "Disponible")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Asignar()
        {
            if (Estado == "Disponible")
            {
                Estado = "Ocupado";
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Liberar()
        {
            Estado = "Disponible";
        }

        public string MostrarDatos()
        {
            return Codigo + " | " + Ubicacion + " | " +
                   Descripcion + " | " + Estado;
        }
    }
}
