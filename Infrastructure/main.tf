terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.22.0"
    }
    random = {
      source  = "hashicorp/random"
      version = "2.3.1"
    }
  }
  backend "azurerm" {
    resource_group_name  = "tf-backend-rg"
    storage_account_name = "tfbackendstac01"
    container_name       = "tfstate"
    key                  = "terraform.tfstate"
  }

}
provider "azurerm" {
  features {}
}

resource "random_string" "demo" {
  length  = 6
  special = false
  upper   = false
}

resource "azurerm_resource_group" "demo" {
  name     = var.resource_group_name
  location = var.location

  tags = {
    Scope = "Demo"
  }
}

#resource "azurerm_storage_account" "demo" {
#  name                     = "sqlserverstorage${random_string.demo.result}"
#  resource_group_name      = azurerm_resource_group.demo.name
#  location                 = azurerm_resource_group.demo.location
#  account_tier             = "Standard"
#  account_replication_type = "LRS"
#
#  tags = {
#    Scope = "Demo"
#  }
#}

resource "azurerm_mssql_server" "demo" {
  name                         = "sqlserver${random_string.demo.result}"
  resource_group_name          = azurerm_resource_group.demo.name
  location                     = azurerm_resource_group.demo.location
  version                      = "12.0"
  administrator_login          = var.sqlserver_administrator_login
  administrator_login_password = var.sqlserver_administrator_login_password

  tags = {
    Scope = "Demo"
  }
}

resource "azurerm_mssql_firewall_rule" "demo" {
  name             = "sqlserverfwrule${random_string.demo.result}"
  server_id        = azurerm_mssql_server.demo.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

resource "azurerm_mssql_database" "demo" {
  name      = "sqldb${random_string.demo.result}"
  server_id = azurerm_mssql_server.demo.id
  collation = "SQL_Latin1_General_CP1_CI_AS"
  sku_name  = "Basic"
  tags = {
    Scope = "Demo"
  }
}

resource "azurerm_service_plan" "demo" {
  name                = "webappserviceplan${random_string.demo.result}"
  location            = azurerm_resource_group.demo.location
  resource_group_name = azurerm_resource_group.demo.name
  os_type             = "Linux"
  sku_name            = "F1"

  tags = {
    Scope = "Demo"
  }
}

resource "azurerm_linux_web_app" "demo" {
  name                = "webapp${random_string.demo.result}"
  resource_group_name = azurerm_resource_group.demo.name
  location            = azurerm_service_plan.demo.location
  service_plan_id     = azurerm_service_plan.demo.id

  site_config {
    always_on         = false
    app_command_line  = "dotnet TerraAzSQLAPI.dll"
    use_32_bit_worker = true
    application_stack {
      dotnet_version = "6.0"
    }
  }
  auth_settings {
    enabled                       = true
    unauthenticated_client_action = "AllowAnonymous"
  }

  connection_string {
    name  = "cnTareas"
    type  = "SQLAzure"
    value = "Server=tcp:${azurerm_mssql_server.demo.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.demo.name};User Id=${azurerm_mssql_server.demo.administrator_login};Password=${azurerm_mssql_server.demo.administrator_login_password};Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }

  tags = {
    Scope = "Demo"
  }
}