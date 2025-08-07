#!/bin/bash

# Nombre de la base de datos
DB="frostbase"

# Directorio de respaldo con timestamp
BACKUP_DIR="backup_${DB}_$(date +%Y%m%d_%H%M%S)"

# Hacer backup de la base de datos
echo "Iniciando respaldo de la base de datos '$DB'..."
mongodump --db "$DB" --out "$BACKUP_DIR" > /dev/null 2>&1
echo "Respaldo completado en '$BACKUP_DIR'."

# Eliminar la base de datos
echo "Eliminando base de datos '$DB'..."
mongosh --quiet --eval "db.getSiblingDB('$DB').dropDatabase()" > /dev/null 2>&1
echo "Base de datos '$DB' eliminada."

# Recuperar los datos desde archivos que empiecen con 'start'
echo "Iniciando recuperación de datos en '$DB' desde archivos 'start*'..."
mongorestore --db "$DB" start* > /dev/null 2>&1
echo "Recuperación completada."

