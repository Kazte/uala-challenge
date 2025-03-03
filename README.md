# Uala Challenge

## Instrucciones

### Programas necesarios

- Docker Desktop.
- PgAdmin o DBeaver (o alguna app para entrar a una base de datos postgresql).
- Postman o algun otro API Client.
- Visual Studio
- .NET 8

### Ejecutar la aplicacion

Abrir la solucion con Visual Studio, corroborar que el startup item este `docker-compose` y darle a play.
Nota: Antes de darle a play tener docker compose abierto.

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
  
