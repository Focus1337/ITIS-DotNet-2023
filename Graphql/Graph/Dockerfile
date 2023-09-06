FROM mcr.microsoft.com/dotnet/sdk:7.0 AS dev
WORKDIR /app

COPY ["Graph/Graph.csproj", "Graph/"]
RUN dotnet restore "Graph/Graph.csproj"

COPY ["Graph/", "Graph/"]

WORKDIR "/app/Graph"

ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://+:81"] 
