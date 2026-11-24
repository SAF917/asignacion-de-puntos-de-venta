namespace AsignacionPuestosUni.Dominio
{
    public class Persona
    {
        public string Identificacion;
        public string Nombre;
        public string Apellido;

        // Constructor
        public Persona(string identificacion, string nombre, string apellido)
        {
            Identificacion = identificacion;
            Nombre = nombre;
            Apellido = apellido;
        }

        // Método que luego vamos a sobrescribir
        public virtual string MostrarDatos()
        {
            return Identificacion + " - " + Nombre + " " + Apellido;
        }
    }
}