
namespace AsignacionPuestosUni.Dominio
{
    public class Carrera
    {
        public string Codigo;
        public string Nombre;
        public int CuposTotales;
        public int CuposDisponibles;

        public Carrera(string codigo, string nombre, int cupos)
        {
            Codigo = codigo;
            Nombre = nombre;
            CuposTotales = cupos;
            CuposDisponibles = cupos;
        }

        public bool TieneCupos()
        {
            if (CuposDisponibles > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AsignarCupo()
        {
            if (CuposDisponibles > 0)
            {
                CuposDisponibles--;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string MostrarDatos()
        {
            return Codigo + " - " + Nombre +
                   " | Cupos disponibles: " + CuposDisponibles;
        }
    }
}
