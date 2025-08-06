#!/bin/bash

# Nombre de la base de datos
DB="frostbase"

mongosh --eval "db.getSiblingDB('frostbase').dropDatabase()"

# Hacer backup
mongorestore --db frostbase start*

