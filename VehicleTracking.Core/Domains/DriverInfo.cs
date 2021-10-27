using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Core.Domains.VehicleProfiles;
using VehicleTracking.Core.Exceptions;
using VehicleTracking.Core.Interfaces;

namespace VehicleTracking.Core.Domains
{
    public class DriverInfo : IAggregateRoot
    {
        public string IdentityId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime CreatedDate { get; set; }


        private List<VehicleProfile> _vehicleProfiles = new List<VehicleProfile>();
        public IReadOnlyCollection<VehicleProfile> VehicleProfiles => _vehicleProfiles.AsReadOnly();

        private DriverInfo()
        {

        }

        public DriverInfo(string id, string name, string lastName, string licenseNumber)
        {
            EnsureArg.IsNotNullOrWhiteSpace(id, nameof(id));
            EnsureArg.IsNotNullOrWhiteSpace(name, nameof(name));
            EnsureArg.IsNotNullOrWhiteSpace(lastName, nameof(lastName));
            EnsureArg.IsNotNullOrWhiteSpace(licenseNumber, nameof(licenseNumber));

            IdentityId = id;
            Name = name;
            LastName = lastName;
            LicenseNumber = licenseNumber;
            CreatedDate = DateTime.Now;
        }

        public VehicleProfile AddVehicle(string licensePlate, VehicleType vehicleType)
        {
            if(_vehicleProfiles.Any(a=>a.LicensePlate == licensePlate))
            {
                throw new DomainException($"Duplicate License Plate {licensePlate}");
            }

            var vehicle = new VehicleProfile(licensePlate, vehicleType);
            _vehicleProfiles.Add(vehicle);

            return vehicle;
        }
    }
}
