FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /App


# Copy everything
COPY --link . ./
# Restore as distinct layers
RUN dotnet restore 
# Build and publish a release
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
COPY --from=build /App/out .
ENTRYPOINT ["dotnet", "SNotifier.dll"]