﻿using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Repositories.Hobbies
{
    public class HobbyRepository(AppDbContext context) : IHobbyRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<ICollection<Hobby>> GetHobbiesAsync()
        {
            return await _context.Hobbies.ToListAsync();
        }

        public async Task<Hobby?> GetHobbyAsync(int id)
        {
            return await _context.Hobbies.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Hobby?> GetHobbyAsync(string name)
        {
            return await _context.Hobbies.FirstOrDefaultAsync(h => h.Name == name);
        }
    }
}
