FROM mcr.microsoft.com/dotnet/sdk:8.0
COPY ./ /app
WORKDIR /app
RUN dotnet restore
RUN dotnet publish -c Release -o out
WORKDIR /app/out

EXPOSE 92

CMD ["dotnet", "Library.NotificationService2.dll"]
