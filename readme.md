# Building & Runing the project
This project can be easily build and run following the next steps:
1. Build the container image
 To build the image run this command on the terminal on the solution folder
	```
	docker build . -t technicaltest
	```
2. Run the container 
 Run the container image executing this command on terminal
	```
	docker run -d -p 8000:80 technicaltest
	```
    After the application starts, navigate to [http://localhost:8000/swagger](http://localhost:8000/swagger) in your web browser to test the service using the **Swagger UI** interface or using any other tool, for example run the next command on the terminal to test the **get** method using **curl** :

    ```
    curl -X 'GET' 'http://localhost:8000/Geolocation'
    ```
## Note
For testing purpose and avoid aditional setups or configurations the project don't work with https. Full explanation for how to setup and work with dev certificates in .net 6 consult this [link](https://github.com/dotnet/dotnet-docker/blob/main/samples/run-aspnetcore-https-development.md#linux) and run the container with this command :

    
    docker run -it -p 8000:80 \
    -p 8001:443 \
    -e ASPNETCORE_HTTPS_PORT=8001 \
    -e ASPNETCORE_URLS="http://+;https://+" \
    -e ASPNETCORE_ENVIRONMENT=Production \
    -v ${HOME}/.microsoft/usersecrets/:/root/.microsoft/usersecrets \
    -v ${HOME}/.aspnet/https:/root/.aspnet/https/ technicaltest
    


