namespace GestionEstudiante.Models
{
    public class Estudiante
    {
        
            public int IdEstudiante { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string DNI { get; set; }
            public string Correo { get; set; }
            public bool Estado { get; set; }


            public Estudiante()
            {
                IdEstudiante = 0;
                Nombres = "";
                Apellidos = "";
                DNI = "";
                Correo = "";
                Estado = true;

            }

            public Estudiante(int idEstudiante, string nombres, string apellidos, string dni, string correo, bool estado)
            {
                IdEstudiante = idEstudiante;
                Nombres = nombres;
                Apellidos = apellidos;
                DNI = dni;
                Correo = correo;
                Estado = estado;
            }
        }
}
