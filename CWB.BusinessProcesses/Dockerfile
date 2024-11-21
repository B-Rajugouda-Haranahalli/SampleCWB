#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CWB.BusinessProcesses/CWB.BusinessProcesses.csproj", "CWB.BusinessProcesses/"]
COPY ["CWB.CommonUtils/CWB.CommonUtils.csproj", "CWB.CommonUtils/"]
COPY ["CWB.Extensions/CWB.Extensions.csproj", "CWB.Extensions/"]
COPY ["CWB.Logging/CWB.Logging.csproj", "CWB.Logging/"]
COPY ["CWB.ExceptionHandling/CWB.ExceptionHandling.csproj", "CWB.ExceptionHandling/"]
COPY ["CWB.Constants/CWB.Constants.csproj", "CWB.Constants/"]
RUN dotnet restore "CWB.BusinessProcesses/CWB.BusinessProcesses.csproj"
COPY . .
WORKDIR "/src/CWB.BusinessProcesses"
RUN dotnet build "CWB.BusinessProcesses.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CWB.BusinessProcesses.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CWB.BusinessProcesses.dll"]