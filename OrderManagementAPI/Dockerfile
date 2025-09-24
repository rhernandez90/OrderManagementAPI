    FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
    WORKDIR /src

    COPY ../*.sln ./
    COPY ./*.csproj ./
    RUN dotnet restore ./OrderManagementAPI.csproj

    COPY .. .

    RUN dotnet tool install --global dotnet-watch

    ENV PATH="$PATH:/root/.dotnet/tools"

    WORKDIR /src/OrderManagementAPI
    ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:5000;https://0.0.0.0:5001"]