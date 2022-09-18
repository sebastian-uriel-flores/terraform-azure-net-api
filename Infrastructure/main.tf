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

resource "azurerm_storage_account" "demo" {
  name                     = "sqlserverstorage${random_string.demo.result}"
  resource_group_name      = azurerm_resource_group.demo.name
  location                 = azurerm_resource_group.demo.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  tags = {
    Scope = "Demo"
  }
}

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

resource "azurerm_mssql_database" "demo" {
  name           = "sqlserverdb${random_string.demo.result}"
  server_id      = azurerm_mssql_server.demo.id
  collation      = "SQL_Latin1_General_CP1_CI_AS"
  max_size_gb    = 250
  read_scale     = false
  sku_name       = "S0"
  zone_redundant = false

  tags = {
    Scope = "Demo"
  }
}

resource "azurerm_service_plan" "demo" {
  name                = "webappserviceplan${random_string.demo.result}"
  location            = azurerm_resource_group.demo.location
  resource_group_name = azurerm_resource_group.demo.name
  os_type             = "Linux"
  sku_name            = "P1v2"

  tags = {
    Scope = "Demo"
  }
}

#resource "azurerm_network_security_group" "demo" {
#  name                = "securitygroup${random_string.demo.result}"
#  location            = azurerm_resource_group.demo.location
#  resource_group_name = azurerm_resource_group.demo.name
#}

resource "azurerm_virtual_network" "demo" {
  name                = "virtualnetwork${random_string.demo.result}"
  location            = azurerm_resource_group.demo.location
  resource_group_name = azurerm_resource_group.demo.name
  address_space       = ["10.0.0.0/16"]
  dns_servers         = ["10.0.0.4", "10.0.0.5"]

  tags = {
    Scope = "Demo"
  }
}

resource "azurerm_subnet" "demo" {
  name                 = "subnet${random_string.demo.result}"
  resource_group_name  = azurerm_resource_group.demo.name
  virtual_network_name = azurerm_virtual_network.demo.name
  address_prefixes     = ["10.0.1.0/24"]

  delegation {
    name = "delegation"

    service_delegation {
      name    = "Microsoft.ContainerInstance/containerGroups"
      actions = ["Microsoft.Network/virtualNetworks/subnets/join/action", "Microsoft.Network/virtualNetworks/subnets/prepareNetworkPolicies/action"]
    }
  }
}

resource "azurerm_api_management" "demo" {
  name                 = "apimanagement${random_string.demo.result}"
  location             = azurerm_resource_group.demo.location
  resource_group_name  = azurerm_resource_group.demo.name
  publisher_name       = "demo-api"
  publisher_email      = "s.flores@outlook.com.ar"
  virtual_network_type = "Internal"
  virtual_network_configuration {
    subnet_id = azurerm_subnet.demo.id
  }

  sku_name = "Basic_0"

  tags = {
    Scope = "Demo"
  }
}

resource "azurerm_api_management_api" "demo" {
  name                  = "api${random_string.demo.result}"
  resource_group_name   = azurerm_resource_group.demo.name
  api_management_name   = azurerm_api_management.demo.name
  revision              = "1"
  display_name          = "API Tareas"
  protocols             = ["https"]
  subscription_required = false

}

resource "azurerm_api_management_api_operation" "demo" {
  operation_id        = "health-check"
  api_name            = azurerm_api_management_api.demo.name
  api_management_name = azurerm_api_management_api.demo.api_management_name
  resource_group_name = azurerm_api_management_api.demo.resource_group_name
  display_name        = "Health Check"
  method              = "GET"
  url_template        = "/api/helloworld"
  description         = "Chequea que la API esté funcionando"

  response {
    status_code = 200
  }
}

#resource "azurerm_api_management_api_operation" "terra-az-sqldb-tareas-get-all" {
#  operation_id        = "tareas-get-all"
#  api_name            = azurerm_api_management_api.terra-az-sqldb.name
#  api_management_name = azurerm_api_management_api.terra-az-sqldb.api_management_name
#  resource_group_name = azurerm_api_management_api.terra-az-sqldb.resource_group_name
#  display_name        = "Tareas GET"
#  method              = "GET"
#  url_template        = "/api/tarea"
#  description         = "Devuelve el listado de todas las tareas."
#
#  response {
#    status_code = 200
#  }
#}
#
#resource "azurerm_api_management_api_operation" "terra-az-sqldb-categorias-get-all" {
#  operation_id        = "categorias-get-all"
#  api_name            = azurerm_api_management_api.terra-az-sqldb.name
#  api_management_name = azurerm_api_management_api.terra-az-sqldb.api_management_name
#  resource_group_name = azurerm_api_management_api.terra-az-sqldb.resource_group_name
#  display_name        = "Categorías GET"
#  method              = "GET"
#  url_template        = "/api/categoria"
#  description         = "Devuelve el listado de todas las categorías."
#
#  response {
#    status_code = 200
#  }
#}



resource "azurerm_linux_web_app" "demo" {
  name                = "webapp${random_string.demo.result}"
  resource_group_name = azurerm_resource_group.demo.name
  location            = azurerm_service_plan.demo.location
  service_plan_id     = azurerm_service_plan.demo.id

  site_config {
    api_management_api_id = azurerm_api_management_api.demo.id
  }

  virtual_network_subnet_id = azurerm_subnet.demo.id

  connection_string {
    name  = "cnTareas"
    type  = "SQLAzure"
    value = "Server=tcp:${azurerm_mssql_server.demo.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.demo.name};User Id=${azurerm_mssql_server.demo.administrator_login};Password=${azurerm_mssql_server.demo.administrator_login_password};Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }

  tags = {
    Scope = "Demo"
  }
}