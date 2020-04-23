
watch:
	dotnet watch -p src/WeatherApi/WeatherApi.csproj run

package-release-lambda:
	cd src/WeatherApi && dotnet build -c Release
	cd src/WeatherApi && dotnet publish -c Release /p:GenerateRuntimeConfigurationFiles=true
	cd src/WeatherApi/bin/Release/netcoreapp3.1/publish && \
		zip -r ../../../../../../packages/api.zip ./

package-debug-lambda:
	cd src/WeatherApi && dotnet build -c Debug
	cd src/WeatherApi && dotnet publish -c Debug /p:GenerateRuntimeConfigurationFiles=true
	cd src/WeatherApi/bin/Debug/netcoreapp3.1/publish && \
		zip -r ../../../../../../packages/api.zip ./		