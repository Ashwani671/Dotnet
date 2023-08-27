#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Book Management/Book Management.csproj", "Book Management/"]
RUN dotnet restore "Book Management/Book Management.csproj"
COPY . .
WORKDIR "/src/Book Management"
RUN dotnet build "Book Management.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Book Management.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Book Management.dll"]