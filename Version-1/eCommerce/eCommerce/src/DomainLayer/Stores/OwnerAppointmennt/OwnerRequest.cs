using eCommerce.src.DataAccessLayer.DataTransferObjects.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.src.DomainLayer.Stores.OwnerAppointmennt
{
    public class OwnerRequest
    {
        public string Id { get; set; }
        public string UserID { get; }
        public string StoreID { get; }
        public string AppointedBy { get; set; }
        public List<string> acceptedOwners { get; }

        
        public OwnerRequest(string userID, string storeID, string appointedBy, string id = "", List<string> acceptedOwners = null)
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
            this.UserID = userID;
            this.StoreID = storeID;
            this.acceptedOwners = acceptedOwners;
            AppointedBy = appointedBy;
            if (acceptedOwners == null)
                this.acceptedOwners = new List<string>();
        }

        public DTO_OwnerRequest getDTO()
        {
            return new DTO_OwnerRequest(this.Id, this.UserID, this.StoreID,this.AppointedBy,  this.acceptedOwners);
        }



        private bool didAllOwnersAccept(List<string> allOwners)
        {
            foreach (string id in allOwners)
                if (!acceptedOwners.Contains(id))
                    return false;
            return true;
        }

        public OwnerRequestResponse AcceptedResponse(string ownerID, List<string> allOwners)
        {
            if (acceptedOwners.Contains(ownerID))
                throw new Exception("Failed to response to an offer: Owner Can't accept an offer more than once");
            acceptedOwners.Add(ownerID);
            if (didAllOwnersAccept(allOwners))
                return OwnerRequestResponse.Accepted;
            else
                return OwnerRequestResponse.None;
        }

        public Dictionary<string, string> GetData()
        {
            Dictionary<string, string> data = new Dictionary<string,string>() {
                { "Id", Id },
                { "User", UserID },
                { "Store", StoreID },
                { "AppointedBy", AppointedBy }
            };
            return data;
        }
    }
}
