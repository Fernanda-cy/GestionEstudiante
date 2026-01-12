
using Microsoft.Data.SqlClient;
using GestionEstudiante.Models;
using System.Data;

namespace GestionEstudiante.Repository
{
    public class EstudianteRepository
    {
        private readonly string cn;

        public EstudianteRepository(IConfiguration configuration)
        {
            cn = configuration.GetConnectionString("cn")!;
        }

        public Usuario? ValidarUsuario(string correo, string password)
        {
            Usuario? user = null;
            using SqlConnection con = new(cn);
            using SqlCommand cmd = new("SP_ValidarUsuario", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Correo", (object)correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Password", (object)password ?? DBNull.Value);
            con.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                user = new Usuario
                {
                    Nombre = dr["Nombre"].ToString()!,
                    Rol = dr["Rol"].ToString()!
                };
            }
            return user;
        }

        public List<Estudiante> listar(string? busqueda = null)
        {
            List<Estudiante> lista = new();

            using SqlConnection con = new(cn);
            using SqlCommand cmd = new("SP_ListarEstudiante", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Pasamos el parámetro al SP. Si es null, enviamos DBNull.Value
            cmd.Parameters.AddWithValue("@Busqueda", (object?)busqueda ?? DBNull.Value);

            con.Open();
            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new Estudiante
                {
                    IdEstudiante = (int)dr["IdEstudiante"],
                    Nombres = dr["Nombres"]?.ToString() ?? "",
                    Apellidos = dr["Apellidos"]?.ToString() ?? "",
                    DNI = dr["DNI"]?.ToString() ?? "",
                    Correo = dr["Correo"]?.ToString() ?? "",
                    Estado = (bool)dr["Estado"]
                });
            }
            return lista;
        }

        public void Insertar (Estudiante e)
            {
            using SqlConnection con = new(cn);
            using SqlCommand cmd = new("SP_InsertarEstudiante", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Nombres", e.Nombres);
            cmd.Parameters.AddWithValue("@Apellidos", e.Apellidos);
            cmd.Parameters.AddWithValue("@DNI", e.DNI);
            cmd.Parameters.AddWithValue("@Correo", e.Correo);

            con.Open();
            cmd.ExecuteNonQuery();

            }

        public Estudiante? Buscar(int IdEstudiante)
        {
            Estudiante? e = null;

            using SqlConnection con = new(cn);
            using SqlCommand cmd = new("SP_BuscarEstudiante", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEstudiante",IdEstudiante);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                e = new Estudiante
                {
                    IdEstudiante = (int)dr["IdEstudiante"],
                    Nombres = dr["Nombres"]?.ToString() ?? "",
                    Apellidos = dr["Apellidos"]?.ToString() ?? "",
                    DNI = dr["DNI"]?.ToString() ?? "",
                    Correo = dr["Correo"]?.ToString() ?? "",
                    Estado = dr["Estado"] != DBNull.Value && (bool)dr["Estado"]
                };
            }
            return e;
        }

        public void Actualizar(Estudiante e)
        { 

            using SqlConnection con = new(cn);
            using SqlCommand cmd = new("SP_ActualizarEstudiante", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEstudiante", e.IdEstudiante);
            cmd.Parameters.AddWithValue("@Nombres", e.Nombres);
            cmd.Parameters.AddWithValue("@Apellidos", e.Apellidos);
            cmd.Parameters.AddWithValue("@DNI", e.DNI);
            cmd.Parameters.AddWithValue("@Correo", e.Correo);
            cmd.Parameters.AddWithValue("@Estado", e.Estado);
            

            con.Open();
            cmd.ExecuteNonQuery();


        }

        
        public void Eliminar(int IdEstudiante)
        {
            using SqlConnection con = new(cn);
            using SqlCommand cmd = new("SP_EliminarEstudiante", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEstudiante",IdEstudiante);
           
            con.Open(); 
            cmd.ExecuteNonQuery();
        }
        
        
    }
}

