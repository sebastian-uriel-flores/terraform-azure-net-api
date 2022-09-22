# terraform-azure-webapi

This is a simple project which works as the base for any Terraform Projects that create Infrastructure on Azure for REST APIs.

Los pasos a seguir, luego de hacer fork al proyecto son los siguientes:

# 1. Crear credenciales de autenticación a los servicios de Azure

```powershell
$SUBSCRIPTION='[subscription id]'
$LOCATION='[location of the resources]'
```

Registrar la aplicación Github Actions como un Service Principal en Azure, para que este último le de permisos de ejecutar los procedimientos de Despliegue Continuo:

-- https://learn.microsoft.com/es-es/azure/developer/terraform/authenticate-to-azure?tabs=bash
-- https://github.com/marketplace/actions/azure-login#configure-a-service-principal-with-a-secret

```powershell
az ad sp create-for-rbac --name "[service principal name]" --role contributor --scopes /subscriptions/ $SUBSCRIPTION --sdk-auth
```

-- https://learn.microsoft.com/en-us/powershell/module/az.accounts/Set-AzContext?view=azps-8.3.0
Set-AzContext -Subscription $SUBSCRIPTION

-- https://learn.microsoft.com/es-es/azure/developer/terraform/store-state-in-azure-storage?tabs=powershell

```powershell
$RESOURCE_GROUP_NAME='[backend resource group name]'
$STORAGE_ACCOUNT_NAME='[backend storage account name]'
$CONTAINER_NAME='[backend storage container name]'
```

# Crea el resource group

```powershell
New-AzResourceGroup -Name $RESOURCE_GROUP_NAME -Location $LOCATION
```

# Crea la storage account

```powershell
$storageAccount = New-AzStorageAccount -ResourceGroupName $RESOURCE_GROUP_NAME -Name $STORAGE_ACCOUNT_NAME -SkuName Standard_LRS -Location $LOCATION -AllowBlobPublicAccess $true
```
# Crea un blob container
New-AzStorageContainer -Name $CONTAINER_NAME -Context $storageAccount.context -Permission blob

# Crea el Account Key

```powershell
$ACCOUNT_KEY=(Get-AzStorageAccountKey -ResourceGroupName $RESOURCE_GROUP_NAME -Name $STORAGE_ACCOUNT_NAME)[0].value
```

# Outputs terraform:

-- https://www.terraform.io/cli/commands/output
-- https://docs.github.com/en/actions/using-jobs/defining-outputs-for-jobs
