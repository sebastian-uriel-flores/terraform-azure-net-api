# terraform-azure-webapi

This is a simple project which works as the base for any Terraform Projects that create Infrastructure on Azure for REST APIs.

# Crea credenciales de autenticación

$SUBSCRIPTION='[subscription id]'
$LOCATION='[location of the resources]'

-- https://learn.microsoft.com/es-es/azure/developer/terraform/authenticate-to-azure?tabs=bash
-- https://github.com/marketplace/actions/azure-login#configure-a-service-principal-with-a-secret

az ad sp create-for-rbac --name "[service principal name]" --role contributor --scopes /subscriptions/ $SUBSCRIPTION --sdk-auth

-- https://learn.microsoft.com/en-us/powershell/module/az.accounts/Set-AzContext?view=azps-8.3.0
Set-AzContext -Subscription $SUBSCRIPTION

-- https://learn.microsoft.com/es-es/azure/developer/terraform/store-state-in-azure-storage?tabs=powershell
$RESOURCE_GROUP_NAME='[backend resource group name]'
$STORAGE_ACCOUNT_NAME='[backend storage account name]'
$CONTAINER_NAME='[backend storage container name]'

# Crea el resource group

New-AzResourceGroup -Name $RESOURCE_GROUP_NAME -Location $LOCATION

# Crea la storage account

$storageAccount = New-AzStorageAccount -ResourceGroupName $RESOURCE_GROUP_NAME -Name $STORAGE_ACCOUNT_NAME -SkuName Standard_LRS -Location $LOCATION -AllowBlobPublicAccess $true

# Crea un blob container
New-AzStorageContainer -Name $CONTAINER_NAME -Context $storageAccount.context -Permission blob

# Crea el Account Key

$ACCOUNT_KEY=(Get-AzStorageAccountKey -ResourceGroupName $RESOURCE_GROUP_NAME -Name $STORAGE_ACCOUNT_NAME)[0].value

# Outputs terraform:

-- https://www.terraform.io/cli/commands/output
-- https://docs.github.com/en/actions/using-jobs/defining-outputs-for-jobs
