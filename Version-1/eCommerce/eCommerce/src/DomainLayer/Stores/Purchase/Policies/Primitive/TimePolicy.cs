using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Purchase.Policies.Primitive
{
    internal class TimePolicy : PrimitivePolicy
    {
        public DateTime StartRestrict { get; set; }
        public DateTime EndRestrict { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public IPurchasePolicy Policy { get; set; }

        public TimePolicy(DateTime startRestrict, DateTime endRestrict, IPurchasePolicy policy, string id = "") : base(id)
        {
            if (policy == null)
                throw new Exception("Policy can't be null in Time Policy!");
            StartRestrict = startRestrict;
            EndRestrict = endRestrict;
            Policy = policy;
            Hour = -1;
            Minute = -1;
        }

        public TimePolicy(int hour, int minute, IPurchasePolicy policy, string id = "") : base(id)
        {
            this.Hour = hour;
            this.Minute = Minute;
            Policy = policy;
        }
        public override PrimitivePolicy Create(Dictionary<string, object> info, IPurchasePolicy policy)
        {
            if (!info.ContainsKey("StartRestrict") && !info.ContainsKey("EndRestrict"))
                throw new Exception("StartRestrict and EndRestrict not found!");

            DateTime startRestrict = createDateTime((JsonElement)info["StartRestrict"]);
            DateTime endRestrict = createDateTime((JsonElement)info["EndRestrict"]);

            if ((info.ContainsKey("Hour") && info.ContainsKey("Minute")) && (info.ContainsKey("StartRestrict") || info.ContainsKey("EndRestrict")))
                throw new Exception("(Both StartRestrict and EndRestrict) or (Both Hour and Minute) not Both!");

            int hour = ((JsonElement)info["Hour"]).GetInt32();
            int minute = ((JsonElement)info["Minute"]).GetInt32();

            return new TimePolicy(startRestrict, endRestrict, policy);

        }

        private static DateTime createDateTime(JsonElement timeElement)
        {
            String timeString = timeElement.GetString();
            DateTime time = DateTime.ParseExact(timeString, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            return time;
        }

        public override bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            if (Hour == -1 && Minute == -1)
            {
                DateTime now = DateTime.Now;
                return (now >= StartRestrict && now <= EndRestrict) && Policy.IsSatisfiedCond(bag, user);
            }
            else
            {
                DateTime now = DateTime.Now;
                DateTime limit = new DateTime(now.Year, now.Month, now.Day, Hour, Minute, 0);
                return now <= limit;
            }
        }
    }
}
