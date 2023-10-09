using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using System.Text.Json;

namespace ResultViewer.Server.Tests
{
    public class IntegrationsTests : IDisposable
    {
        //Dotnet run --urls "https://localhost:7241"
        private readonly HttpClient _httpClient;
        private const string GraphQLEndpoint = "https://localhost:7241/graphql";

        public IntegrationsTests()
        {
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        [UseReporter(typeof(DiffReporter))]
        [Fact]
        public async Task TestSchema()
        {
            // Send the Introspection Query
            var introspectionQuery = @"
                query {
                  __schema {
                    queryType {
                      kind,
                      name,
                      description
                    },
                    mutationType {
                      kind,
                      name,
                      description
                    },
                    directives{
                      name,
                      description
                    },
                    types{
                      kind,
                      name,
                      description
                    }
                  }
                }";

            var responseBody = await ExecuteAsync(introspectionQuery);
            Approvals.Verify(responseBody.FormatJson());
        }

        [UseReporter(typeof(DiffReporter))]
        [Fact]
        public async Task TestGetAllLotRunsQuery()
        {
            // Arrange
            var query = @"
                query {
                  allLotRuns {
                    run_Id,
                    tool_Id,
                    failed_Wafers,
                    failed_Sites,
                    comments,
                    stepper_Id_2,
                    recipe_Run_Id,
                    slot_Map,
                    iteration,
                    calibration_Mode,
                    port_Num,
                    lot_Name,
                    arp_Flag,
                    tis_Mode,
                    access_Method,
                    lied_File_Flag,
                    control_Job_Id,
                    process_Job_Id,
                    carrier_Id,
                    analysis_Recipe_Name,
                    handling_Mode,
                    ds_Iteration_Number,
                    recipe_Name,
                    calc_Qmerit,
                    modeled_TIS_Run_Enabled,
                    imaging_Accuracy_Metrics,
                    lot_Status,
                    run_Start_Time,
                    run_End_Time
                  }
                }
            ";

            var responseBody = await ExecuteAsync(query);
            Approvals.Verify(responseBody.FormatJson());
        }
        [UseReporter(typeof(DiffReporter))]
        [Fact]
        public async Task TestGetLotRunByIdQuery()
        {
            // Arrange
            var query = @"
                query{
                  lotRunById(id: ""6e5c37ef-c400-11ed-85e8-005056b2bec0"") {
                    run_Id,
                    tool_Id,
                    failed_Wafers,
                    failed_Sites,
                    comments,
                    stepper_Id_2,
                    recipe_Run_Id,
                    slot_Map,
                    iteration,
                    calibration_Mode,
                    port_Num,
                    lot_Name,
                    arp_Flag,
                    tis_Mode,
                    access_Method,
                    lied_File_Flag,
                    control_Job_Id,
                    process_Job_Id,
                    carrier_Id,
                    analysis_Recipe_Name,
                    handling_Mode,
                    ds_Iteration_Number,
                    recipe_Name,
                    calc_Qmerit,
                    modeled_TIS_Run_Enabled,
                    imaging_Accuracy_Metrics,
                    lot_Status,
                    run_Start_Time,
                    run_End_Time
                  }
                }
            ";

            var responseBody = await ExecuteAsync(query);
            Approvals.Verify(responseBody.FormatJson());
        }
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestGetAllRecipeRunsQuery()
        {
            // Arrange
            var query = @"
                  query{
                  allRecipeRuns {
                    recipe_Run_Id,
                    recipe_Name,
                    wafer_Size
                    lotRuns {
                      run_Id,
                      lot_Name,
                      lot_Status
                    }
                  }
                }
            ";

            var responseBody = await ExecuteAsync(query);
            Approvals.Verify(responseBody.FormatJson());
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestGetRecipeByIdQuery()
        {
            // Arrange
            var query = @"
                query {
                recipeRunById(id: ""1f8115e2-d20e-11eb-85b9-005056b27306"") {
                recipe_Run_Id,
                version_String,
                recipe_Creation_Date,
                recipe_Modification_Date,
                wafer_Size,
                wafer_Type,
                wafer_Orientation,
                lotRuns {
                  run_Id,
                  lot_Name,
                  lot_Status
                }
               }
              }
            ";

            var responseBody = await ExecuteAsync(query);
            Approvals.Verify(responseBody.FormatJson());
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestGetallWaferRunsQuery()
        {
            //"1f8115e1-d20e-11eb-85b9-005056b27306"
            // Arrange
            var query = @"
                query GetWaferRuns {
                allWaferRuns {
                    run_Id,
                    wafer_Run_Id,
                    wafer_Ordinal_Num,
                    wafer_Start_Time,
                    wafer_End_Time,
                    conditional_Site_Mode,
                    images_Meta_Data_Path,
                    lotRun {
                      run_Id,
                      lot_Name,
                      recipe_Name,
                      lot_Status,
                      run_Start_Time,
                      run_End_Time
                    }
                  }
                }
            ";

            var responseBody = await ExecuteAsync(query);
            Approvals.Verify(responseBody.FormatJson());
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestGetWaferRunByIdQuery()
        {
            //"1f8115e1-d20e-11eb-85b9-005056b27306"
            // Arrange
            var query = @"
                query{
                  waferRunById(id: ""1f8115e1-d20e-11eb-85b9-005056b27306"") {
                    run_Id,
                     wafer_Run_Id,
                     wafer_Ordinal_Num,
                     wafer_Start_Time,
                     wafer_End_Time,
                     conditional_Site_Mode,
                     images_Meta_Data_Path,
                    lotRun {
                      run_Id,
                       lot_Name,
                       recipe_Name,
                       lot_Status,
                       run_Start_Time,
                       run_End_Time
                    }
                  }
                }
            ";

            var responseBody = await ExecuteAsync(query);
            Approvals.Verify(responseBody.FormatJson());
        }
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestGetAllTestRecipeRunsQuery()
        {
            // Arrange
            var query = @"
                  query{
                  allTestRecipeRuns {
                   test_Recipe_Run_Id,
                   recipe_Run_Id,
                   test_Number,
                   recipeRun {
                     recipe_Run_Id,
                     wafer_Size,
                     recipe_Name
                   }
                 }
                }
            ";

            var responseBody = await ExecuteAsync(query);
            Approvals.Verify(responseBody.FormatJson());
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestGetTestRecipeByIdQuery()
        {
            // Arrange
            var query = @"
                query {
                  testRecipeRunById(id: ""1f8115e3-d20e-11eb-85b9-005056b27306"") {
                   test_Recipe_Run_Id,
                   recipe_Run_Id,
                   test_Number,
                   recipeRun {
                     recipe_Run_Id,
                     wafer_Size,
                     recipe_Name
                   }
                 }
              }
            ";

            var responseBody = await ExecuteAsync(query);
            Approvals.Verify(responseBody.FormatJson());
        }
        private async Task<string> ExecuteAsync(string query)
        {
            var requestBody = new { query };
            var jsonBody = System.Text.Json.JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var httpContent = new StringContent(jsonBody);

            // Set the Content-Type header
            httpContent.Headers.ContentType.MediaType = "application/json";

            // Act
            var response = await _httpClient.PostAsync(GraphQLEndpoint, httpContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Ensure HTTP request was successful

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }

}