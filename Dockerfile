FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY ./target /app
# RUN
EXPOSE 5001
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://0.0.0.0:5001
CMD dotnet /app/Colipu.BasicData.API.WebApi.dll