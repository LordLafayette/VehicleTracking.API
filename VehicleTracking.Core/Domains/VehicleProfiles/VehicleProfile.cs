using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Core.Interfaces;

namespace VehicleTracking.Core.Domains.VehicleProfiles
{
    public class VehicleProfile : IAggregateRoot
    {
        public string DriverId { get; private set; }
        public int Id { get; private set; }
        public string LicensePlate { get; private set; }
        public VehicleType VehicleType { get; private set; }
        public float Lat { get; private set; }
        public float Lng { get; private set; }
        public float LastFule { get; private set; }
        public float LastSpeed { get; private set; }
        public string LastLocationName { get; private set; }
        public DateTime? LastUpdateDate { get; private set; }

        private List<VehicleHistory> _vehicleHistories = new List<VehicleHistory>();
        public IReadOnlyCollection<VehicleHistory> VehicleHistories => _vehicleHistories.AsReadOnly();
        public DriverInfo DriverInfo { get; private set; }

        private VehicleProfile()
        {

        }

        public VehicleProfile(string licensePlate, VehicleType vehicleType)
        {
            EnsureArg.IsNotNullOrWhiteSpace(licensePlate, nameof(licensePlate));

            LicensePlate = licensePlate;
            VehicleType = vehicleType;
        }

        public void UpdateStatus(float lat, float lng, float fule, float speed, string locationName)
        {
            var history = new VehicleHistory(lat, lng, locationName,fule,speed);
            _vehicleHistories.Add(history);

            Lat = lat;
            Lng = lng;
            LastFule = fule;
            LastSpeed = speed;
            LastLocationName=locationName;

            LastUpdateDate = DateTime.Now;
        }
    }
}
