#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["AspNetCoreDemo/AspNetCoreDemo.csproj", "AspNetCoreDemo/"]
RUN dotnet restore "AspNetCoreDemo/AspNetCoreDemo.csproj"
COPY . .
WORKDIR "/src/AspNetCoreDemo"
RUN dotnet build "AspNetCoreDemo.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AspNetCoreDemo.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AspNetCoreDemo.dll"]