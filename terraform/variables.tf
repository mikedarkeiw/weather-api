variable "stack_name" {
    type = string
    default = "weather"
}

variable "aws_region" {
    type = string
    default = "us-east-1"
}

variable "subnet_ids" {
    type    = list(string)
    default = ["subnet-073258d3e93b24c3f", "subnet-03e0cf29a57e0ccf3"]
}

variable "vpc_id" {
    default = "vpc-026f5b618d0ad3cea"
}

variable "sg_ingress" {
    type    = list(string)
    default = ["10.100.11.0/24", "10.100.10.0/24"]
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