# terraform-azure-webapi
This is a simple project which works as the base for any Terraform Projects that create Infrastructure on Azure for REST APIs 


1. Create Service principal
 az ad sp create-for-rbac --name {Service Principal Name} --role contributor \
                            --scopes /subscriptions/{ Subscription Id }/resourceGroups/{ Resource Group Name } \
                            --sdk-auth
                            
https://github.com/marketplace/actions/azure-login#configure-a-service-principal-with-a-secret

3. 
