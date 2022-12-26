VERSION=1.1.6
APIKEY=oy2dkifv4u54cibiyhbqszczpvaq4p2erhspkh6ft3kuie

dotnet pack -c release /p:version=$VERSION
nuget push -Source nuget.org ./patrickreinan-aspnet-logging/bin/Release/patrickreinan-aspnet-logging.$VERSION.nupkg -ApiKey $APIKEY