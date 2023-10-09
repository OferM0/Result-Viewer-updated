namespace ResultViewer.Client.Models
{
    // This interface defines the contract for the report model operations
    public interface IAnalysisModel
    {
        // Get all data for Analysis by its recipe ID
        Task<IGetAnalysisDataByRecipeId_RecipeRunById> GetAnalysisDataByRecipeId(Guid recipeId);
    }

    // This class implements the IAnalysisModel interface
    public class AnalysisModel : IAnalysisModel
    {
        private RVClient _rvClient;

        // Constructor injecting RVClient dependency
        public AnalysisModel(RVClient rvClient)
        {
            _rvClient = rvClient;
        }

        // Implementation of GetAnalysisDataByRecipeId method
        public async Task<IGetAnalysisDataByRecipeId_RecipeRunById> GetAnalysisDataByRecipeId(Guid recipeId)
        {
            // Execute the RVClient method to fetch all measurements for a lot ID
            var result = await _rvClient.GetAnalysisDataByRecipeId.ExecuteAsync(recipeId);
            return result.Data.RecipeRunById;
        }
    }
}
