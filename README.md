# Techreo Fullstack Challenge

Aplicación que incluye un Backend y un Fronted para gestionar cuentas de ahorro del cliente.

## Descripción general de arquitectura
![Alt text](images/Techreo-dev-fullstack-challenge-Overview.drawio.png)

## Pasos para clonar y ejecutar el proyecto en local

Sigue estos pasos para clonar y ejecutar el proyecto en tu máquina local:

1. Clona el repositorio:
    ```bash
    git clone https://github.com/leonardoalvarez20/techreo-challenge.git
    ```

2. Navega al directorio del proyecto:
    ```bash
    cd techreo-challenge
    ```

3. Instala las dependencias del proyecto Angular:
    ```bash
    cd techreo-challenge-web
    npm install
    ```

4. Instala las dependencias de ASP.NET Core:
    ```bash
    cd ../TechreoChallenge.API
    dotnet restore
    ```

5. Inicia el proyecto Angular:
    ```bash
    cd ../techreo-challenge-web
    ng serve
    ```

6. Inicia el proyecto ASP.NET Core:
    ```bash
    cd ../TechreoChallenge.API
    dotnet run
    ```

7. Abre tu navegador y navega a `http://localhost:4200/login` para ver el front-end Angular.

## Pasos para ejecutar el proyecto en Docker

Sigue estos pasos para ejecutar el proyecto usando Docker:

1. Clona el repositorio:
    ```bash
    git clone https://github.com/leonardoalvarez20/techreo-challenge.git
    ```

2. Navega al directorio del proyecto:
    ```bash
    cd techreo-challenge
    ```

3. Asegúrate de tener **Docker** y **Docker Compose** instalados en tu máquina. Puedes verificar las versiones instaladas con estos comandos:
    ```bash
    docker --version
    docker-compose --version
    ```

4. Construye las imágenes de Docker utilizando `docker-compose`:
    ```bash
    docker-compose build
    ```

5. Inicia los contenedores:
    ```bash
    docker-compose up
    ```

6. Verifica que los servicios están corriendo:
    - El proyecto Angular estará disponible en `http://localhost:4200`.
    - La API de ASP.NET Core estará disponible en `http://localhost:5000` o el puerto configurado en tu `docker-compose.yml`.

7. Para detener los contenedores:
    ```bash
    docker-compose down
    ```

# Instrucciones para Descargar y Ejecutar los contenedores desde GHCR


1. **Descargar la imagen del frontend**:

    ```bash
    docker pull ghcr.io/leonardoalvarez20/techreo-challenge-web:latest
    ```

2. **Descargar la imagen del backend**:

    ```bash
    docker pull ghcr.io/leonardoalvarez20/techreo-challenge-api:latest
    ```

3. **Descargar la imagen de MongoDB**:

    ```bash
    docker pull mongo:latest
    ```

## Pasos para Ejecutar los Contenedores

1. **Crear una Docker network**:

    ```bash
    docker network create techreo-challenge-network
    ```

2. **Ejecutar MongoDB**:

    Ejecuta el siguiente comando para agregar MongoDB a la red y permitir que el backend pueda conectarse a la base de datos:

    ```bash
    docker run -d \
      --network techreo-challenge-network \
      --name mongodb \
      -p 27017:27017 \
      -e MONGO_INITDB_DATABASE=banking_db \
      -e MONGO_INITDB_ROOT_USERNAME=banking_user \
      -e MONGO_INITDB_ROOT_PASSWORD=banking_password \
      mongo:latest
    ```

3. **Ejecutar la API (Backend)**:

    Ejecuta el contenedor para la API con el siguiente comando:

    ```bash
    docker run -d \
      --network techreo-challenge-network \
      --name techreo-challenge-api \
      -p 5014:5014 \
      -e JWT__KEY=F5BE22A679E35BA82F04D1427DBE56B8FC7301E529A1322110715467DA59E7CE \
      -e JWT__ISSUER=yourIssuer \
      -e JWT__AUDIENCE=yourAudience \
      -e MONGO__CONNECTIONSTRING=mongodb://mongodb:27017/banking_db \
      ghcr.io/leonardoalvarez20/techreo-challenge-api:latest
    ```

4. **Ejecutar la Aplicación Web (Frontend)**:

    Ejecuta el contenedor para la aplicación web con el siguiente comando:

    ```bash
    docker run -d \
      --network techreo-challenge-network \
      --name techreo-challenge-web \
      -p 4200:80 \
      ghcr.io/leonardoalvarez20/techreo-challenge-web:latest
    ```

## Verificar los Contenedores

Puedes verificar que los contenedores estén ejecutándose con el siguiente comando:

```bash
docker ps
```
## Acceder a la aplicación 

Haz click en el siguiente enlace para visualizar el sistema: `http://localhost:4200/login`