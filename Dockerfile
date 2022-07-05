FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ["src/TheDevSpaceWebApp/TheDevSpaceWebApp.csproj", "src/TheDevSpaceWebApp/"]
COPY ["src/TheDevSpace.Repository/TheDevSpace.Repository.csproj", "src/TheDevSpace.Repository/"]
COPY ["src/TheDevSpace.Domain/TheDevSpace.Domain.csproj", "src/TheDevSpace.Domain/"]
COPY ["src/TheDevSpace.Application/TheDevSpace.Application.csproj", "src/TheDevSpace.Application/"]
RUN dotnet restore "src/TheDevSpaceWebApp/TheDevSpaceWebApp.csproj"
COPY . .
RUN dotnet test tests/TheDevSpaceTests.Domain
RUN dotnet publish -c Release -o out

ARG LICENSE_KEY

# Install the agent
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y newrelic-netcore20-agent \
&& rm -rf /var/lib/apt/lists/*

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-netcore20-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-netcore20-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=$LICENSE_KEY \
NEW_RELIC_APP_NAME=TheDevSpaceWebApp

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TheDevSpaceWebApp.dll"]