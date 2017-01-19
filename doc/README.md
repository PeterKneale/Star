Sample Jwt
- Key (HS256): qwertyuiopasdfghjklzxcvbnm123456 

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