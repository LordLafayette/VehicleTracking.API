using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Core.Domains.VehicleProfiles
{
    public class VehicleHistory
    {
        public VehicleHistory(float lat, float lng, string locationName, float fule, float speed)
        {
            Lat = lat;
            Lng = lng;
            LocationName = locationName;
            Fule = fule;
            Speed = speed;

            CreatedDate = DateTime.Now;
        }

        public int Id { get; private set; }
        public int VehicleProfileId { get; private set; }
        public float Lat { get; private set; }
        public float Lng { get; private set; }
        public string LocationName { get; private set; }
        public float Fule { get; private set; }
        public float Speed { get; private set; }
        public DateTime CreatedDate { get; set; }

    }
}
