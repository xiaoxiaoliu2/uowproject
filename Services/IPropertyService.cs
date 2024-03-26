using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using uowpublic.Models;
using uowpublic.Data;

namespace uowpublic.Services
{
    public interface IPropertyService
    {
        Task CreateAsync(Property newProperty);
        Task<List<Property>> GetAsync();
        Task<Property?> GetAsync(int id);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Property updatedProperty);
    }

    public class PropertyService : IPropertyService
    {
        private readonly DatabaseContext _context;

        public PropertyService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Property newProperty)
        {
            _context.Property.Add(newProperty);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Property>> GetAsync()
        {
            return await _context.Property.ToListAsync();
        }

        public async Task<Property?> GetAsync(int id)
        {
            return await _context.Property.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            var property = await _context.Property.FindAsync(id);
            if (property != null)
            {
                _context.Property.Remove(property);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Property updatedProperty)
        {
            var property = await _context.Property.FindAsync(id);
            if (property != null)
            {
                property.Publisher_Id = updatedProperty.Publisher_Id;
                property.Address = updatedProperty.Address;
                property.Type = updatedProperty.Type;
                property.Rent = updatedProperty.Rent;
                property.Facilitity = updatedProperty.Facilitity;
                property.Description = updatedProperty.Description;
                property.Attribute = updatedProperty.Attribute;
                property.Longitude = updatedProperty.Longitude;
                property.Latitude = updatedProperty.Latitude;
                property.IsDeleted = updatedProperty.IsDeleted;
                
                await _context.SaveChangesAsync();
            }
        }
    }
}
