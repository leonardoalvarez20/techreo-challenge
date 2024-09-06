FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5014

ENV ASPNETCORE_URLS=http://+:5014

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["TechreoChallenge.Api/TechreoChallenge.Api.csproj", "TechreoChallenge.Api/"]
RUN dotnet restore "TechreoChallenge.Api/TechreoChallenge.Api.csproj"
COPY . .
WORKDIR "/src/TechreoChallenge.Api"
RUN dotnet build "TechreoChallenge.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "TechreoChallenge.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechreoChallenge.Api.dll"]
