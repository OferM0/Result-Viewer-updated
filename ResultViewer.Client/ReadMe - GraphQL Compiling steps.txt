1) Remove from "GraphQL" folder 3 files : ".graphqlrc.json", "schema.extensions.graphql", and "schema.graphql"
2) Open the project's folder in File Explorer => Remove two folders : "obj" and "bin". 
	(if the removal didn't work, then you need to close Visual Studio)
3) Run the ResultViewer.Server(required)
4) Open the Package Manager Console and execute the command below
	dotnet graphql init https://localhost:7241/graphql/ -n RVClient -p ./GraphQL
5) Build the project.