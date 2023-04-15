using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class ClassService : GenericService<FitnessClass>, IClassService
    {
        private readonly ITrainerService _trainerService;
        private readonly IMemberService _memberService;

        public ClassService(IRepository<FitnessClass> repository, ITrainerService trainerService, IMemberService memberService)
            : base(repository)
        {
            _trainerService = trainerService;
            _memberService = memberService;
        }

        public async Task<FitnessClass> ScheduleClass(FitnessClass fitnessClass)
        {
            await Add(fitnessClass);
            return fitnessClass;

        }

        public async Task<List<FitnessClass>> GetClassesByDate(DateTime date)
        {
            var classes = await GetAll();
            return classes.Where(x => x.Date == date).ToList();
        }

        public async Task<List<FitnessClass>> GetClassesByType(string classType)
        {
            var classes = await GetAll();
            return classes.Where(x => x.Type == classType).ToList();
        }

        public async Task<List<FitnessClass>> GetClassesByTrainer(Guid trainerId)
        {
            var trainer = await _trainerService.GetById(trainerId);
            var classes = await GetAll();
            return classes.Where(x => x.Trainer == trainer).ToList();
        }

        public async Task AddAttendeeToClass(Guid classId, Guid memberId)
        {
            var fitnessClass = await GetById(classId);
            var member = await _memberService.GetById(memberId);
            fitnessClass.Attendees.Add(member);
        }
    }
}