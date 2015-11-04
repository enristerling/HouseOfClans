using HouseOfClans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfClans.Managers
{
    public class RequestManager
    {
        /// <summary>
        /// Gets a Request based on the request id
        /// </summary>
        /// <param name="requestId">Request Id</param>
        public static Request SelectById(int requestId)
        {
            Request request = new Request();

            using (var dbContext = new HouseOfClansEntities())
            {
                request = dbContext.Requests.Where(p => p.id == requestId).FirstOrDefault();
            }

            return request;
        }

        /// <summary>
        /// Gets a Request based on the aspnetUserId
        /// </summary>
        /// <param name="requestId">Aspnet User Id</param>
        public static Request SelectByAspnetUserId(string aspnetUserId)
        {
            Request request = new Request();

            using (var dbContext = new HouseOfClansEntities())
            {
                request = dbContext.Requests.Where(p => p.aspnetUserId == aspnetUserId).FirstOrDefault();
            }

            return request;
        }

        /// <summary>
        /// Gets the list of unprocessed requests based on the clan id
        /// </summary>
        /// <param name="clanId">Clan Id</param>
        public static List<Request> SelectUnprocessedRequestsByClanId(int clanId)
        {
            List<Request> requests = new List<Request>();

            using (var dbContext = new HouseOfClansEntities())
            {
                requests = dbContext.Requests.Where(p => p.clanId == clanId && p.processed == false).ToList();
            }

            return requests;
        }

        /// <summary>
        /// Inserts a new Request record
        /// </summary>
        public static void Insert(Request req)
        {
            using (var dbContext = new HouseOfClansEntities())
            {
                req.addedOn = DateTime.Now;
                dbContext.Requests.Add(req);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a Request
        /// </summary>
        public static void Update(Request updatedReq)
        {
            Request newReq = RequestManager.SelectById(updatedReq.id);
            using (var dbContext = new HouseOfClansEntities())
            {
                updatedReq.updatedOn = DateTime.Now;
                dbContext.Requests.Attach(newReq);
                dbContext.Entry(newReq).CurrentValues.SetValues(updatedReq);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a Request
        /// </summary>
        public static bool Delete(int requestId)
        {
            bool isDeleted = false;
            Request deleteReq = RequestManager.SelectById(requestId);

            using (var dbContext = new HouseOfClansEntities())
            {
                dbContext.Requests.Attach(deleteReq);
                dbContext.Requests.Remove(deleteReq);
                isDeleted = dbContext.SaveChanges() > 0;
            }

            return isDeleted;
        }

    }
}