﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class TrainerService : GenericService<Trainer>, ITrainerService
    {
        public TrainerService(IRepository<Trainer> repository)
            : base(repository)
        {
        }

        public async Task<Trainer> AddTrainer(Trainer trainer)
        {
            await Add(trainer);
            return trainer;
        }

        public async Task<List<Trainer>> GetTrainersBySpecialization(string specialization)
        { var allTrainers = await GetAll();
            return allTrainers.Where(x  => x.Specialization == specialization).ToList();
        }

        public async Task<List<Trainer>> GetAvailableTrainers(DateTime date)
        {
            var allTrainers = await GetAll();
            return allTrainers.Where(x => x.AvailableDates.Contains(date)).ToList();
        }

        public async Task<bool> CheckTrainerAvailability(Guid trainerId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var trainer = await GetById(trainerId);
            return trainer.AvailableDates.Where(x  => x.Date == date && x.TimeOfDay >= startTime && x.TimeOfDay <= endTime).Any();


        }
    }
}