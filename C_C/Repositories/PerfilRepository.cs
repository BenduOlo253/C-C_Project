using C_C.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace C_C.Repositories
{
    
    public class PerfilRepository : IPerfilRepository
    {
        private readonly string _connectionString;

        public PerfilRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

       

        public List<Perfil> ObtenerPerfilesSugeridos(int idPerfilActual)
        {
            var listaPerfiles = new List<Perfil>();

            
            string query = @"
                SELECT p.*, a.* FROM Perfil p
                JOIN Alumno a ON p.Matricula = a.Matricula
                WHERE p.IdPerfil != @IdPerfilActual
                AND p.IdPerfil NOT IN (
                    SELECT IdPerfilReceptor FROM Match WHERE IdPerfilEmisor = @IdPerfilActual
                )";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.Add("@IdPerfilActual", SqlDbType.Int).Value = idPerfilActual;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var perfil = MapearPerfil(reader);
                        listaPerfiles.Add(perfil);
                    }
                }
            }

            
            foreach (var p in listaPerfiles)
            {
                p.Fotos.Add("https://placehold.co/400x600/555/FFF?text=Foto+1");
                p.Fotos.Add("https://placehold.co/400x600/666/FFF?text=Foto+2");
            }


            return listaPerfiles;
        }

        public Perfil ObtenerPerfilPorMatricula(int matricula)
        {
            Perfil perfil = null;
            string query = "SELECT p.*, a.* FROM Perfil p JOIN Alumno a ON p.Matricula = a.Matricula WHERE p.Matricula = @Matricula";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.Add("@Matricula", SqlDbType.Int).Value = matricula;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        perfil = MapearPerfil(reader);
                    }
                }
            }

           

            return perfil;
        }

        public Perfil ObtenerPerfilPorId(int idPerfil)
        {
            Perfil perfil = null;
            string query = "SELECT p.*, a.* FROM Perfil p JOIN Alumno a ON p.Matricula = a.Matricula WHERE p.IdPerfil = @IdPerfil";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.Add("@IdPerfil", SqlDbType.Int).Value = idPerfil;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        perfil = MapearPerfil(reader);
                    }
                }
            }

            
            return perfil;
        }

        public void ActualizarPerfil(Perfil perfil)
        {
           
            string query = @"
                UPDATE Alumno SET 
                    Nombre = @Nombre, 
                    Amaterno = @Amaterno, 
                    Apaterno = @Apaterno, 
                    Genero = @Genero, 
                    Email = @Email 
                WHERE Matricula = @Matricula;

                UPDATE Perfil SET 
                    NikName = @NikName, 
                    Biografia = @Biografia 
                WHERE IdPerfil = @IdPerfil;";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
               
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = perfil.Nombre;
                command.Parameters.Add("@Amaterno", SqlDbType.NVarChar).Value = perfil.Amaterno ?? (object)DBNull.Value;
                command.Parameters.Add("@Apaterno", SqlDbType.NVarChar).Value = perfil.Apaterno ?? (object)DBNull.Value;
                command.Parameters.Add("@Genero", SqlDbType.Char, 1).Value = perfil.Genero;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = perfil.Email;
                command.Parameters.Add("@Matricula", SqlDbType.Int).Value = perfil.Matricula;

               
                command.Parameters.Add("@NikName", SqlDbType.NVarChar).Value = perfil.NikName ?? (object)DBNull.Value;
                command.Parameters.Add("@Biografia", SqlDbType.NVarChar).Value = perfil.Biografia ?? (object)DBNull.Value;
                command.Parameters.Add("@IdPerfil", SqlDbType.Int).Value = perfil.IdPerfil;

                command.ExecuteNonQuery();
            }

            
        }


       
        public void AddPerfil(string Nikname)
        {
            throw new NotImplementedException("Este método está obsoleto, usa el repositorio de Cuentas.");
        }

        public void UpdatePerfil(string Nikname)
        {
            throw new NotImplementedException("Este método está obsoleto, usa ActualizarPerfil(Perfil perfil).");
        }


        
        private Perfil MapearPerfil(SqlDataReader reader)
        {
            return new Perfil
            {
                
                IdPerfil = (int)reader["IdPerfil"],
                NikName = reader["NikName"] as string,
                Biografia = reader["Biografia"] as string,

                
                Matricula = (int)reader["Matricula"],
                Nombre = reader["Nombre"] as string,
                Amaterno = reader["Amaterno"] as string,
                Apaterno = reader["Apaterno"] as string,
                Edad = (int)reader["Edad"],
                Genero = (reader["Genero"] as string)?[0] ?? '\0', 
                Carrera = reader["Carrera"] as string,
                Email = reader["Email"] as string

               
            };
        }
    }
}

