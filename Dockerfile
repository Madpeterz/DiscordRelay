#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DiscordRelay/DiscordRelay.csproj", "DiscordRelay/"]
RUN dotnet restore "DiscordRelay/DiscordRelay.csproj"
COPY . .
WORKDIR "/src/DiscordRelay"
RUN dotnet build "DiscordRelay.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiscordRelay.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# --- Update your settings ---

ENV Discord_Token='XXXXX'
ENV Discord_Target_server='XXXXX'
ENV Discord_Target_channel='XXXXX'
ENV Discord_Master='XXXXX'
ENV Relay_1_server='XXX'
ENV Relay_1_channel='XXX'

# --- End of settings ---

ENTRYPOINT ["/app/DiscordRelay"]