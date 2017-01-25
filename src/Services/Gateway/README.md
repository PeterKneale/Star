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

