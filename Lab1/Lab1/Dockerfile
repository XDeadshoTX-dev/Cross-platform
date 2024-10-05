FROM mcr.microsoft.com/dotnet/runtime:8.0-nanoserver-1809 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /Lab1
COPY ["./", "./Lab1"]

SHELL ["cmd", "/S", "/C"]
RUN del .\Lab1\INPUT.TXT

RUN dotnet restore "./Lab1/Lab1.csproj"
COPY . .

WORKDIR "/Lab1/Lab1"
RUN dotnet build "./Lab1.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lab1.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["INPUT.TXT", "./Lab1/"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab1.dll"]