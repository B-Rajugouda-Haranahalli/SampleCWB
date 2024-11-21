#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CWB.Email/CWB.Email.csproj", "CWB.Email/"]
COPY ["CWB.Extensions/CWB.Extensions.csproj", "CWB.Extensions/"]
COPY ["CWB.Logging/CWB.Logging.csproj", "CWB.Logging/"]
COPY ["CWB.ExceptionHandling/CWB.ExceptionHandling.csproj", "CWB.ExceptionHandling/"]
COPY ["CWB.Constants/CWB.Constants.csproj", "CWB.Constants/"]
COPY ["CWB.Notification/CWB.Notification.csproj", "CWB.Notification/"]
RUN dotnet restore "CWB.Email/CWB.Email.csproj"
COPY . .
WORKDIR "/src/CWB.Email"
RUN dotnet build "CWB.Email.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CWB.Email.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CWB.Email.dll"]