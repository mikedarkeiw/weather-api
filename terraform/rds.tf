resource "aws_rds_cluster" "location_log" {
  cluster_identifier   = "${var.stack_name}-rds-log"
  engine               = "aurora"
  engine_mode          = "serverless"
  master_password      = data.aws_ssm_parameter.rds_password.value
  master_username      = var.rds_username
  skip_final_snapshot  = true
  availability_zones   = module.vpc.azs
  db_subnet_group_name = module.vpc.database_subnet_group
  vpc_security_group_ids = [aws_security_group.allow_mysql.id]
  database_name        = var.database_name 
  apply_immediately    = true

  scaling_configuration {
    auto_pause               = true
    max_capacity             = 2
    min_capacity             = 2
    seconds_until_auto_pause = 300
    timeout_action           = "ForceApplyCapacityChange"
  }

  tags = {
    App = var.stack_name
  } 
}

# resource "aws_db_subnet_group" "rds_subnets" {
#   name       = "${var.stack_name}-rds-log-subnet-group"
#   subnet_ids = module.vpc.database_subnets

#   tags = {
#     App = var.stack_name
#   }
# }