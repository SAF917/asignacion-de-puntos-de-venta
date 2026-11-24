
namespace AsignacionPuestosUni.Dominio
{
    public class Estudiante : Persona
    {
        public string ProgramaAcademico;

        public Estudiante(string identificacion, string nombre,
                          string apellido, string programaAcademico)
            : base(identificacion, nombre, apellido)
        {
            ProgramaAcademico = programaAcademico;
        }

        public override string MostrarDatos()
        {
            return base.MostrarDatos() +
                   " | Programa: " + ProgramaAcademico;
        }
    }
}
