#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_16.x | bash \
    && apt-get install nodejs -yq 

RUN npm install -g yarn

WORKDIR /src
COPY ["src/ArmaPresetCreator.Web/ArmaPresetCreator.Web.csproj", "src/ArmaPresetCreator.Web/"]
RUN dotnet restore "src/ArmaPresetCreator.Web/ArmaPresetCreator.Web.csproj"
COPY . .
WORKDIR "/src/src/ArmaPresetCreator.Web"
RUN dotnet build "ArmaPresetCreator.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArmaPresetCreator.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArmaPresetCreator.Web.dll"]