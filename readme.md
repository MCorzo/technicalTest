# Technical Test 

This current project can easily build and run following the next steps:

[^1]: sdfsdf
[^2]: sdfsdf dfgfs dfsd fsdf sdfsdfsd fsdf sdfsdfs



docker build . -t technicaltest

docker run -it -p 8000:80 technicaltest

For testing purpose and avoid further setup or configuration the project don't work with https, full explation for how to setup and work with dev certificates in .net 6 consult this [link](https://github.com/dotnet/dotnet-docker/blob/main/samples/run-aspnetcore-https-development.md#linux) and run the container with this command : 
```
docker run -it -p 8000:80 \
-p 8001:443 \
-e ASPNETCORE_HTTPS_PORT=8001 \
-e ASPNETCORE_URLS="http://+;https://+" \
-e ASPNETCORE_ENVIRONMENT=Production \
-v ${HOME}/.microsoft/usersecrets/:/root/.microsoft/usersecrets \
-v ${HOME}/.aspnet/https:/root/.aspnet/https/ technicaltest
```









