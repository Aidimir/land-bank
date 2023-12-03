namespace Logic.Interfaces
{
	public interface ILandService
	{
		public Task<IEnumerable<string>> FetchLandAssets();
		public Task DeleteAsset(int id);
		public Task<string> CreateAsset(string asset);
		public Task<string> UpdateAsset(int id, string asset);
	}
}

