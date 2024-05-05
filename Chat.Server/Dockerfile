ARG VERSION=8.0
FROM mcr.microsoft.com/dotnet/aspnet:${VERSION} AS base
EXPOSE 5000
WORKDIR /

FROM mcr.microsoft.com/dotnet/sdk:${VERSION} AS build
WORKDIR /src
#COPY ["Chat.Server/", "Chat.Server/"]
#COPY ["Chat.Shared/", "Chat.Shared/"]
COPY . .
RUN dotnet restore
COPY . .
WORKDIR "/src/Chat.Server"
RUN dotnet build -c Debug -o /

FROM build AS publish
RUN dotnet publish -c Debug -o /

FROM base AS final
WORKDIR /
COPY --from=publish / .
ENTRYPOINT ["dotnet", "Chat.Server.dll"]