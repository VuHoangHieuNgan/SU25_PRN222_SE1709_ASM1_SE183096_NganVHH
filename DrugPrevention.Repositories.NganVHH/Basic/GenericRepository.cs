using DrugPrevention.Repositories.NganVHH.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPrevention.Repositories.NganVHH.Basic
{
    public class GenericRepository<T> where T : class
    {
        protected SU25_PRN222_SE1709_G2_DrugPreventionSystemContext _context;

        public GenericRepository()
        {
            _context ??= new SU25_PRN222_SE1709_G2_DrugPreventionSystemContext();
        }

        public GenericRepository(SU25_PRN222_SE1709_G2_DrugPreventionSystemContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            //// Turning off Tracking for UpdateAsync in Entity Framework
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            //// Turning off Tracking for UpdateAsync in Entity Framework
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            
            // Explicitly mark all properties as modified to ensure all changes are saved
            foreach (var property in tracker.Properties)
            {
                if (!property.Metadata.IsPrimaryKey())
                {
                    property.IsModified = true;
                }
            }
            
            return await _context.SaveChangesAsync();
        }

        //public async Task<int> UpdateAsync(T entity)
        //{
        //    try
        //    {
        //        // Clear tracking trước
        //        _context.ChangeTracker.Clear();

        //        // Attach entity
        //        var entry = _context.Attach(entity);

        //        // Đánh dấu tất cả properties là Modified, NGOẠI TRỪ key
        //        foreach (var property in entry.Properties)
        //        {
        //            if (!property.Metadata.IsPrimaryKey())
        //            {
        //                property.IsModified = true;
        //            }
        //        }

        //        return await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error updating entity: {ex.Message}");
        //    }
        //}

        public bool Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T GetById(string code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(string code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        /*
        https://guidgenerator.com/
        daacb4fb-ff73-46ef-98f1-4af9aab2a30a
         */
        public T GetById(Guid code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(Guid code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        #region Separating asigned entity and save operators        

        public void PrepareCreate(T entity)
        {
            _context.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _context.Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Separating asign entity and save operators
    }
}
