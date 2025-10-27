using C_C.Model;
using C_C.Utils;
using Microsoft.Data.SqlClient; 
using System;
using System.Configuration; 
using System.Data;

namespace C_C.Repositories
{
    
    public class CuentaRepository : ICuentaRepository
    {
        private readonly string _connectionString;

        public CuentaRepository()
        {
            
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

       
        public void AddCuenta(Alumno alumno, string hashContrasena)
        {
            
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

               
                command.CommandText = "INSERT INTO Alumno (Matricula, Nombre, Edad, Genero, Carrera, Email) VALUES (@Matricula, @Nombre, @Edad, @Genero, @Carrera, @Email)";
                command.Parameters.Add("@Matricula", SqlDbType.Int).Value = alumno.Matricula;
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = alumno.Nombre;
                command.Parameters.Add("@Edad", SqlDbType.Int).Value = alumno.Edad;
                command.Parameters.Add("@Genero", SqlDbType.Char, 1).Value = alumno.Genero;
                command.Parameters.Add("@Carrera", SqlDbType.NVarChar).Value = alumno.Carrera;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = alumno.Email;
                command.ExecuteNonQuery(); 

                
                command.Parameters.Clear(); 
                command.CommandText = "INSERT INTO Cuenta (HashContrasena, Matricula, FechaRegistro, EstadoCuenta) VALUES (@Hash, @Matricula, @Fecha, 'Activa')";
                command.Parameters.Add("@Hash", SqlDbType.NVarChar).Value = hashContrasena;
                command.Parameters.Add("@Matricula", SqlDbType.Int).Value = alumno.Matricula;
                command.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = DateTime.Now;
                command.ExecuteNonQuery(); 
            }
        }

        public bool AutenticateCuenta(int matricula, string contrasena)
        {
            bool esValido = false;
            string hashContrasenaGuardado = "";

          
            string hashContrasenaIngresado = HashUtil.ComputeSha256Hash(contrasena);

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT HashContrasena FROM Cuenta WHERE Matricula = @Matricula AND EstadoCuenta = 'Activa'";
                command.Parameters.Add("@Matricula", SqlDbType.Int).Value = matricula;

               
                var resultado = command.ExecuteScalar();

                if (resultado != null)
                {
                    hashContrasenaGuardado = resultado.ToString();
                }

               
                if (hashContrasenaGuardado == hashContrasenaIngresado)
                {
                    esValido = true; 
                }
            }

            return esValido;
        }

       
        public void DeleteCuenta(int Matricula)
        {
            throw new NotImplementedException();
        }

        public void EditContraseña(int Matricula, string nuevaContraseña)
        {
            throw new NotImplementedException();
        }
    }
}

