using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Core.Validators
{
    public class SimpleSecretValidator : ISecretValidator
    {
        public Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
        {

            if (parsedSecret.Credential.GetType().Name != "String")
            {
                return Task.FromResult(new SecretValidationResult
                {
                    Success = false,
                    Error = "Invalid data type",
                    ErrorDescription = "Secret should only be of type String"
                });
            }


            foreach (Secret secret in secrets)
            {
                if (secret.Value == (string)parsedSecret.Credential && secret.Type == parsedSecret.Type)
                {
                    return Task.FromResult(new SecretValidationResult
                    {
                        Success = true,

                    });
                }
            }
            return Task.FromResult(new SecretValidationResult
            {
                Success = false,
                Error = "Invalid credentials",
                ErrorDescription = "Id or secret is invalid"
            });
        }
    }
}
