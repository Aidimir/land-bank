using System.ComponentModel.DataAnnotations;
using Dal.Models;
namespace Logic.Interfaces
{
	public interface ILandService
	{
		public Task<IEnumerable<LandAsset>> FetchLandAssets(int? id = null,
			string? owner = null,
			string? Fullname = null,
			LandType? type = null);

		[Required]
		[EnumDataType(typeof(DealStageEnum), ErrorMessage = "Недопустимое значение для статуса")]
        public DealStageEnum DealStage { get; set; }
        public Task DeleteAsset(int id);
		public Task<LandAsset> CreateAsset(LandAsset asset);
		public Task<LandAsset> UpdateAsset(int id, LandAsset asset);
	}
}