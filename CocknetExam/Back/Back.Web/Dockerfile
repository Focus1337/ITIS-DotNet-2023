FROM mcr.microsoft.com/dotnet/sdk:7.0 AS dev
WORKDIR /app

COPY ["Back.Web/Back.Web.csproj", "Back.Web/"]
COPY ["Back.Core/Back.Core.csproj", "Back.Core/"]
COPY ["Back.Infrastructure/Back.Infrastructure.csproj", "Back.Infrastructure/"]
RUN dotnet restore "Back.Web/Back.Web.csproj"

COPY . .

WORKDIR "/app/Back.Web"

ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://+:5000"] 