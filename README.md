# web

# RoverLink Manager 

[![](https://raw.githubusercontent.com/ServiceStack/Assets/master/csharp-templates/web.png)](http://web.web-templates.io/)

### AWS Setup

Install AWS Cli tools
msiexec.exe /i https://awscli.amazonaws.com/AWSCLIV2.msi

Create User profile for Secrets
https://us-east-1.console.aws.amazon.com/iamv2/home#/users

Navigate to "User groups" and create a group named "SecretAccess" with the Permissions "SecretsManagerReadWrite"
Create user and add them to the "SecretAccess" group, make sure you save the security credentials (access key and secret access key)

If needed, create access key and secret access key: https://us-east-1.console.aws.amazon.com/iam/home#/users/vs?section=security_credentials

Restart cmd and run:
aws configure

Set access key and secret access key for a user you created in AWS
Default region should be us-east-1

### Update Server TypeScript DTOs

Run the dtos package.json script to update your server dtos:

    $ x scripts dtos
