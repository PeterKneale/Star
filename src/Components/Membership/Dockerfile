FROM microsoft/dotnet:latest

COPY . /app
WORKDIR /app/Services.Membership
RUN ["dotnet", "restore"]

EXPOSE 8080
ENTRYPOINT ["dotnet", "run"]