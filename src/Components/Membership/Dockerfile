FROM microsoft/dotnet:1.1.0-sdk-projectjson

COPY . /app
WORKDIR /app/Services.Membership
RUN ["dotnet", "restore"]

EXPOSE 8080
ENTRYPOINT ["dotnet", "run"]