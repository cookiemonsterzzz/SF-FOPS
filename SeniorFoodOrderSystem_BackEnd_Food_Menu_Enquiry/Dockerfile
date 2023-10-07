#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry/SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry.csproj", "SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry/"]
RUN dotnet restore "SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry/SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry.csproj"
COPY . .
WORKDIR "/src/SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry"
RUN dotnet build "SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SeniorFoodOrderSystem_BackEnd_Food_Menu_Enquiry.dll"]
