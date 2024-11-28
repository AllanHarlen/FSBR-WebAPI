# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
EXPOSE 3006 

# Variáveis de Ambiente
ENV ASPNETCORE_ENVIRONMENT=Development

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiando os arquivos .csproj de cada projeto individualmente
COPY ["WebApis/WebApis.csproj", "WebApis/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Infraestructure/Infraestructure.csproj", "Infraestructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]

# Restaurando as dependências
RUN dotnet restore "WebApis/WebApis.csproj"

# Copiando todo o código-fonte para o contêiner
COPY . .

# Definindo o diretório de trabalho para a pasta do projeto WebApis e construindo o projeto
WORKDIR "/src/WebApis"
RUN dotnet build "WebApis.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebApis.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app

# Copia os arquivos de publicação e os certificados SSL para a imagem final
COPY --from=publish /app/publish .
COPY --from=base /app/ssl /app/ssl

ENTRYPOINT ["dotnet", "WebApis.dll"]
