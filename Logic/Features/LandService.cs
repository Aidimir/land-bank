using System;
using Dal.Models;
using Dal.Repositories.Interfaces;
using Logic.Interfaces;
using Dal.Exceptions;

namespace Logic.Features
{
    public class LandService : ILandService
    {
        private readonly ILandRepository _db;

        public LandService(ILandRepository db)
        {
            _db = db;
        }

        public async Task<LandAsset> CreateAsset(LandAsset asset)
        {
            return await _db.CreateAssetAsync(asset);
        }

        public async Task DeleteAsset(int id)
        {
            await _db.DeleteAssetAsync(id);
        }

        public async Task<IEnumerable<LandAsset>> FetchLandAssets(int? id = null,
            string? owner = null,
            string? fullname = null,
            LandType? type = null)
        {
            return await _db.FetchLandAssetsAsync(id, owner, fullname, type);
        }

        public async Task<LandAsset> UpdateAsset(int id, LandAsset asset)
        {
            var existingAsset = (await _db.FetchLandAssetsAsync(id)).FirstOrDefault();
            if (existingAsset is null)
            {
                throw new NotFoundException("Не существует актива с таким id");
            }
            existingAsset.DealStage = asset.DealStage;
            existingAsset.Fullname = asset.Fullname;
            existingAsset.ObjectName = asset.ObjectName;
            existingAsset.Owner = asset.Owner;
            existingAsset.Type = asset.Type;
        
            return await _db.UpdateAssetAsync(id, existingAsset);
        }
    }
}

