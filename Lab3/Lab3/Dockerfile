FROM mcr.microsoft.com/dotnet/runtime:8.0-nanoserver-1809 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /Lab3
COPY ["./", "./Lab3"]

SHELL ["cmd", "/S", "/C"]
RUN del .\Lab3\INPUT.TXT

RUN dotnet restore "./Lab3/Lab3.csproj"
COPY . .

WORKDIR "/Lab3/Lab3"
RUN dotnet build "./Lab3.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lab3.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["INPUT.TXT", "./Lab3/"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab3.dll"]