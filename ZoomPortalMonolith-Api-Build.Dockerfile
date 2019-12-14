#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
RUN  apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /src

ARG CACHE_DATE

COPY ["ZoomPortalMonolith.Api/ZoomPortalMonolith.Api.csproj", "ZoomPortalMonolith.Api/"]
COPY ["ZoomPortalMonolith.Domain/ZoomPortalMonolith.Domain.csproj", "ZoomPortalMonolith.Domain/"]
COPY ["ZoomPortalMonolith.SharedKernal.Domain/ZoomPortalMonolith.SharedKernal.Domain.Foundation.csproj", "ZoomPortalMonolith.SharedKernal.Domain/"]
COPY ["ZoomPortalMonolith.ViewModels/ZoomPortalMonolith.ViewModels.csproj", "ZoomPortalMonolith.ViewModels/"]
COPY ["ZoomPortalMonolith.Infrastructure.EntityFramework/ZoomPortalMonolith.Infrastructure.EntityFramework.csproj", "ZoomPortalMonolith.Infrastructure.EntityFramework/"]
COPY ["ZoomPortalMonolith.Infrastructure/ZoomPortalMonolith.SharedKernal.Infrastructure.csproj", "ZoomPortalMonolith.Infrastructure/"]
RUN dotnet restore "ZoomPortalMonolith.Api/ZoomPortalMonolith.Api.csproj"
COPY . .
WORKDIR "/src/ZoomPortalMonolith.Api"
RUN dotnet build "ZoomPortalMonolith.Api.csproj" -c Release -o /app/build


FROM build AS publish
ARG CACHE_DATE
RUN dotnet publish "ZoomPortalMonolith.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ARG CACHE_DATE
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZoomPortalMonolith.Api.dll"]