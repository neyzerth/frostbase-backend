# Levantar las bd con Docker

## Instalar docker

### En Linux:

- Docker:

  ```bash
  sudo apt install docker.io docker-compose -y
  sudo systemctl enable docker
  sudo systemctl start docker
  ```

- Agrega tu usuario al grupo docker (para evitar usar `sudo`):

  ```bash
  sudo usermod -aG docker $USER
  ```

- Reinicia la sesión.

### En Windows:

- Instala **Docker Desktop** desde:
   https://www.docker.com/products/docker-desktop/
- Asegúrate de activar:
  - WSL2 backend
  - Soporte para Linux containers

## Levantar las bases de datos

1. Desde la raíz del proyecto:

   ```bash
   docker compose up -d
	```
