Echo batch file delete folder
dotnet build ../ResultViewer.Server/ResultViewer.Server.csproj
start cmd.exe @cmd /k "dotnet run --urls "https://localhost:7241" --no-build --project ../ResultViewer.Server/ResultViewer.Server.csproj"
TIMEOUT /T 60
@RD /S /Q "bin"
@RD /S /Q "obj"
del "GraphQL\schema.graphql"
del "GraphQL\schema.extensions.graphql"
del "GraphQL\.graphqlrc.json"
dotnet graphql init https://localhost:7241/graphql/ -n RVClient -p ./GraphQL
dotnet build ./ResultViewer.Client.csproj
cmd /k