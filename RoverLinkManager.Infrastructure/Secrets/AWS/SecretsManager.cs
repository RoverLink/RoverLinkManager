using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Text.Json;

/*
 * The AWS CLI uses credentials and configuration settings located in multiple places, such as the system or
 * user environment variables, local AWS configuration files, or explicitly declared on the command line as a
 * parameter. Certain locations take precedence over others. The AWS CLI credentials and configuration settings
 * take precedence in the following order:
 *
 * Command line options – Overrides settings in any other location. You can specify --region, --output, and
 * --profile as parameters on the command line.
 *
 * Environment variables – You can store values in your system's environment variables.
 *
 * CLI credentials file – The credentials and config file are updated when you run the command aws configure. The
 * credentials file is located at ~/.aws/credentials on Linux or macOS, or at C:\Users\USERNAME\.aws\credentials on
 * Windows. This file can contain the credential details for the default profile and any named profiles.
 *
 * CLI configuration file – The credentials and config file are updated when you run the command aws configure. The
 * config file is located at ~/.aws/config on Linux or macOS, or at C:\Users\USERNAME\.aws\config on Windows. This
 * file contains the configuration settings for the default profile and any named profiles.
 *
 * Container credentials – You can associate an IAM role with each of your Amazon Elastic Container Service (Amazon ECS)
 * task definitions. Temporary credentials for that role are then available to that task's containers. For more
 * information, see IAM Roles for Tasks in the Amazon Elastic Container Service Developer Guide.
 *
 * Instance profile credentials – You can associate an IAM role with each of your Amazon Elastic Compute Cloud (Amazon EC2)
 * instances. Temporary credentials for that role are then available to code running in the instance. The credentials are
 * delivered through the Amazon EC2 metadata service. For more information, see IAM Roles for Amazon EC2 in the Amazon EC2
 * User Guide for Linux Instances and Using Instance Profiles in the IAM User Guide.
 */

namespace RoverLinkManager.Infrastructure.Secrets.AWS;

public static class SecretsManager
{
    public static T? GetSecret<T>(string name, string region)
    {
        var secret = GetSecret(name, region);

        return JsonSerializer.Deserialize<T>(secret);
    }

    public static string GetSecret(string name, string region)
    {
        string secret = "";

        MemoryStream memoryStream = new MemoryStream();

        IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

        GetSecretValueRequest request = new GetSecretValueRequest();
        request.SecretId = name;
        request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.

        GetSecretValueResponse response = null;

        // In this sample we only handle the specific exceptions for the 'GetSecretValue' API.
        // See https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
        // We rethrow the exception by default.

        try
        {
            response = client.GetSecretValueAsync(request).Result;
        }
        catch (DecryptionFailureException e)
        {
            // Secrets Manager can't decrypt the protected secret text using the provided KMS key.
            // Deal with the exception here, and/or rethrow at your discretion.
            throw;
        }
        catch (InternalServiceErrorException e)
        {
            // An error occurred on the server side.
            // Deal with the exception here, and/or rethrow at your discretion.
            throw;
        }
        catch (InvalidParameterException e)
        {
            // You provided an invalid value for a parameter.
            // Deal with the exception here, and/or rethrow at your discretion
            throw;
        }
        catch (InvalidRequestException e)
        {
            // You provided a parameter value that is not valid for the current state of the resource.
            // Deal with the exception here, and/or rethrow at your discretion.
            throw;
        }
        catch (ResourceNotFoundException e)
        {
            // We can't find the resource that you asked for.
            // Deal with the exception here, and/or rethrow at your discretion.
            throw;
        }
        catch (System.AggregateException ae)
        {
            // More than one of the above exceptions were triggered.
            // Deal with the exception here, and/or rethrow at your discretion.
            throw;
        }

        // Decrypts secret using the associated KMS key.
        // Depending on whether the secret is a string or binary, one of these fields will be populated.
        if (response.SecretString != null)
        {
            secret = response.SecretString;
        }
        else
        {
            memoryStream = response.SecretBinary;
            StreamReader reader = new StreamReader(memoryStream);
            secret = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
        }

        return secret;
    }
}