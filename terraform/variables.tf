variable "stack_name" {
    type = string
    default = "weather"
}

variable "aws_region" {
    type = string
    default = "us-east-1"
}

variable "rds_password_ssm_parameter" {
    type = string
    default = "/weatherapi/rds/password"
}

variable  rds_username {
    type = string
    default = "weather_admin"
}

variable database_name {
    type = string
    default = "WeatherApi"
}