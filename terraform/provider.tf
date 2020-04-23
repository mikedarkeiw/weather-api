provider "aws" {
  version = "~> 2.0"
  region  = var.aws_region
}

data "aws_ssm_parameter" "rds_password" {
  name     = var.rds_password_ssm_parameter
}