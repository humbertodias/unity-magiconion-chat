ARG VERSION=8.0
ARG CONFIGURATION=Debug
#ARG RUNTIME=osx-arm64
#ARG RUNTIME=linux-arm64
ARG RUNTIME=linux-x64

FROM mcr.microsoft.com/dotnet/aspnet:${VERSION} AS base
EXPOSE 5000
WORKDIR /

FROM mcr.microsoft.com/dotnet/sdk:${VERSION} AS build
WORKDIR /src
COPY ["Chat.Server/", "Chat.Server/"]
COPY ["Chat.Shared/", "Chat.Shared/"]
RUN dotnet restore "Chat.Server/Chat.Server.csproj"
COPY . .
WORKDIR "/src/Chat.Server"
RUN dotnet build "Chat.Server.csproj" -c ${CONFIGURATION} -r ${RUNTIME} -o /

FROM build AS publish
RUN dotnet publish "Chat.Server.csproj" -c ${CONFIGURATION} -r ${RUNTIME} -o /

FROM base AS final
WORKDIR /
COPY --from=publish / .
ENTRYPOINT ["dotnet", "Chat.Server.dll"]