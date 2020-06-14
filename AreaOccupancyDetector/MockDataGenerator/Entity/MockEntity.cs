using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockDataGenerator.Entity
{
    public class MockEntity
    {
        [JsonProperty("Employee Count")]
        public int EmployeeCount { get; set; }
        [JsonProperty("TimeStamp")]
        public DateTime TimeStamp { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("State")]
        public string State { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("DC")]
        public string DC { get; set; }
        [JsonProperty("Region")]
        public string Region { get; set; }
        [JsonProperty("Sub Region 1")]
        public string SubRegion1 { get; set; }
        [JsonProperty("Sub Region 2")]
        public string SubRegion2 { get; set; }
    }
}
