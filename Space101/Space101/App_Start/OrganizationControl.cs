using AutoMapper;
using Space101.Dtos;
using Space101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<RaceClassification, RaceClassificationsDto>();
            CreateMap<RaceClassificationsDto, RaceClassification>();
            CreateMap<Planet, AvailableDestinationDto>();
            CreateMap<PlanetDetail, PlanetDetailDto>();
            CreateMap<SurfaceMorphology, SurfaceMorphologyDto>();
            CreateMap<ClimateZone, ClimateZoneDto>();
            CreateMap<Terrain, TerrainDto>();
            CreateMap<Climate, ClimateDto>();
            CreateMap<Race, RaceDto>();
            CreateMap<RaceHabitat, RaceHabitatPLANETDto>();
            CreateMap<RaceHabitat, RaceHabitatRACEDto>();
            CreateMap<Planet, PlanetDto>();
            CreateMap<Race, RaceDto>();
            CreateMap<Race, RacePLANETDto>();
            CreateMap<RaceFile, RaceFileDto>();
            CreateMap<FilePath, FilePathDto>();
            CreateMap<Flight, FlightDto>();
            CreateMap<Planet, PlanetFlightDto>();
            CreateMap<FlightPath, FlightPathDto>();
            CreateMap<FlightStatus, FlightStatusDto>();
            CreateMap<Starship, StarshipDto>();
            CreateMap<UserFavorite, UserFavoriteDto>();
        }
    }
}