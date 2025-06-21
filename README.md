# Levantar servicios con docker (MSSQL-Server, MongoDB, Apache)

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

## Levantar docker

1. Desde la raíz del proyecto:

   ```bash
   docker compose up -d
	```
