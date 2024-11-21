using System;

namespace CWB.App.Models.Plants
{
    public class HolidayVM
    {
        public long HolidayId { get; set; }
        public long PlantId { get; set; }
        public string Name { get; set; }
        DateTime? poDate = null;
        public DateTime? HolidayDate
        {
            get { return poDate; }
            set { poDate = value; }
        }
        public string HolidayDateStr
        {
            get
            {
                if (poDate == null)
                {
                    return "";
                }
                return poDate.Value.ToString("dd-MM-yyyy");
            }
            set { }
        }

        public string Day
        {
            get
            {
                if (poDate == null)
                {
                    return "";
                }
                return poDate.Value.DayOfWeek.ToString();
            }
            set { }
        }
        public long TenantId { get; set; }
    }
}
