using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.src.ServiceLayer.Controllers
{
    public class SysInfoController
    {
        private static SysInfoController Instance = null;

        DateTime today;
        public int GuestUsers { get; set; }
        public int RegisteredUsers { get; set; }
        public int ManagersNotOwners { get; set; }
        public int Owners { get; set; }
        public int Admins { get; set; }

        public List<String> visitorsIDs { get; set; }

        private SysInfoController()
        {
            SystemInfo dto = DBUtil.getInstance().LoadSystemInfo();
            today = DateTime.Now.Date;
            GuestUsers = dto.GuestUsers;
            RegisteredUsers = dto.RegisteredUsers;
            ManagersNotOwners = dto.ManagersNotOwners;
            Owners = dto.Owners;
            Admins = dto.Admins;
            visitorsIDs = new List<string>();
        }

        public static SysInfoController getInstance()
        {
            if (Instance == null)
            {
                Instance = new SysInfoController();
            }
            return Instance;
        }

        public void updateForOpenStore(String fieldName, String userID)
        {
            if (DateTime.Now.Date > today)
            {
                today = DateTime.Now.Date;
                GuestUsers = 0;
                RegisteredUsers = 0;
                ManagersNotOwners = 0;
                Owners = 0;
                Admins = 0;
                visitorsIDs.Clear();
            }
            switch (fieldName)
            {
                case "RegisteredUsers":
                    Owners++;
                    if (RegisteredUsers > 0)
                        RegisteredUsers--;
                    break;

                case "ManagersNotOwners":
                    Owners++;
                    if (ManagersNotOwners > 0)
                        ManagersNotOwners--;
                    break;
            }

            String date = today.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            DBUtil.getInstance().Update(new SystemInfo(date, GuestUsers, RegisteredUsers, ManagersNotOwners, Owners, Admins));
        }

        public void update(String fieldName, String userID)
        {
            if (DateTime.Now.Date > today)
            {
                today = DateTime.Now.Date;
                GuestUsers = 0;
                RegisteredUsers = 0;
                ManagersNotOwners = 0;
                Owners = 0;
                Admins = 0;
                visitorsIDs.Clear();
            }

            if (visitorsIDs.Contains(userID) && !fieldName.Equals("GuestUsers"))
            {
                if (GuestUsers > 0)
                {
                    GuestUsers--;
                }
            }
            else
            {
                switch (fieldName)
                {
                    case "GuestUsers":
                        GuestUsers++;
                        break;

                    case "RegisteredUsers":
                        RegisteredUsers++;
                        if (GuestUsers > 0)
                            GuestUsers--;
                        break;

                    case "ManagersNotOwners":
                        ManagersNotOwners++;
                        if (GuestUsers > 0)
                            GuestUsers--;
                        break;


                    case "Owners":
                        Owners++;
                        if (GuestUsers > 0)
                            GuestUsers--;
                        break;


                    case "Admins":
                        Admins++;
                        if (GuestUsers > 0)
                            GuestUsers--;
                        break;
                }
                visitorsIDs.Add(userID);
            }

            //save in DB
            String date = today.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            DBUtil.getInstance().Update(new SystemInfo(date, GuestUsers, RegisteredUsers, ManagersNotOwners, Owners, Admins));
        }

        public Result<SystemInfo> getUserVisistsInDate(string date)
        {
            if(DateTime.TryParseExact(date,"yyyy-mm-dd", CultureInfo.InvariantCulture,DateTimeStyles.None, out _))
            {
                SystemInfo res = DBUtil.getInstance().LoadSystemInfoRecord(date);
                if (res == null)
                    return new Result<SystemInfo>("given date is not recorded to the system");
                return new Result<SystemInfo>(res);
            }
            return new Result<SystemInfo>("date format is not correct");
        }
    }
}
