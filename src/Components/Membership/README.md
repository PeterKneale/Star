# Building the code
dotnet restore
dotnet build
dotnet test
dotnet run

# Building a Docker Image
docker build -t membership .
docker tag account:latest AWS_ACCOUNT_ID.dkr.ecr.ap-southeast-1.amazonaws.com/account:latest
docker push AWS_ACCOUNT_ID.dkr.ecr.ap-southeast-1.amazonaws.com/account:latest

# API
- Operations
  - CreateMember
    - Request - `{ UserId, AccountId }`
    - Reply - `{ UserId, AccountId }`
  - DeleteMember
    - Request - `{ UserId, AccountId }`
    - Reply - `{ }`
  - GetMembers
    - Request - `{ AccountId }`
    - Reply - `{ UserId[] }`
    
- Events
 - UserAddedToAccountEvent - `{ UserId, AccountId }`
 - UserRemovedFromAccountEvent - `{ UserId, AccountId }`

