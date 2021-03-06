using DogGo.Interfaces;
using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IConfiguration _config;
        public WalksRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walks> GetAllWalks()
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select w.Date, w.Duration,w.id, w.walkerId, w.dogid, d.Name as DogName, o.Name
                    from Walks w left join dog d on w.DogId = d.Id
                    join Owner o on o.Id = d.OwnerId 
                    ;";


                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Walks> walks = new List<Walks>();
                    while (reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogID")),
                            Client = new Owner
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }

                        };

                        walks.Add(walk);
                    }
                    reader.Close();

                    return walks;
                }
            }
        }


        public List<Walks> GetWalksByWalkerId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Select w.Id, w.Date, W.Duration, w.walkerId, w.dogId,  
                        Owner.name 
                        FROM Walks w
                        left join dog on w.DogId = dog.Id
                        Left Join Owner on Dog.OwnerId = Owner.Id
                        WHERE w.walkerId = @id
                        order by owner.name;
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walks> walks = new List<Walks>();
                    while (reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogID")),
                            Client = new Owner
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }
                        };

                     walks.Add(walk);
                    }
                    reader.Close();
                    return walks;
                    
                }
            }
        }

        public Walker GetWalkerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT w.Id, w.[Name], w.ImageUrl, 
                    n.Name as Neighborhood
                    FROM Walker w
                    Left Join Neighborhood n on w.NeighborhoodId = n.id
                    WHERE w.Id =  @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            Neighborhood = new Neighborhood
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                NeighborhoodName = reader.GetString(reader.GetOrdinal("Name"))

                            }

                        };

                        reader.Close();
                        return walker;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
    }
}
