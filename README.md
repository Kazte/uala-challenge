# Uala Challenge

- [Uala Challenge](#uala-challenge)
  - [Documentacion](#documentacion)
  - [Instrucciones](#instrucciones)
    - [Programas necesarios](#programas-necesarios)
    - [Ejecutar la aplicacion](#ejecutar-la-aplicacion)
    - [Entrar a la base de datos](#entrar-a-la-base-de-datos)
    - [Copiar estructura y datos](#copiar-estructura-y-datos)
  - [Endpoints](#endpoints)
    - [Users](#users)
      - [Register](#register)
      - [Login](#login)
    - [Tweets](#tweets)
      - [Create Tweet](#create-tweet)
      - [Get Tweet By Id](#get-tweet-by-id)
      - [Delete Tweet](#delete-tweet)
    - [Follows](#follows)
      - [Follow](#follow)
      - [Unfollow](#unfollow)
    - [Timeline](#timeline)
      - [Get Timeline](#get-timeline)

## Documentacion

La documentacion de la aplicacion se encuentra en la wiki del repositorio.

## Instrucciones

### Programas necesarios

- Docker Desktop.
- PgAdmin o DBeaver (o alguna app para entrar a una base de datos postgresql).
- Postman o algun otro API Client.
- Visual Studio
- .NET 8

### Ejecutar la aplicacion

Abrir la solucion con Visual Studio, corroborar que el startup item este `docker-compose` y darle a play.
> [!IMPORTANT]  
> Antes de darle a play tener `Docker Desktop` abierto.

![image](https://github.com/user-attachments/assets/05dddbd4-0410-4835-bd8c-b687361e166e)

### Entrar a la base de datos

Conexion:

- Url: `jdbc:postgresql://localhost:5432/ualax-database`
- Username: `ualax-admin`
- Password: `ualax-password`
- Host: `localhost`
- Port: `5432`
- Database: `ualax-database`

### Copiar estructura y datos

Ejecutar lo que se encuentra en `database.sql` en el DBeaver para crear las estructuras y copiar algunos datos de prueba.

## Endpoints

> [!IMPORTANT]  
> El port de la URL base puede cambiar, una vez iniciada la aplicacion copiar el port del Swagger.

### Users

#### Register

```bash
curl -X POST 'https://localhost:52358/api/user/register' \
  --header 'accept: */*' \
  --header 'Content-Type: application/json' \
  --data-raw '{
    "userName": ""
  }'
```

Body:
```json
{
  "userName": "string"
}
```

#### Login

```bash
curl -X POST 'https://localhost:52358/api/user/login' \
  --header 'accept: */*' \
  --header 'Content-Type: application/json' \
  --data-raw '{
    "userName": ""
  }'
```

Body:
```json
{
  "userName": "string"
}
```

---

### Tweets

#### Create Tweet

```bash
curl -X POST 'https://localhost:52358/api/tweet' \
  --header 'Content-Type: application/json' \
  --data-raw '{
    "content": ""
  }'
```

Body:

```json
{
  "content": "string"
}
```

#### Get Tweet By Id

```bash
curl -X GET 'https://localhost:52358/api/tweet/{id}'
```

#### Delete Tweet

```bash
curl -X DELETE 'https://localhost:52358/api/tweet/{id}'
```

---

### Follows

#### Follow

```bash
curl -X POST 'https://localhost:52358/api/follow' \
  --header 'Content-Type: application/json' \
  --data-raw '{
    "followedId": 0
  }'
```

Body:

```json
{
  "followedId": 0
}
```

#### Unfollow

```bash
curl -X DELETE 'https://localhost:52358/api/follow' \
  --header 'Content-Type: application/json' \
  --data-raw '{
    "followedId": 0
  }'
```

Body:

```json
{
  "followedId": 0
}
```

---

### Timeline

#### Get Timeline

```bash
curl -X GET 'https://localhost:52358/api/timeline' \
  --url-query 'cursor=' \
  --url-query 'limit=' \
  --header 'accept: */*'
```

Params:

- Cursor: Optional, String
- Limit: Optional, Number
  
