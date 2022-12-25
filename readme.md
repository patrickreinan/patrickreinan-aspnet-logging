VERSION=1.1.2
APIKEY=xxxx

dotnet pack -c release /p:version=1.1.2
nuget push -Source nuget.org /Users/patrickreinan/git/patrickreinan-aspnet-logging/patrickreinan-aspnet-logging/bin/Release/patrickreinan-aspnet-logging.1.1.2.nupkg -ApiKey $APIKEY