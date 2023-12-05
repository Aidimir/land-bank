using System;
using Dal.Models;

namespace Dal.Repositories.Interfaces
{
	public interface ILandRepository
	{
        public Task<IEnumerable<LandAsset>> FetchLandAssetsAsync(int? id = null,
            string? owner = null,
            string? fullname = null,
            LandType? type = null);
        public Task DeleteAssetAsync(int id);
        public Task<LandAsset> CreateAssetAsync(LandAsset asset);
        public Task<LandAsset> UpdateAssetAsync(int id, LandAsset asset);
    }
}