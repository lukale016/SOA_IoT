FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
COPY . .
RUN ["dotnet", "restore"]
COPY . ./
RUN dotnet build "HandOneAndTwoMicroservice.csproj" -c Release -o /app/build
RUN dotnet publish "HandOneAndTwoMicroservice.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "HandOneAndTwoMicroservice.dll"]