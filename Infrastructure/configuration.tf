variable "backend-resource-group-name" {
  description = "Name of the resource group used by the backend"
  type        = string
  sensitive   = true  
}

variable "backend-storage-account-name" {
  description = "Name of the storage account used by the backend"
  type        = string
  sensitive   = true  
}

variable "location" {
  description = "Location of the resources"
  type        = string
  sensitive   = false
  default     = "Brazil South"
  nullable    = false
}