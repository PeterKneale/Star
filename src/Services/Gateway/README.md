# Building the packages
dotnet pack ../../Account/Services.Account.Models -o packages
dotnet pack ../../User/Services.User.Models -o packages
dotnet pack ../../Membership/Services.Membership.Models -o packages

# Building the code
dotnet restore -f packages
dotnet build
dotnet test
dotnet run

# Building a Docker Image
docker build -t gateway .
docker tag gateway:latest AWS_ACCOUNT_ID.dkr.ecr.ap-southeast-1.amazonaws.com/gateway:latest
docker push AWS_ACCOUNT_ID.dkr.ecr.ap-southeast-1.amazonaws.com/gateway:latest

# Sample Jwt
- Key (HS256): qwertyuiopasdfghjklzxcvbnm123456 
- Key Base 64 Encoded: cXdlcnR5dWlvcGFzZGZnaGprbHp4Y3Zibm0xMjM0NTY=

- Decoded:
```
{
    "iss": "simplicate.net",
    "iat": 1484622010,
    "exp": 1516158010,
    "aud": "simplicate.net",
    "sub": "peterkneale",
    "GivenName": "peter",
    "Surname": "kneale",
    "Email": "peterkneale@gmail.com",
    "Role": [
        "manager",
        "administrator"
    ]
}
```

- Encoded:

```
eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJzaW1wbGljYXRlLm5ldCIsImlhdCI6MTQ4NDYyMjAxMCwiZXhwIjoxNTE2MTU4MDEwLCJhdWQiOiJzaW1wbGljYXRlLm5ldCIsInN1YiI6InBldGVya25lYWxlIiwiR2l2ZW5OYW1lIjoicGV0ZXIiLCJTdXJuYW1lIjoia25lYWxlIiwiRW1haWwiOiJwZXRlcmtuZWFsZUBnbWFpbC5jb20iLCJSb2xlIjpbIm1hbmFnZXIiLCJhZG1pbmlzdHJhdG9yIl19.XUDlMxIZB4YXuMWoF73ycsdQLSbC0z8qWWl0n7RFvMU
```