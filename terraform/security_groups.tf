resource "aws_security_group" "allow_mysql" {
  name        = "${var.stack_name}-allow-mysql"
  description = "Allow TLS inbound traffic on ports 3306 for MySql DB"
  vpc_id = var.vpc_id

  ingress {
    from_port   = 3306
    to_port     = 3306
    protocol    = "tcp"
    cidr_blocks = var.sg_ingress
  }

  egress {
    from_port       = 0
    to_port         = 0
    protocol        = "-1"
    cidr_blocks     = ["0.0.0.0/0"]
  }

  tags = {
    App = var.stack_name
  }
}

resource "aws_security_group" "all_https" {
  name        = "${var.stack_name}-allow-https"
  description = "Allow TLS inbound traffic on ports 443 for lambda api gateway requests"
  vpc_id = var.vpc_id

  ingress {
    from_port       = 0
    to_port         = 0
    protocol        = "HTTPS"
    cidr_blocks     = ["0.0.0.0/0"]
  }

  egress {
    from_port       = 0
    to_port         = 0
    protocol        = "-1"
    cidr_blocks     = ["0.0.0.0/0"]
  }

  tags = {
    App = var.stack_name
  }
}