resource "aws_lambda_function" "weather_api_lambda" {
    filename      = "../packages/api.zip"
    function_name = "${var.stack_name}_api"
    role          = aws_iam_role.iam_for_lambda.arn
    handler       = "WeatherApi::WeatherApi.LambdaEntryPoint::FunctionHandlerAsync"
    source_code_hash = filebase64sha256("../packages/api.zip")
    runtime = "dotnetcore3.1"
    reserved_concurrent_executions = -1
    memory_size = 512
    timeout = 6
    vpc_config {
        subnet_ids = module.vpc.private_subnets
        security_group_ids = [aws_security_group.all_https.id]
    }
    environment {
        variables = {
            ConnectionStrings__Main = "server=${aws_rds_cluster.location_log.endpoint};port=3306;user=${var.rds_username};password=${data.aws_ssm_parameter.rds_password.value};database=${var.database_name};"
        }
    }
    tags = {
        App = var.stack_name
    }    
}

resource "aws_lambda_permission" "lambda_permission" {
  statement_id  = "AllowWeatherAPIInvoke"
  action        = "lambda:InvokeFunction"
  function_name = "${var.stack_name}_api"
  principal     = "apigateway.amazonaws.com"

  # The /*/*/* part allows invocation from any stage, method and resource path
  # within API Gateway REST API.
  source_arn = "${aws_api_gateway_rest_api.WeatherApi.execution_arn}/*/*/*"
}