

using C_C.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks; 

namespace C_C.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly string _connectionString;

        public ChatRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

   
        public async Task<List<ChatPreview>> ObtenerChatsAsync(int idPerfilUsuario)
        {
            var listaChats = new List<ChatPreview>();

           
            listaChats = ObtenerChatsDePrueba();

           
            return await Task.FromResult(listaChats);
        }

        
        public async Task<List<Mensaje>> ObtenerMensajesAsync(int idChat)
        {
            var listaMensajes = new List<Mensaje>();
            string query = "SELECT * FROM Mensaje WHERE IdChat = @IdChat ORDER BY Fecha ASC";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    command.Parameters.Add("@IdChat", SqlDbType.Int).Value = idChat;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            listaMensajes.Add(new Mensaje
                            {
                                IdMensaje = (int)reader["IdMensaje"],
                                IdChat = (int)reader["IdChat"],
                                IdPerfilRemitente = (int)reader["IdPerfilRemitente"],
                                Texto = reader["Texto"].ToString(),
                                Fecha = (DateTime)reader["Fecha"],
                                ConfirmacionLectura = (bool)reader["ConfirmacionLectura"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar mensajes: " + ex.Message);
            }

            return listaMensajes;
        }

        
        public async Task EnviarMensajeAsync(Mensaje nuevoMensaje)
        {
            string query = @"
                INSERT INTO Mensaje (IdChat, IdPerfilRemitente, Texto, Fecha, ConfirmacionLectura) 
                VALUES (@IdChat, @IdPerfilRemitente, @Texto, @Fecha, 0)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                await connection.OpenAsync();
                command.Parameters.Add("@IdChat", SqlDbType.Int).Value = nuevoMensaje.IdChat;
                command.Parameters.Add("@IdPerfilRemitente", SqlDbType.Int).Value = nuevoMensaje.IdPerfilRemitente;
                command.Parameters.Add("@Texto", SqlDbType.NVarChar).Value = nuevoMensaje.Texto;
                command.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = nuevoMensaje.Fecha;

                await command.ExecuteNonQueryAsync();
            }
        }

        
        private List<ChatPreview> ObtenerChatsDePrueba()
        {
            return new List<ChatPreview>
            {
                new ChatPreview { IdChat = 1, IdOtroPerfil = 10, Nombre = "Armin Arlert (Prueba)", FotoUrl = "https://placehold.co/100x100/A0A/FFF?text=Armin", UltimoMensaje = "¡Hola! ¿También te gusta la historia?" },
                new ChatPreview { IdChat = 2, IdOtroPerfil = 11, Nombre = "Eren Jaeger (Prueba)", FotoUrl = "https://placehold.co/100x100/F0A/FFF?text=Eren", UltimoMensaje = "Nos vemos en la biblioteca." }
            };
        }
    }
}