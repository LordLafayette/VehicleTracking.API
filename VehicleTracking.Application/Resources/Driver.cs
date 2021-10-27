using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Core.Domains;
using VehicleTracking.Core.Domains.VehicleProfiles;

namespace VehicleTracking.Application.Resources
{
    public class DriverMapperProfile : Profile {
        public DriverMapperProfile()
        {
            CreateMap<DriverInfo, DriverInformationDto>();
            CreateMap<VehicleProfile, VehicleProfileDto>()
                .ForMember(d => d.VehicleType, o => o.MapFrom(s => s.VehicleType.ToString()));
            CreateMap<VehicleHistory, VehicleHistoryDto>();
        }
    }

    public class UpdateVehicleInformationRequest
    {
        public float? Lat { get; set; }
        public float? Lng { get; set; }
        public string Location { get; set; }
        public float Fule { get; set; }
        public float Speed { get; set; }

        public void Validate()
        {
            var validator = new UpdateVehicleInformationRequestValidator();
            validator.ValidateAndThrow(this);
        }

        class UpdateVehicleInformationRequestValidator : AbstractValidator<UpdateVehicleInformationRequest>
        {
            public UpdateVehicleInformationRequestValidator()
            {
                RuleFor(e => e.Lat).NotNull().GreaterThan(0);
                RuleFor(e => e.Lng).NotNull().GreaterThan(0);
            }
        }
    }

    public class DriverInformationDto
    {
        public string IdentityId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string LicenseNumber { get; set; }

    }

    public class VehicleProfileDto
    {
        public string DriverId { get; set; }
        public int Id { get; set; }
        public string VehicleType { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public float LastFule { get; set; }
        public float LastSpeed { get; set; }
        public string LastLocationName { get; set; }
        public string LicensePlate { get; set; }
    }

    public class VehicleHistoryDto
    {
        public int Id { get; private set; }
        public int VehicleProfileId { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public string LocationName { get; set; }
        public float Fule { get; set; }
        public float Speed { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
