using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAtelier.DB
{
    abstract class FactoryDB <T>: DBConnect
    {
        /// <summary>
        /// Converty the SqlDataReader to class
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public abstract T ConvertSqlDataReaderToClass(SqlDataReader dr);

        /// <summary>
        /// Return all elements in database
        /// </summary>
        /// <returns></returns>
        public abstract List<T> SelectAllElement();

        /// <summary>
        /// Return element by id
        /// </summary>
        /// <returns></returns>
        public abstract T SelectElementById(int ElementId);
    }
}
