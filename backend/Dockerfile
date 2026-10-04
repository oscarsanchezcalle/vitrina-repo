FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Vitrina.sln", "."]
COPY ["src/1. Vitrina.Api/Vitrina.Api.csproj", "src/1. Vitrina.Api/"]
COPY ["src/2. Vitrina.Business/Vitrina.Business.csproj", "src/2. Vitrina.Business/"]
COPY ["src/3. Vitrina.Dto/Vitrina.Dto.csproj", "src/3. Vitrina.Dto/"]
COPY ["src/4. Vitrina.Data/Vitrina.Data.csproj", "src/4. Vitrina.Data/"]
RUN dotnet restore "src/1. Vitrina.Api/Vitrina.Api.csproj"

COPY ["src/", "src/"]
WORKDIR /src/src/1. Vitrina.Api
RUN dotnet publish "Vitrina.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Vitrina.Api.dll"]
