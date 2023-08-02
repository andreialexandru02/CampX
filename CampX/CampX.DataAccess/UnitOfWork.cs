using CampX.Common;
using CampX.Context;
using CampX.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CampX.DataAccess
{
    public class UnitOfWork
    {

        private readonly CampXContext Context;

        public UnitOfWork(CampXContext context)
        {
            Context = context;
        }

        private IRepository<Badge> badges;
        public IRepository<Badge> Badges => badges ?? (badges = new BaseRepository<Badge>(Context));


        private IRepository<Camper> campers;
        public IRepository<Camper> Campers => campers ?? (campers = new BaseRepository<Camper>(Context));

        
        private IRepository<CamperBadge> camperBadges;
        public IRepository<CamperBadge> CamperBadges => camperBadges ?? (camperBadges = new BaseRepository<CamperBadge>(Context));

       
        private IRepository<Campsite> campsites;
        public IRepository<Campsite> Campsites => campsites ?? (campsites = new BaseRepository<Campsite>(Context));
       
        
        private IRepository<Equipment> equipment; 
        public IRepository<Equipment> Equipment => equipment ?? (equipment = new BaseRepository<Equipment>(Context));


        private IRepository<EquipmentCamperTrip> equipmentCamperTrips;
        public IRepository<EquipmentCamperTrip> EquipmentCamperTrips => equipmentCamperTrips ?? (equipmentCamperTrips = new BaseRepository<EquipmentCamperTrip>(Context));

        
        private IRepository<Image> images;
        public IRepository<Image> Images => images ?? (images = new BaseRepository<Image>(Context));

        
        private IRepository<Note> notes;
        public IRepository<Note> Notes => notes ?? (notes = new BaseRepository<Note>(Context));


        private IRepository<Request> requests;
        public IRepository<Request> Requests => requests ?? (requests = new BaseRepository<Request>(Context));

        
        private IRepository<Review> reviews;
        public IRepository<Review> Reviews => reviews ?? (reviews = new BaseRepository<Review>(Context));


        private IRepository<Role> roles;
        public IRepository<Role> Roles=> roles ?? (roles = new BaseRepository<Role>(Context));


        private IRepository<Trip> trips;
        public IRepository<Trip> Trips=> trips ?? (trips = new BaseRepository<Trip>(Context));

        private IRepository<TripCamper> tripCampers;
        public IRepository<TripCamper> TripCampers => tripCampers ?? (tripCampers = new BaseRepository<TripCamper>(Context));

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}



