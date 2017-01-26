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

```
eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJzaW1wbGljYXRlLm5ldCIsImlhdCI6MTQ4NTQyOTM0NCwiZXhwIjoxNTE2OTY1MzQ0LCJhdWQiOiJzaW1wbGljYXRlLm5ldCIsInN1YiI6InBldGVya25lYWxlQGdtYWlsLmNvbSIsIkdpdmVuTmFtZSI6IlBldGVyIiwiU3VybmFtZSI6IktuZWFsZSIsIkVtYWlsIjoicGV0ZXJrbmVhbGVAZ21haWwuY29tIiwiUm9sZSI6WyJNYW5hZ2VyIiwiQWRtaW5pc3RyYXRvciJdLCJVc2VySWQiOiI5ZGY2ZTY5ZjFhM2M0YmUzYTEzMGI1MTc3MzZlZGI3NyIsIkFjY291bnRJZCI6ImI1NWRhMjA2N2E1MTRkODNiNmQxMmNiMzg1MjQwNWUxIn0.Nz4ytcLgyz9QlBgPId480Nra4DfvY2Egkzvo1b9TNAQ`
```
- With userid in given_name  and accountid in family_name 
```
eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJzaW1wbGljYXRlLm5ldCIsImlhdCI6MTQ4NTQyOTM0NCwiZXhwIjoxNTE2OTY1MzQ0LCJhdWQiOiJzaW1wbGljYXRlLm5ldCIsInN1YiI6InBldGVya25lYWxlQGdtYWlsLmNvbSIsImdpdmVuX25hbWUgIjoiOWRmNmU2OWYxYTNjNGJlM2ExMzBiNTE3NzM2ZWRiNzciLCJmYW1pbHlfbmFtZSAiOiJiNTVkYTIwNjdhNTE0ZDgzYjZkMTJjYjM4NTI0MDVlMSIsIkVtYWlsIjoicGV0ZXJrbmVhbGVAZ21haWwuY29tIiwiUm9sZSI6WyJNYW5hZ2VyIiwiQWRtaW5pc3RyYXRvciJdLCJVc2VySWQiOiI5ZGY2ZTY5ZjFhM2M0YmUzYTEzMGI1MTc3MzZlZGI3NyIsIkFjY291bnRJZCI6ImI1NWRhMjA2N2E1MTRkODNiNmQxMmNiMzg1MjQwNWUxIn0.tBIL_4T-H25Rx5HQPGNv4M8MFu0TzoRzgIk_UZOFzxE
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

## Get an account

```
curl --request GET \
  --verbose \
  --url http://localhost/account \
  --header 'accept: application/json' \
  --header 'authorization: Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJzaW1wbGljYXRlLm5ldCIsImlhdCI6MTQ4NTQyOTM0NCwiZXhwIjoxNTE2OTY1MzQ0LCJhdWQiOiJzaW1wbGljYXRlLm5ldCIsInN1YiI6InBldGVya25lYWxlQGdtYWlsLmNvbSIsImdpdmVuX25hbWUgIjoiOWRmNmU2OWYxYTNjNGJlM2ExMzBiNTE3NzM2ZWRiNzciLCJmYW1pbHlfbmFtZSAiOiJiNTVkYTIwNjdhNTE0ZDgzYjZkMTJjYjM4NTI0MDVlMSIsIkVtYWlsIjoicGV0ZXJrbmVhbGVAZ21haWwuY29tIiwiUm9sZSI6WyJNYW5hZ2VyIiwiQWRtaW5pc3RyYXRvciJdLCJVc2VySWQiOiI5ZGY2ZTY5ZjFhM2M0YmUzYTEzMGI1MTc3MzZlZGI3NyIsIkFjY291bnRJZCI6ImI1NWRhMjA2N2E1MTRkODNiNmQxMmNiMzg1MjQwNWUxIn0.tBIL_4T-H25Rx5HQPGNv4M8MFu0TzoRzgIk_UZOFzxE' 
```
