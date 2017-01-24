# Building the code
dotnet restore
dotnet build
dotnet test
dotnet run

# Building a Docker Image
docker build -t account .
docker tag account:latest AWS_ACCOUNT_ID.dkr.ecr.ap-southeast-1.amazonaws.com/account:latest
docker push AWS_ACCOUNT_ID.dkr.ecr.ap-southeast-1.amazonaws.com/account:latest

- Operations
  - CreateMember
    - Request - `{ UserId, AccountId }`
    - Reply - `{ }`
  - DeleteMember
    - Request - `{ UserId, AccountId }`
    - Reply - `{ }`
  - GetMembers
    - Request - `{ AccountId, Page, PageSize }`
    - Reply - `{ UserId[], Total }`
    
- Events
 - UserAddedToAccountEvent - `{ UserId, AccountId }`
 - UserRemovedFromAccountEvent - `{ UserId, AccountId }`

