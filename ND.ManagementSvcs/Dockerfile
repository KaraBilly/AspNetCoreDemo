#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["ND.ManagementSvcs/ND.ManagementSvcs.csproj", "ND.ManagementSvcs/"]
RUN dotnet restore "ND.ManagementSvcs/ND.ManagementSvcs.csproj"
COPY . .
WORKDIR "/src/ND.ManagementSvcs"
RUN dotnet build "ND.ManagementSvcs.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ND.ManagementSvcs.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ND.ManagementSvcs.dll"]
