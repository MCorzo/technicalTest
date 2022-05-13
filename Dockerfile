FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80 443

ENV ASPNETCORE_URLS=http://+;
ENV ASPNETCORE_ENVIRONMENT=Development

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["technicalTest.csproj", "./"]
RUN dotnet restore "technicalTest.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "technicalTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "technicalTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "technicalTest.dll"]

