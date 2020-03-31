using AutoMapper;
using DPEMoveDAL.Helper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Services
{
    public class StepService : IStepService
    {
        private readonly AppDbContext _context;

        public StepService(AppDbContext context)
        {
            _context = context;
        }

        public Step AddStep(Step model)
        {
            var q = model;

            _context.Add(q).State = EntityState.Added;
            _context.SaveChanges();

            return q;
        }
    }
}
