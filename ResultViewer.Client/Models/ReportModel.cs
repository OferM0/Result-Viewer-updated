namespace ResultViewer.Client.Models
{
    // This interface defines the contract for the report model operations
    public interface IReportModel
    {
        // Get all lot runs with sorting options
        Task<List<IGetLotRuns_AllLotRuns>> GetAllLotRuns(List<LotRunSortInput> sortOptions);

        // Get a recipe run by its ID
        Task<IGetRecipeById_RecipeRunById> GetRecipeRunById(Guid recipeId);

        // Get all measurements for a specific lot ID
        Task<List<IGetMeasurementsByLotId_AllMeasurements>> GetAllMeasurementsByLotId(Guid lotId);
    }

    // This class implements the IReportModel interface
    public class ReportModel : IReportModel
    {
        private RVClient _rvClient;

        // Constructor injecting RVClient dependency
        public ReportModel(RVClient rvClient)
        {
            _rvClient = rvClient;
        }

        // Implementation of GetAllLotRuns method
        public async Task<List<IGetLotRuns_AllLotRuns>> GetAllLotRuns(List<LotRunSortInput> sortOptions)
        {
            // Execute the RVClient method to fetch lot runs with specified sorting options
            var result = await _rvClient.GetLotRuns.ExecuteAsync(sortOptions);
            return result.Data.AllLotRuns.ToList();
        }

        // Implementation of GetRecipeRunById method
        public async Task<IGetRecipeById_RecipeRunById> GetRecipeRunById(Guid recipeId)
        {
            // Execute the RVClient method to fetch a recipe run by its ID
            var result = await _rvClient.GetRecipeById.ExecuteAsync(recipeId);
            return result.Data.RecipeRunById;
        }

        // Implementation of GetAllMeasurementsByLotId method
        public async Task<List<IGetMeasurementsByLotId_AllMeasurements>> GetAllMeasurementsByLotId(Guid lotId)
        {
            // Execute the RVClient method to fetch all measurements for a lot ID
            var result = await _rvClient.GetMeasurementsByLotId.ExecuteAsync(lotId);
            return result.Data.AllMeasurements.ToList();
        }
    }
}
