using CWB.CommonUtils.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CWB.App.Models.BusinessProcesses
{
    public class DeliveryScheduleVM 
    {
        DateTime? requiredByDate = null;
        public int ScheduleId { get; set; }
        public long CustomerOrderId { get; set; }

        [Required]
        [Remote(
           "CheckPartNo",
           "BusinessAquisition",
           AdditionalFields = "DSPartId",
           ErrorMessage = "{0} already exists. Please enter a different {0}.",
           HttpMethod = "GET"
       )]
        public long DSPartId { get; set; }

        public string? PartNo { get; set; }

        public int RequiredQuantity { get; set; }
        public DateTime? RequiredByDate { 
            get { return requiredByDate; }
            set { requiredByDate = value; } 
        }

        public String RequiredByDateStr {
            get { 
                if(requiredByDate == null)
                {
                    return "";
                }
                return requiredByDate.Value.ToString("dd-MM-yyyy"); 
            }
            set { }
        }

        public string? Comment { get; set; }
        public int Status { get; set; }
        public long TenantId { get; set; }
  
    }
}
