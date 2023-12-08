using Dal.Exceptions;
using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public class LandRepository : DbContext, ILandRepository
    {
        private DbSet<LandAsset> _landAssets { get; set; }

        public LandRepository(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LandAsset>()
                .Property(l => l.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();
        }

            public async Task<LandAsset> CreateAssetAsync(LandAsset asset)
        {
            var sameAssetsInDb = await _landAssets.FirstOrDefaultAsync(g => g.ObjectName == asset.ObjectName);

            if (sameAssetsInDb != null)
            {
                throw new ObjectAlreadyExistsException("Asset with this id is already in database");
            }

            await _landAssets.AddAsync(asset);
            await SaveChangesAsync();

            return await FetchAssetById(asset.Id);
        }

        public async Task DeleteAssetAsync(int id)
        {
            var neededAsset = await FetchAssetById(id);
            _landAssets.Remove(neededAsset);
            await SaveChangesAsync();
        }

        private async Task<LandAsset> FetchAssetById(int id)
        {
            var result = await _landAssets.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                throw new NotFoundException("Не найдено земельного актива с таким id");
            }

            return result;
        }

        public async Task<IEnumerable<LandAsset>> FetchLandAssetsAsync(int? id = null,
            string? owner = null,
            string? Fullname = null,
            LandType? type = null)
        {
            if (_landAssets.Count() == 0)
            {
                return new List<LandAsset>();
            }

            IQueryable<LandAsset> result = _landAssets;

            if (id is not null && id != 0)
            {
                var possibleResult = result.Where(c => c.Id == id).FirstOrDefault();
                if (possibleResult is not null)
                {
                    return new List<LandAsset> { possibleResult };
                }
            }

            if (!string.IsNullOrEmpty(owner))
            {
                result = result.Where(c => c.Owner.ToLower().Contains(owner.ToLower()));
            }

            if (!string.IsNullOrEmpty(Fullname))
            {
                result = result.Where(c => c.Fullname.ToLower().Contains(Fullname.ToLower()));
            }

            if (type is not null)
            {
                result = result.Where(c => c.Type == type);
            }

            return await result.ToListAsync();
        }

        public async Task<LandAsset> UpdateAssetAsync(int id, LandAsset asset)
        {
            _landAssets.Update(asset);
            await SaveChangesAsync();
            var existingAsset = await FetchAssetById(id);

            return existingAsset;
        }
    }
}

