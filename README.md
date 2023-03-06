


generate a file 'appsettings.Development.json' in the root of the project

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "BankConnection": "Server="your server",port;User="your user";Password="your password";DataBase="name database";Trusted_connection=false;TrustServerCertificate=true"
  },
  "JWT": {
    "Key": "your key token"
  }
}


```