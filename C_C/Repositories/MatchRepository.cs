using C_C.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Data;

namespace C_C.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly string _connectionString;

        public MatchRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        
        public bool CrearLike(int idPerfilEmisor, int idPerfilReceptor)
        {
            bool esMatch = false;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

              
                string mergeLikeQuery = @"
                    MERGE INTO Match AS T
                    USING (VALUES (@IdEmisor, @IdReceptor)) AS S (IdPerfilEmisor, IdPerfilReceptor)
                    ON T.IdPerfilEmisor = S.IdPerfilEmisor AND T.IdPerfilReceptor = S.IdPerfilReceptor
                    WHEN MATCHED THEN 
                        UPDATE SET EstadoMatch = 'Like', FechaMatch = GETDATE()
                    WHEN NOT MATCHED THEN
                        INSERT (IdPerfilEmisor, IdPerfilReceptor, FechaMatch, EstadoMatch)
                        VALUES (S.IdPerfilEmisor, S.IdPerfilReceptor, GETDATE(), 'Like');";

                using (var commandLike = new SqlCommand(mergeLikeQuery, connection))
                {
                    commandLike.Parameters.Add("@IdEmisor", SqlDbType.Int).Value = idPerfilEmisor;
                    commandLike.Parameters.Add("@IdReceptor", SqlDbType.Int).Value = idPerfilReceptor;
                    commandLike.ExecuteNonQuery();
                }

               
                string checkMatchQuery = @"
                    SELECT COUNT(1) 
                    FROM Match 
                    WHERE IdPerfilEmisor = @IdReceptor AND IdPerfilReceptor = @IdEmisor AND EstadoMatch = 'Like'";

                using (var commandMatch = new SqlCommand(checkMatchQuery, connection))
                {
                    commandMatch.Parameters.Add("@IdEmisor", SqlDbType.Int).Value = idPerfilEmisor;
                    commandMatch.Parameters.Add("@IdReceptor", SqlDbType.Int).Value = idPerfilReceptor;

                    
                    int likeDeVuelta = (int)commandMatch.ExecuteScalar();

                    if (likeDeVuelta > 0)
                    {
                        esMatch = true;

                        
                        string updateToMatchQuery = @"
                            UPDATE Match SET EstadoMatch = 'Match' 
                            WHERE (IdPerfilEmisor = @IdEmisor AND IdPerfilReceptor = @IdReceptor)
                               OR (IdPerfilEmisor = @IdReceptor AND IdPerfilReceptor = @IdEmisor)";

                        using (var commandUpdate = new SqlCommand(updateToMatchQuery, connection))
                        {
                            commandUpdate.Parameters.Add("@IdEmisor", SqlDbType.Int).Value = idPerfilEmisor;
                            commandUpdate.Parameters.Add("@IdReceptor", SqlDbType.Int).Value = idPerfilReceptor;
                            commandUpdate.ExecuteNonQuery();
                        }

                        
                    }
                }
            }

            return esMatch;
        }

      
    }
}

