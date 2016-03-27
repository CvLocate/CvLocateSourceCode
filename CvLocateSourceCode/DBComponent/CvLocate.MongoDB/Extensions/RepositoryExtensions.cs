using CvLocate.MongoDB.Entities;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.MongoDB
{
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Get automatic next id by FriendlyId
        /// </summary>
        /// <typeparam name="T">type of repository entities</typeparam>
        /// <param name="repository">the repository</param>
        /// <returns>the next id</returns>
        public static int GetNextId<T>(this MongoRepository<T> repository) 
            where T:BaseMongoEntity
        {
            var ordered = repository.OrderByDescending(entity => entity.FriendlyId).ToList();
            if (ordered == null || ordered.Count == 0)
                return 1;
            int lastId = ordered.First().FriendlyId;
            return lastId + 1;
        }
    }
}
