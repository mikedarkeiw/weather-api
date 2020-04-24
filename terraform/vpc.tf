# Create a VPC
module "vpc" {
  source  = "terraform-aws-modules/vpc/aws"
  version = "2.33.0"
  # insert the 8 required variables here
  name = "weather-api-vpc"

  cidr = "10.101.0.0/16"

  azs             = ["${var.aws_region}a", "${var.aws_region}b", "${var.aws_region}d"]
  private_subnets = ["10.101.1.0/24", "10.101.2.0/24", "10.101.3.0/24"]
  public_subnets  = ["10.101.101.0/24", "10.101.102.0/24", "10.101.103.0/24"]
  database_subnets = ["10.101.201.0/24", "10.101.202.0/24", "10.101.203.0/24"]

  enable_ipv6 = true
  enable_classiclink = false
  enable_nat_gateway = true
  single_nat_gateway = true
  create_database_subnet_group = true

  public_subnet_tags = {
    App = var.stack_name
  }

  tags = {
    Environment = "dev"
    App = var.stack_name
  }

  vpc_tags = {
    Name = "${var.stack_name}-vpc"
  }
}