FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet tool install --global dotnet-watch
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /src

ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:5121;https://0.0.0.0:5121"]
