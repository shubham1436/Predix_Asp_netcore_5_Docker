using AspnetCorePostgres.Models;
using AspnetCorePostgres.Postgres.POCO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCorePostgres.Repositories
{
    public class PartFamilyRepository
    {
        private readonly PartFamilyContext _context;

        public PartFamilyRepository(PartFamilyContext context)
        {
            //        connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            _context = context;
        }

        //internal IDbConnection Connection
        //{
        //    get
        //    {
        //        return new NpgsqlConnection(connectionString);
        //    }
        //}

        public void Add(PartFamily item)
        {
            //using (IDbConnection dbConnection = Connection)
            //{
            //    dbConnection.Open();
            //    dbConnection.Execute("INSERT INTO customer (name,phone,email,address) VALUES(@Name,@Phone,@Email,@Address)", item);
            //}

            try
            {
                _context.PartFamily.Add(new PartFamily()
                {
                    PartFamilyID = item.PartFamilyID,
                  PartFamilyName=item.PartFamilyName,
                  Description=item.Description,
                  IsDeleted=item.IsDeleted
                });
                _context.SaveChanges();

                var added = _context.PartFamily.FirstOrDefault(x => x.PartFamilyID == item.PartFamilyID);
              //  return new JsonResult(string.Format("Inserted part family: ID : {0} ;  Name: {1}", added.PartFamilyID, added.PartFamilyName));
            }
            catch (Exception ex)
            {
                //return new JsonResult(string.Format("Error: {0}\r\n {1}", ex.Message, ex.InnerException));
            }
        }

        public IEnumerable<PartFamily> FindAll()
        {
            //using (IDbConnection dbConnection = Connection)
            //{
            //    dbConnection.Open();
            //    return dbConnection.Query<Customer>("SELECT * FROM customer");
            //}

            try
            {


                var data = _context.PartFamily.Where(i => i.IsDeleted == 0).ToList();//.FirstOrDefault(x => x.PartFamilyID == 1);
                return data;// data.PartFamilyName;
                //  return new JsonResult(string.Format("Inserted part family: ID : {0} ;  Name: {1}", added.PartFamilyID, added.PartFamilyName));
            }
            catch (Exception ex)
            {
                //error = ex.Message;
                //return new JsonResult(string.Format("Error: {0}\r\n {1}", ex.Message, ex.InnerException));
            }

            return null;
        }

        public PartFamily FindByID(int id)
        {
            //using (IDbConnection dbConnection = Connection)
            //{
            //    dbConnection.Open();
            //    return dbConnection.Query<Customer>("SELECT * FROM customer WHERE id = @Id", new { Id = id }).FirstOrDefault();
            //}
            var part = _context.PartFamily.FirstOrDefault(i => i.PartFamilyID == id);
            return part;
        }

        public void Remove(int id)
        {
            var part = _context.PartFamily.FirstOrDefault(i => i.PartFamilyID == id);
            if(part != null)
            {
                part.IsDeleted = 1;
            }
            _context.SaveChanges();
            //using (IDbConnection dbConnection = Connection)
            //{
            //    dbConnection.Open();
            //    dbConnection.Execute("DELETE FROM customer WHERE Id=@Id", new { Id = id });
            //}
        }

        public void Update(PartFamily item)
        {
            _context.PartFamily.Update(item);
            _context.SaveChanges();

            //using (IDbConnection dbConnection = Connection)
            //{
            //    dbConnection.Open();
            //    dbConnection.Query("UPDATE customer SET name = @Name,  phone  = @Phone, email= @Email, address= @Address WHERE id = @Id", item);
            //}
        }
    }
}

