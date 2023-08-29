#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src
COPY ["powerplant-coding-challenge.csproj", "powerplant-coding-challenge/"]
RUN dotnet restore "powerplant-coding-challenge/powerplant-coding-challenge.csproj"
WORKDIR "/src/powerplant-coding-challenge"
COPY . .
RUN dotnet build "powerplant-coding-challenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "powerplant-coding-challenge.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "powerplant-coding-challenge.dll"]
