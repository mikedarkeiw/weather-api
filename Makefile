
watch:
	dotnet watch -p src/WeatherApi/WeatherApi.csproj run

package-release-lambda:
	cd src/WeatherApi && dotnet build -c Release
	cd src/WeatherApi && dotnet publish -c Release /p:GenerateRuntimeConfigurationFiles=true
	cd src/WeatherApi/bin/Release/netcoreapp3.1/publish && \
		zip -r ../../../../../../packages/weather-api.zip ./

package-debug-lambda:
	cd src/WeatherApi && dotnet build -c Debug
	cd src/WeatherApi && dotnet publish -c Debug /p:GenerateRuntimeConfigurationFiles=true
	cd src/WeatherApi/bin/Debug/netcoreapp3.1/publish && \
		zip -r ../../../../../../packages/weather-api.zip ./

upload-lambda-package: package-debug-lambda
	aws s3 cp ./packages/weather-api.zip s3://miked-lambda-packages/weather-api.zip

aws-tf-deploy:
	$(MAKE) package-debug-lambda
	cd terraform && terraform init
	cd terraform && terraform apply

upload-cf-stacks:
	aws s3 cp --recursive  ./cloudformation/ s3://miked-cf-stacks/