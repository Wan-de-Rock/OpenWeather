FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5020
EXPOSE 7020

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/Infrastructure/OpenWeather.WebAPI/OpenWeather.WebAPI.csproj", "Src/Infrastructure/OpenWeather.WebAPI/"]
COPY ["Src/Core/OpenWeather.Application/OpenWeather.Application.csproj", "Src/Core/OpenWeather.Application/"]
COPY ["Src/Core/OpenWeather.Domain/OpenWeather.Domain.csproj", "Src/Core/OpenWeather.Domain/"]
RUN dotnet restore "Src/Infrastructure/OpenWeather.WebAPI/OpenWeather.WebAPI.csproj"
COPY . .
WORKDIR "/src/Src/Infrastructure/OpenWeather.WebAPI"
RUN dotnet build "OpenWeather.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenWeather.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenWeather.WebAPI.dll"]