VERSION=1.1.7
APIKEY=

dotnet pack -c release /p:version=$VERSION
nuget push -Source nuget.org ./patrickreinan-aspnet-logging/bin/Release/patrickreinan-aspnet-logging.$VERSION.nupkg -ApiKey $APIKEY
