FROM mcr.microsoft.com/dotnet/runtime:8.0-nanoserver-1809 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /Lab2
COPY ["./", "./Lab2"]

SHELL ["cmd", "/S", "/C"]
RUN del .\Lab2\INPUT.TXT

RUN dotnet restore "./Lab2/Lab2.csproj"
COPY . .

WORKDIR "/Lab2/Lab2"
RUN dotnet build "./Lab2.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lab2.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["INPUT.TXT", "./Lab2/"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab2.dll"]