using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Mutation
{
    [ExtendObjectType(Name = "Mutation")]
    public class LotRunMutation
    {
        [GraphQLDescription("Creates a new lot run")]
        public async Task<LotRunPayload> CreateLotRunAsync(LotRunInput lotRun, [Service] ILotRunRepository lotRunRepo)
        {
            // Create a new LotRun entity
            LotRun lotRunEntity = new();

            // Insert the LotRun entity into the repository and await the result
            var result =  await lotRunRepo.Insert(lotRunEntity);

            // Return a new LotRunPayload with the result
            return new LotRunPayload(result);
        }

        [GraphQLDescription("Updates lot run by id")]
        public async Task<LotRunPayload> UpdateLotRunByIdAsync(Guid id, LotRunInput lotRun, [Service] ILotRunRepository lotRunRepo)
        {
            // Create a new LotRun entity
            LotRun lotRunEntity = new();

            // Update the LotRun entity in the repository and await the result
            var result = await lotRunRepo.Update(lotRunEntity);

            // Return a new LotRunPayload with the result
            return new LotRunPayload(result);
        }

        [GraphQLDescription("Deletes lot run by id")]
        public async Task DeleteLotRunByIdAsync(Guid id, [Service] ILotRunRepository lotRunRepo)
        {
            // Delete the LotRun entity from the repository by its ID
            await lotRunRepo.Delete(id);
        }
    }
}
