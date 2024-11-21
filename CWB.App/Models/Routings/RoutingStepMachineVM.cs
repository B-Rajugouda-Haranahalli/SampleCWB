using CWB.App.AppUtils;
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CWB.App.Models.Routing
{
    public class RoutingStepMachineVM
    {
        public long RoutingStepMachineId { get; set; }
        public long TenantId { get; set; }
        public long MachineId { get; set; }
        public long RoutingStepId { get; set; }
        public long OrigStepMachineId { get; set; } = 0;

        //[JsonConverter(typeof(TimeSpanToStringConverter))]
        public string SetupTime { get; set; }
        //[JsonConverter(typeof(TimeSpanToStringConverter))]
        public string FloorToFloorTime { get; set; }
        //[JsonConverter(typeof(TimeSpanToStringConverter))]
        public string FirstPieceProcessingTime { get; set; }
        public int NoOfPartsPerLoading { get; set; }

        public int PreferredMachine { get; set; }
        public string StrPreferredMachine { get; set; } = string.Empty;
        public string BGColor { get; set; }
        public string Name { get; set; } = "NA";
        public string MandocAvl { get; set; } = string.Empty;
    }
}
