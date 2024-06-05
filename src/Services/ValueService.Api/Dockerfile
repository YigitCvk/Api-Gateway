#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/ValueService.Api/ValueService.Api.csproj", "src/Services/ValueService.Api/"]
COPY ["src/Common/Service.Core/Service.Core.csproj", "src/Common/Service.Core/"]
RUN dotnet restore "src/Services/ValueService.Api/ValueService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/ValueService.Api"
RUN dotnet build "ValueService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ValueService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ValueService.Api.dll"]