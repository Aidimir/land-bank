using System;
using Dal.Models;
using Dal.Repositories.Interfaces;
using Logic.Interfaces;

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
            return await _db.UpdateAssetAsync(id, asset);
        }
    }
}

