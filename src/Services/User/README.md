# Building the code
dotnet restore
dotnet build
dotnet test
dotnet run

# Building a Docker Image
docker build -t user .
docker tag user:latest AWS_ACCOUNT_ID.dkr.ecr.ap-southeast-1.amazonaws.com/user:latest
docker push AWS_ACCOUNT_ID.dkr.ecr.ap-southeast-1.amazonaws.com/user:latest

