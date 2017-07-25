FROM microsoft/aspnetcore:1.1
ARG source
WORKDIR /app
ENV ASPNETCORE_URLS http://+:5103
EXPOSE 5103
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "VSTSDemo.Api.dll"]
