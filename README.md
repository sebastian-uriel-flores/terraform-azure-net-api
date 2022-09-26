# Introduction

Welcome!

This project consists of a ToDo Web API created with the .net 6 framework, that is connected to an Azure SQL Database and deployed to an Azure Web App using Terraform.
Every time you push some changes to the __main__ branch, a GitHub Action does the next jobs:

1. It updates the Azure infrastructure using Terraform.
2. Next, it compiles the API and deploys it to the created Azure Web App.
3. In the end, it runs a collection of automated tests created using the Postman testing tools.

As you can see, this project covers the basics of the Continuous Integration and Continuous Deployment of a cloud App based on Azure.
I wish it could be of help mainly for beginners, but also for the people who are used to work with other clouds or who wants to remember the basics of every part of a CI/CD workflow.

![Azure APIs](https://user-images.githubusercontent.com/5461235/192288380-81e254d4-7721-4071-957c-f6ab1f965eb6.png)


# How to use it

In the beginning, you have to fork the project. It is mainly because you will have to store some secrets and the way it is done in this project is using GitHub secrets. So, be sure of forking the project.

## 1. Setting up your Azure account
Next, you must create an Azure Account. You can check this link for more information about this step ([Create Azure Account](https://learn.microsoft.com/en-us/dotnet/azure/create-azure-account)).
If you don't know anything about Azure, I suggest you read the [Azure Fundamentals](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/ready/considerations/fundamental-concepts).

Once you have your account ready to use, I suggest you create a new subscription for all the resources we need, instead of using the default one. It will give you more control of your cloud environment and the costs associated.

Next, you have to create a Service Principal to give GitHub Actions permissions over the recently created subscription. You can do this by running the next script in a PowerShell terminal:

```powershell
$SUBSCRIPTION='[subscription id]'
$SERVICE_PRINCIPAL_NAME='[name of the service principal]'

# Create the Service Principal
az ad sp create-for-rbac --name $SERVICE_PRINCIPAL_NAME --role contributor --scopes /subscriptions/ $SUBSCRIPTION --sdk-auth
```

Once the Service Principal has been created, you will see that the terminal outputs a JSON similar to the next one:

```powershell
{
    "clientId": "<GUID>",
    "clientSecret": "<STRING>",
    "subscriptionId": "<GUID>",
    "tenantId": "<GUID>"
    (...)
}
```

As you can see, it contains some important values, that we are going to use in the next steps. Please, be sure of storing as GitHub Secrets, using the following names:

- **ARM_CLIENT_ID:** [clientId]
- **ARM_CLIENT_SECRET:** [clientSecret]
- **ARM_SUBSCRIPTION_ID:** [subscriptionID]
- **ARM_TENANT_ID:** [tenantId]

To indicate to your machine that you are working with the new Subscription, you have to set it as your *current Azure context*, running the following script:
```powershell
Set-AzContext -Subscription $SUBSCRIPTION
```

### Creating the Terraform backend in Azure
Now, you have to create a Resource Group in your Subscription and a Storage Account inside of it, that will be used by the Terraform client as the backend.
You can do it by running the next scripts in the Powershell terminal we were working on before:

```powershell
$RESOURCE_GROUP_NAME='[backend resource group name]'
$STORAGE_ACCOUNT_NAME='[backend storage account name]'
$CONTAINER_NAME='[backend storage container name]'
$LOCATION='[location of the resources]'

# Creates the Storage Account
$storageAccount = New-AzStorageAccount -ResourceGroupName $RESOURCE_GROUP_NAME -Name $STORAGE_ACCOUNT_NAME -SkuName Standard_LRS -Location $LOCATION -AllowBlobPublicAccess $true

# Creates a Container inside of the Storage Account
New-AzStorageContainer -Name $CONTAINER_NAME -Context $storageAccount.context -Permission blob

# Outputs Account Key of the Storage Account
$STORAGE_ACCOUNT_KEY=(Get-AzStorageAccountKey -ResourceGroupName $RESOURCE_GROUP_NAME -Name $STORAGE_ACCOUNT_NAME)[0].value
```

To continue, you must create three GitHub Secrets, to store the new Resource Group Name, the Storage Account Name, and the Storage Account Key, with the following names:
- **ARM_ACCOUNT_KEY**: [The Account Key of the Storage Account created for the Terraform Backend]
- **TF_BACKEND_RESOURCE_GROUP_NAME**: [The Resource Group Name created for the Terraform Backend]
- **TF_BACKEND_STORAGE_ACCOUNT_NAME**: [The Name of the Storage Account created for the Terraform Backend]

## 2. Setting up your Terraform client in GitHub Actions

Before the GitHub Actions workflow can issue a Terraform Client, you must create a Terraform API Token.
To do this, first create a Terraform Cloud Account, following the steps in this [Terraform Tutorial](https://learn.hashicorp.com/tutorials/terraform/cloud-sign-up?in=terraform/cloud-get-started#create-an-account).
Next, Sign In to your Terraform Cloud Account, and go to the following link to create an API Token: [API Tokens](https://app.terraform.io/app/settings/tokens)
Now, touch the **Create API Token**  button and write a name for the API Token:
![image](https://user-images.githubusercontent.com/5461235/192148683-eb844f9c-1c3d-4e01-9722-cb2dc220fcbb.png)
![image](https://user-images.githubusercontent.com/5461235/192148716-84c1c8ad-aed5-4fd4-a3c6-96d8480ece2f.png)


Next, you will see a Token like this:

![image](https://user-images.githubusercontent.com/5461235/192148763-aff20712-1023-4805-97fe-b84ab2a12e45.png)

You have to copy this Token and store it as a new GitHub Secret with the name **TF_API_TOKEN**.

## 3. Latest settings

I have to request you create two more extra GitHub Secrets. They are intended to be used in the creation of the Azure SQL Database:
- **SQL_SERVER_ADMIN_USERNAME:** [The username of your Azure SQL Database]
- **SQL_SERVER_ADMIN_PASSWORD:** [The password of your Azure SQL Database]

## 4. Verify the Creations
In the end, you should have the following things created:

**Azure:**
- A dedicated Subscription for all the Resources.
- A dedicated Resource Group inside the Subscription, for the Terraform backend.
- A Storage Account inside the Resource Group, to be used by the Terraform client.
- A Blob Container inside the Storage Account, to store the Terraform client state.
- A Service Principal in your Tenant who give GitHub Actions the permissions to interact with your dedicated Azure Subscription.

**Terraform Cloud:**
-  A Terraform API Token.

**GitHub Secrets:**
- **ARM_CLIENT_ID:** [clientId]
- **ARM_CLIENT_SECRET:** [clientSecret]
- **ARM_SUBSCRIPTION_ID:** [subscriptionID]
- **ARM_TENANT_ID:** [tenantId]
- **ARM_ACCOUNT_KEY**: [The Account Key of the Storage Account created for the Terraform Backend]
- **TF_BACKEND_RESOURCE_GROUP_NAME**: [The Resource Group Name created for the Terraform Backend]
- **TF_BACKEND_STORAGE_ACCOUNT_NAME**: [The Name of the Storage Account created for the Terraform Backend]
- **TF_API_TOKEN**: [Terraform Cloud API Token]
- **SQL_SERVER_ADMIN_USERNAME:** [The username of your Azure SQL Database]
- **SQL_SERVER_ADMIN_PASSWORD:** [The password of your Azure SQL Database]
---

# Useful links

- [Azure Fundamentals](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/ready/considerations/fundamental-concepts)
- [Create Azure Account](https://learn.microsoft.com/en-us/dotnet/azure/create-azure-account)
- [Terraform Authenticate to Azure](https://learn.microsoft.com/es-es/azure/developer/terraform/authenticate-to-azure?tabs=bash) 
- [Configure a Service Principal with a Secret](https://github.com/marketplace/actions/azure-login#configure-a-service-principal-with-a-secret)
- [Set Azure Context in PowerShell](https://learn.microsoft.com/en-us/powershell/module/az.accounts/Set-AzContext?view=azps-8.3.0)
- [Store Terraform State in Azure Storage](https://learn.microsoft.com/es-es/azure/developer/terraform/store-state-in-azure-storage?tabs=powershell)
- [Terraform Cloud SignUp](https://app.terraform.io/public/signup/account)
- [Terraform Accounts and Tokens](https://www.terraform.io/cloud-docs/users-teams-organizations/users?_gl=1*1i5uqtc*_ga*NTQ2MjMzNTgyLjE2NjExNzMwNTg.*_ga_P7S46ZYEKW*MTY2NDExNTA2Mi4xNS4xLjE2NjQxMTU3MzUuMC4wLjA.#users)
- [Configure a Terraform Client in GitHub Actions](https://learn.hashicorp.com/tutorials/terraform/github-actions)
- [Using Terraform outputs](https://www.terraform.io/cli/commands/output)
- [GitHub Actions - Defining outputs for jobs](https://docs.github.com/en/actions/using-jobs/defining-outputs-for-jobs)

## Platzi Courses

- [Introduction to Azure](https://platzi.com/cursos/introduccion-azure/)
- [Storage in Azure](https://platzi.com/cursos/almacenamiento-azure/)
- [Web Apps and Logic Apps in Azure](https://platzi.com/cursos/web-apps/)
- [Azure Active Directory](https://platzi.com/cursos/azure-active-directory/)
- [Infrastructure as Code with Terraform](https://platzi.com/cursos/devops-terraform/)
- [Introduction to the Terminal](https://platzi.com/cursos/terminal/)
