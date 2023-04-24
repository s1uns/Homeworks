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
        public async Task<bool> CheckMemberAttendance(Guid memberId, DateTime date)
        {
            var allMembers = await _memberService.GetAll();
            var member = allMembers.Where(x => x.Id == memberId).First();
            var allClasses = await GetAll();
            var searchedClass = allClasses.Where(x => x.Date == date).First();
            return searchedClass.Attendees.Contains(member);

        }

        public async Task RecordMemberAttendance(Guid memberId, DateTime date)
        {
            {
                var allMembers = await _memberService.GetAll();
                var member = allMembers.Where(x => x.Id == memberId).First();
                var allClasses = await GetAll();
                var searchedClass = allClasses.Where(x => x.Date == date).First();
                searchedClass.Attendees.Add(member);
            }
        }

        public async Task AssignTrainerToClass(Guid trainerId, Guid classId)
        {
            var allTrainers = await _trainerService.GetAll();
            var trainer = allTrainers.Where(x => x.Id == trainerId).First();
            var searchedClass = await GetById(classId);
            searchedClass.Trainer = trainer;
            await Update(classId, searchedClass);

        }
    }
}