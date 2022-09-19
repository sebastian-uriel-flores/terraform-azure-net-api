#
#output "database_name" {
#  description = "Database name of the Azure SQL Database created."
#  value       = azurerm_mssql_database.terra-az-sqldb.name
#}
#
#output "sql_server_name" {
#  description = "Server name of the Azure SQL Database created."
#  value       = azurerm_mssql_server.terra-az-sqldb.name
#}
#
#output "sql_server_location" {
#  description = "Location of the Azure SQL Database created."
#  value       = azurerm_mssql_server.terra-az-sqldb.location
#}
#
#output "sql_server_version" {
#  description = "Version the Azure SQL Database created."
#  value       = azurerm_mssql_server.terra-az-sqldb.version
#}
#
#output "sql_server_fqdn" {
#  description = "Fully Qualified Domain Name (FQDN) of the Azure SQL Database created."
#  value       = azurerm_mssql_server.terra-az-sqldb.fully_qualified_domain_name
#}
#
#output "azure_web_app" {
#  description = "Azure Web App URL"
#  value       = "${azurerm_linux_web_app.terra-az-sqldb.name}.azurewebsites.net"
#}

output "resource_name_suffix" {
  description = "Suffix of the name of every resource"
  value       = random_string.demo.result
}