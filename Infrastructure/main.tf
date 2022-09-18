terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.22.0"
    }
  }
  backend "azurerm" {
    resource_group_name  = var.backend-resource-group-name
    storage_account_name = var.backend-storage-account-name
    container_name       = "tf-state"
    key                  = "terraform.tfstate"
  }

}
provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "terra-az-sqldb" {
  name     = "terra-az-sqldb-resourcegroup"
  location = var.location
}

#resource "azurerm_storage_account" "terra-az-sqldb" {
#  name                     = "terraazsqldbstorage"
#  resource_group_name      = azurerm_resource_group.terra-az-sqldb.name
#  location                 = azurerm_resource_group.terra-az-sqldb.location
#  account_tier             = "Standard"
#  account_replication_type = "LRS"
#}
#
#resource "azurerm_mssql_server" "terra-az-sqldb" {
#  name                         = "terra-az-sqldb-sqlserver"
#  resource_group_name          = azurerm_resource_group.terra-az-sqldb.name
#  location                     = azurerm_resource_group.terra-az-sqldb.location
#  version                      = "12.0"
#  administrator_login          = "__terra-az-sqldb-sqlserver-username__"
#  administrator_login_password = "__terra-az-sqldb-sqlserver-password__"
#}
#
#resource "azurerm_mssql_database" "terra-az-sqldb" {
#  name           = "terra-az-sqldb-db"
#  server_id      = azurerm_mssql_server.terra-az-sqldb.id
#  collation      = "SQL_Latin1_General_CP1_CI_AS"
#  max_size_gb    = 250
#  read_scale     = false
#  sku_name       = "S0"
#  zone_redundant = false
#
#  tags = {
#    Environment = "dev"
#  }
#}
#
#resource "azurerm_service_plan" "terra-az-sqldb" {
#  name                = "terra-az-sqldb-serviceplan"
#  location            = azurerm_resource_group.terra-az-sqldb.location
#  resource_group_name = azurerm_resource_group.terra-az-sqldb.name
#  os_type             = "Linux"
#  sku_name            = "P1v2"
#}
#
#
#resource "azurerm_linux_web_app" "terra-az-sqldb" {
#  name                = "terra-az-sqldb-linuxwebapp"
#  resource_group_name = azurerm_resource_group.terra-az-sqldb.name
#  location            = azurerm_service_plan.terra-az-sqldb.location
#  service_plan_id     = azurerm_service_plan.terra-az-sqldb.id
#
#  site_config {
#  }
#
#  connection_string {
#    name  = "cnTareas"
#    type  = "SQLAzure"
#    value = "Server=tcp:${azurerm_mssql_server.terra-az-sqldb.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.terra-az-sqldb.name};User Id=${azurerm_mssql_server.terra-az-sqldb.administrator_login};Password=${azurerm_mssql_server.terra-az-sqldb.administrator_login_password};Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
#  }
#}