resource "aws_api_gateway_rest_api" "WeatherApi" {
  name        = "${var.stack_name}_api"
  description = "API to get weather data for a location"
}

resource "aws_api_gateway_resource" "ApiResource" {
  rest_api_id = aws_api_gateway_rest_api.WeatherApi.id
  parent_id   = aws_api_gateway_rest_api.WeatherApi.root_resource_id
  path_part   = "{proxy+}"
}

resource "aws_api_gateway_method" "ApiMethod" {
  rest_api_id   = aws_api_gateway_rest_api.WeatherApi.id
  resource_id   = aws_api_gateway_resource.ApiResource.id
  http_method   = "ANY"
  authorization = "NONE"
}

resource "aws_api_gateway_integration" "ApiIntegration" {
  rest_api_id = aws_api_gateway_rest_api.WeatherApi.id
  resource_id = aws_api_gateway_resource.ApiResource.id
  http_method = aws_api_gateway_method.ApiMethod.http_method
  integration_http_method = "POST"
  type                    = "AWS_PROXY"
  uri                     = aws_lambda_function.weather_api_lambda.invoke_arn
}

resource "aws_api_gateway_deployment" "ApiTestDeployment" {
  depends_on = [aws_api_gateway_integration.ApiIntegration]

  rest_api_id = aws_api_gateway_rest_api.WeatherApi.id
  stage_name  = "test"
}

resource "aws_api_gateway_deployment" "ApiProdDeployment" {
  depends_on = [aws_api_gateway_integration.ApiIntegration]

  rest_api_id = aws_api_gateway_rest_api.WeatherApi.id
  stage_name  = "prod"
}