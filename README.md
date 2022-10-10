# Introduction

Welcome!

This project consists of a ToDo Web API created with the .NET 6 framework, that is connected to an [Azure SQL Database](https://learn.microsoft.com/en-us/azure/azure-sql/database/sql-database-paas-overview?view=azuresql) and deployed to an [Azure Web App](https://learn.microsoft.com/en-us/azure/app-service/overview) using [Terraform](https://www.terraform.io/).
Every time you push some changes to the __main__ branch, a GitHub Actions Workflow does the next jobs:

1. It updates the Azure infrastructure using Terraform.
2. Next, it compiles the API and deploys it to the created Azure Web App.
3. In the end, it runs a collection of automated tests created using the Postman testing tools.

As you can see, this project covers the basics of the Continuous Integration and Continuous Deployment of a cloud App based on Azure.
I wish it could be of help mainly for beginners, but also for the people who are used to work with other clouds or who wants to remember the basics of every part of a CI/CD workflow.


The project infrastructure is show in the next picture:

![Azure APIs](https://user-images.githubusercontent.com/5461235/192288380-81e254d4-7721-4071-957c-f6ab1f965eb6.png)


# How to use it

In the beginning, you have to fork the project. It is mainly because you will have to store some secrets and the way it is done in this project is using GitHub secrets. So, be sure of forking the project.

## 1. Setting up your Azure account
Next, you must create an Azure Account. You can check this link for more information about this step ([Create Azure Account](https://learn.microsoft.com/en-us/dotnet/azure/create-azure-account)).
If you don't know anything about Azure, I suggest you read the [Azure Fundamentals](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/ready/considerations/fundamental-concepts).

Once you have your account ready to use, I suggest you create a new subscription for all the resources that are going to be created, instead of using the default one. It will give you more control of your cloud environment and the costs associated.

To continue, you have to install Azure cli in your machine. Even if you have Azure PowerShell already installed, the Terraform Cli will request you to install Azure Cli, so it is a mandatory step. 

To install Azure Cli, run the next command in a terminal session:

```shell
winget install -e --id Microsoft.AzureCLI
```
Next, you have to sign in into your Azure Account, using Azure Cli. Here, you have two options:

1. The first option is to sign in using your Web Browser. I recommend this option, because it lets you keep your terminal clean of sensitive information. The command to be executed is this:

```shell
az login
```

2. The second options is to include your credentials in the terminal command:

```shell
az login --user [alias@myCompany.com] -password [password]
```

Once you have signed in your Azure account, you have to indicate Azure Cli that you are working with the new Subscription, using the next command:

```shell
az account set --subscription [subscription name or id]
```

Next, you have to create a Service Principal to give GitHub Actions permissions over the recently created Subscription. You can do this by running the next script in a your Terminal session:

```shell
az ad sp create-for-rbac --name "[service principal name]" --role contributor --scopes /subscriptions/[subscription id]
```

If the Service Principal has been created, the previous command will output the main information of the new Service Principal:
```json
{
  "appId": "[application id]",
  "displayName": "[service principal name]",
  "password": "[client secret]",
  "tenant": "[tenant id]"
}
``` 

You will have to store the Service Principal settings as GitHub Secrets, with the following names:
```shell
ARM_CLIENT_ID: [application id]
ARM_CLIENT_SECRET: [client secret]
ARM_SUBSCRIPTION_ID: [subscription id]
ARM_TENANT_ID: [tenant id]
```

### Creating the Terraform backend in Azure

Now, you have to create a Resource Group in your Subscription and a Storage Account inside of it, that will be used by the Terraform client as the backend.
You can do it by running the next scripts in your terminal session:

```shell
# 1. Create the Resource Group in your current Subscription
az group create --name "[resource group name] --location "[location]"
  
# Create the Storage Account
az storage account create --resource-group [resource group name] --name [storage account name] --location [location] --sku Standard_LRS --encryption-services blob

$accountKey=$(az storage account keys list --account-name "[storage account name]" --query "[?permissions == 'FULL'].[value][0]" --output tsv)

# Create a Container inside of the Storage Account
az storage container create --name "[container name]" --account-name "[storage account name]"

# Retrieve the Account Key of the Storage Account
# You will have to store this value as a GitHub Secret
$storage_account_key=(az storage account keys list --resource-group [resource group name] --account-name [storage account name] --query '[0].value' -o tsv)
```

The last step in this section requires you to indicate to the Terraform Client where to store the State of your Infrastructure. To do this, you have to store your recently obtained **Storage Account Key** as a GitHub Secret with the name `ARM_ACCOUNT_KEY`. Next, you have to open the file */Infrastructure/main.tf* and update the section **backend**, to match the current settings of your Storage Account:

```terraform
backend "azurerm" {
    resource_group_name  = "[resource group name used for the Terraform backend]"
    storage_account_name = "[storage account name used for the Terraform backend]"
    container_name       = "[storage container name used for the Terraform backend]"
    key                  = "terraform.tfstate"
  }
```

## 2. Setting up your Terraform client in GitHub Actions

Before the GitHub Actions workflow can issue a Terraform Client, you must create a Terraform API Token.
To do this, first create a Terraform Cloud Account, following the steps in this [Terraform Tutorial](https://learn.hashicorp.com/tutorials/terraform/cloud-sign-up?in=terraform/cloud-get-started#create-an-account).
Next, sign in to your Terraform Cloud Account, and go to the following link to create an API Token: [API Tokens](https://app.terraform.io/app/settings/tokens)
Now, touch the **Create API Token**  button and write a name for the API Token:
![image](https://user-images.githubusercontent.com/5461235/192148683-eb844f9c-1c3d-4e01-9722-cb2dc220fcbb.png)
![image](https://user-images.githubusercontent.com/5461235/192148716-84c1c8ad-aed5-4fd4-a3c6-96d8480ece2f.png)


Next, you will see a Token like this:

![image](https://user-images.githubusercontent.com/5461235/192148763-aff20712-1023-4805-97fe-b84ab2a12e45.png)

You have to copy this Token and store it as a new GitHub Secret with the name **TF_API_TOKEN**.

## 3. Latest settings

I have to request you create two more extra GitHub Secrets. They are intended to be used in the creation of the Azure SQL Database:

```shell
SQL_SERVER_ADMIN_USERNAME: [The username of your Azure SQL Database]
SQL_SERVER_ADMIN_PASSWORD: [The password of your Azure SQL Database]
```

## 4. Verify the Creations
In the end, you should have the following things created:

**Azure:**
- A dedicated Subscription for all the Resources.
- A dedicated Resource Group inside the Subscription, for the Terraform backend.
- A Storage Account inside the Resource Group, to be used by the Terraform client.
- A Blob Container inside the Storage Account, to store the Terraform client state.
- A Service Principal in your Tenant who give GitHub Actions the permissions to interact with your dedicated Azure Subscription.
- The **backend** section of the */Infrastructure/main.tf* Terraform file updated with the settings of the Storage Account used for the Terraform backend.

**Terraform Cloud:**
-  A Terraform API Token.

**GitHub Secrets:**
- **ARM_CLIENT_ID:** [clientId]
- **ARM_CLIENT_SECRET:** [clientSecret]
- **ARM_SUBSCRIPTION_ID:** [subscriptionID]
- **ARM_TENANT_ID:** [tenantId]
- **ARM_ACCOUNT_KEY**: [The Account Key of the Storage Account created for the Terraform Backend]
- **TF_API_TOKEN**: [Terraform Cloud API Token]
- **SQL_SERVER_ADMIN_USERNAME:** [The username of your Azure SQL Database]
- **SQL_SERVER_ADMIN_PASSWORD:** [The password of your Azure SQL Database]
---

# Useful links

- [Azure Fundamentals](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/ready/considerations/fundamental-concepts)
- [Create Azure Account](https://learn.microsoft.com/en-us/dotnet/azure/create-azure-account)
- [Manage Azure Subscriptions in Azure Cli](https://learn.microsoft.com/en-us/cli/azure/manage-azure-subscriptions-azure-cli)
- [Create an Azure service principal with Azure Cli](https://learn.microsoft.com/en-us/cli/azure/create-an-azure-service-principal-azure-cli)
- [Create Azure Storage Account with Azure Cli](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-cli)
- [Manage Azure Blob Containers with Azure Cli](https://learn.microsoft.com/en-us/azure/storage/blobs/blob-containers-cli)
- [Terraform Authenticate to Azure](https://learn.microsoft.com/es-es/azure/developer/terraform/authenticate-to-azure?tabs=bash) 
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
