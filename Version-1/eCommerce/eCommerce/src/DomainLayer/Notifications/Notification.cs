﻿using eCommerce.src.DataAccessLayer.DataTransferObjects.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Notifications
{
    public class Notification
    {
        // Properties
        public String Message { get; }
        public DateTime Date { get; }
        public Boolean isOpened { get; set; }
        public Boolean isStoreStaff { get; set; }
        public String ClientId { get; set; }


        public Notification(String clientId, String msg, Boolean staff)
        {
            this.Message = msg;
            this.Date = DateTime.Now;
            isOpened = false;
            isStoreStaff = staff;
            this.ClientId = clientId;
        }

        public Notification(string clientId, string msg, bool staff, bool isOpened, string date) 
        {
            this.Message =msg;
            this.Date = Convert.ToDateTime(date);
            this.isStoreStaff = staff;
            this.ClientId = clientId;
            this.isOpened = isOpened;
        }

        public String ToString()
        {
            return $"Date: {Date.ToString("MM/dd/yyyy HH:mm")}     {Message}";
        }
        
        public DTO_Notification getDTO()
        {
            return new DTO_Notification(Message, Date.ToString(), isOpened, isStoreStaff, ClientId);
        }
    }
}
