# Building the packages
```
dotnet pack ../../Account/Services.Account.Models -o packages
dotnet pack ../../User/Services.User.Models -o packages
dotnet pack ../../Membership/Services.Membership.Models -o packages
```

# Building the code
```
dotnet restore -f packages
dotnet build
dotnet test
dotnet run
```

# Building a Docker Image
```
docker build -t gateway .
docker tag gateway:latest AWS_ACCOUNT_ID.dkr.ecr.ap-southeast-1.amazonaws.com/gateway:latest
docker push AWS_ACCOUNT_ID.dkr.ecr.ap-southeast-1.amazonaws.com/gateway:latest
```

# Sample Jwt

Sample JWT is built using 
- http://jwtbuilder.jamiekurtz.com/

Values used:
- Key (HS256): `qwertyuiopasdfghjklzxcvbnm123456`
- Key (Base 64 Encoded): `cXdlcnR5dWlvcGFzZGZnaGprbHp4Y3Zibm0xMjM0NTY=`

- JWT Decoded:

```
{
  "iss": "simplicate.net",
  "iat": 1485429344,
  "exp": 1516965344,
  "aud": "simplicate.net",
  "sub": "peterkneale@gmail.com",
  "GivenName": "Peter",
  "Surname": "Kneale",
  "Email": "peterkneale@gmail.com",
  "Role": [
    "Manager",
    "Administrator"
  ],
  "UserId": "0c0e0839-8485-4f3a-abef-3784e33eadee",
  "AccountId": "76b101be-9ba9-4bdc-a84b-b292a44020bc"
}
```

- JWT Encoded:
- With account id in email field because i cant figure out how to get custom payloads to work


```
eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJzaW1wbGljYXRlLm5ldCIsImlhdCI6MTQ4NTQyOTM0NCwiZXhwIjoxNTE2OTY1MzQ0LCJhdWQiOiJzaW1wbGljYXRlLm5ldCIsInN1YiI6InBldGVya25lYWxlQGdtYWlsLmNvbSIsImdpdmVuX25hbWUgIjoicGV0ZXIiLCJmYW1pbHlfbmFtZSAiOiJrbmVhbGUiLCJFbWFpbCI6ImI1NWRhMjA2N2E1MTRkODNiNmQxMmNiMzg1MjQwNWUxIiwiUm9sZSI6WyJNYW5hZ2VyIiwiQWRtaW5pc3RyYXRvciJdLCJVc2VySWQiOiI5ZGY2ZTY5ZjFhM2M0YmUzYTEzMGI1MTc3MzZlZGI3NyIsIkFjY291bnRJZCI6ImI1NWRhMjA2N2E1MTRkODNiNmQxMmNiMzg1MjQwNWUxIn0.V1ZHA4wX6w3pfQ9oaEtKglgjAMuhTZ-JgnaCyC89Y3Q
```

## Create an account

```
curl --request POST \
  --verbose \
  --url http://localhost/account \
  --header 'accept: application/json' \
  --header 'content-type: application/json' \
  --data '{"Name" : "simplicate","FirstName" : "peter","LastName" : "kneale"}'
  ```
# Login

```
curl --request POST \
  --verbose \
  --url http://localhost/authenticate \
  --header 'accept: application/json' \
  --header 'content-type: application/json' \
  --data '{"Username":"X", "Password":"U"}'
```

## Get current account

```
curl --request GET \
 --verbose \
 --url http://localhost/account \
 --header 'accept: application/json' \
 --header 'authorization: Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJzaW1wbGljYXRlLm5ldCIsImlhdCI6MTQ4NTQyOTM0NCwiZXhwIjoxNTE2OTY1MzQ0LCJhdWQiOiJzaW1wbGljYXRlLm5ldCIsInN1YiI6InBldGVya25lYWxlQGdtYWlsLmNvbSIsImdpdmVuX25hbWUgIjoicGV0ZXIiLCJmYW1pbHlfbmFtZSAiOiJrbmVhbGUiLCJFbWFpbCI6ImI1NWRhMjA2N2E1MTRkODNiNmQxMmNiMzg1MjQwNWUxIiwiUm9sZSI6WyJNYW5hZ2VyIiwiQWRtaW5pc3RyYXRvciJdLCJVc2VySWQiOiI5ZGY2ZTY5ZjFhM2M0YmUzYTEzMGI1MTc3MzZlZGI3NyIsIkFjY291bnRJZCI6ImI1NWRhMjA2N2E1MTRkODNiNmQxMmNiMzg1MjQwNWUxIn0.V1ZHA4wX6w3pfQ9oaEtKglgjAMuhTZ-JgnaCyC89Y3Q' 
```

## Get current user
```
curl --request GET \
 --verbose \
 --url http://localhost/user \
 --header 'accept: application/json' \
 --header 'authorization: Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJzaW1wbGljYXRlLm5ldCIsImlhdCI6MTQ4NTQyOTM0NCwiZXhwIjoxNTE2OTY1MzQ0LCJhdWQiOiJzaW1wbGljYXRlLm5ldCIsInN1YiI6InBldGVya25lYWxlQGdtYWlsLmNvbSIsImdpdmVuX25hbWUgIjoicGV0ZXIiLCJmYW1pbHlfbmFtZSAiOiJrbmVhbGUiLCJFbWFpbCI6ImI1NWRhMjA2N2E1MTRkODNiNmQxMmNiMzg1MjQwNWUxIiwiUm9sZSI6WyJNYW5hZ2VyIiwiQWRtaW5pc3RyYXRvciJdLCJVc2VySWQiOiI5ZGY2ZTY5ZjFhM2M0YmUzYTEzMGI1MTc3MzZlZGI3NyIsIkFjY291bnRJZCI6ImI1NWRhMjA2N2E1MTRkODNiNmQxMmNiMzg1MjQwNWUxIn0.V1ZHA4wX6w3pfQ9oaEtKglgjAMuhTZ-JgnaCyC89Y3Q' 
```

## Get current user
```
curl --request GET \
 --verbose \
 --url http://localhost/users \
 --header 'accept: application/json' \
 --header 'authorization: Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJzaW1wbGljYXRlLm5ldCIsImlhdCI6MTQ4NTQyOTM0NCwiZXhwIjoxNTE2OTY1MzQ0LCJhdWQiOiJzaW1wbGljYXRlLm5ldCIsInN1YiI6InBldGVya25lYWxlQGdtYWlsLmNvbSIsImdpdmVuX25hbWUgIjoicGV0ZXIiLCJmYW1pbHlfbmFtZSAiOiJrbmVhbGUiLCJFbWFpbCI6ImI1NWRhMjA2N2E1MTRkODNiNmQxMmNiMzg1MjQwNWUxIiwiUm9sZSI6WyJNYW5hZ2VyIiwiQWRtaW5pc3RyYXRvciJdLCJVc2VySWQiOiI5ZGY2ZTY5ZjFhM2M0YmUzYTEzMGI1MTc3MzZlZGI3NyIsIkFjY291bnRJZCI6ImI1NWRhMjA2N2E1MTRkODNiNmQxMmNiMzg1MjQwNWUxIn0.V1ZHA4wX6w3pfQ9oaEtKglgjAMuhTZ-JgnaCyC89Y3Q' 
```
