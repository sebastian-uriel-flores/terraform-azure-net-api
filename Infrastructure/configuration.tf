variable "location" {
  description = "Location of the resources"
  type        = string
  sensitive   = false
  default     = "Brazil South"
  nullable    = false
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
  sensitive   = false
  default     = "demo-resource-group"
}

variable "sqlserver_administrator_login" {
  description = "Username of the SQL Server Instance"
  type        = string
  sensitive   = true
  nullable    = false
}

variable "sqlserver_administrator_login_password" {
  description = "Password of the SQL Server Instance"
  type        = string
  sensitive   = true
  nullable    = false
}