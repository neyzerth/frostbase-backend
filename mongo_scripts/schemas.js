// 1. Users Collection
db.createCollection("Users", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'first_name',
        'last_name',
        'email',
        'password',
        'is_admin',
        'active'
      ],
      properties: {
        _id: {
          bsonType: 'objectId',
          description: 'Identificador único del usuario'
        },
        first_name: {
          bsonType: 'string',
          description: 'Nombre de pila del usuario'
        },
        last_name: {
          bsonType: 'string',
          description: 'Apellido paterno del usuario'
        },
        middle_name: {
          bsonType: 'string',
          description: 'Apellido materno del usuario'
        },
        email: {
          bsonType: 'string',
          description: 'Correo electrónico del usuario'
        },
        phone: {
          bsonType: 'string',
          description: 'Número telefónico del usuario'
        },
        birthdate: {
          bsonType: 'date',
          description: 'Fecha de nacimiento del usuario'
        },
        password: {
          bsonType: 'string',
          minLength: 8,
          description: 'Contraseña para acceder al sistema'
        },
        IDTruckDefault: {
          bsonType: ['objectId', null],
          description: 'Camion asignado por defecto al conductor'
        },
        is_admin: {
          bsonType: 'bool',
          description: 'Indica si el usuario es administrador (true) o conductor (false)'
        },
        active: {
          bsonType: 'bool',
          description: 'Campo para eliminación lógica. Si es false, el registro está eliminado'
        }
      }
    }
  }
});

// 2. Trucks Collection
db.createCollection("Trucks", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'license_plate',
        'IDStateTruck'
      ],
      properties: {
        _id: {
          bsonType: 'objectId',
          description: 'Identificador único del camión'
        },
        brand: {
          bsonType: 'string',
          description: 'Marca del camión'
        },
        model: {
          bsonType: 'string',
          description: 'Modelo específico del camión'
        },
        license_plate: {
          bsonType: 'string',
          description: 'Placa de matrícula única del vehículo'
        },
        IDStateTruck: {
          bsonType: 'string',
          description: 'Referencia al código del estado del camión'
        }
      }
    }
  }
});

// 3. StateTruck Collection
db.createCollection("StateTruck", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'message'
      ],
      properties: {
        _id: {
          bsonType: 'string',
          description: 'Identificador de texto único del estado del camión'
        },
        message: {
          bsonType: 'string',
          description: 'Descripción específica del estado del camión'
        }
      }
    }
  }
});

// 4. Trips Collection
db.createCollection("Trips", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'start_time',
        'IDTruck',
        'IDUserDriver',
        'IDRoute',
        'IDStateTrip'
      ],
      properties: {
        _id: {
          bsonType: 'objectId',
          description: 'Identificador único del viaje'
        },
        start_time: {
          bsonType: 'date',
          description: 'Fecha y hora de inicio del viaje'
        },
        end_time: {
          bsonType: 'date',
          description: 'Fecha y hora de finalización del viaje'
        },
        IDTruck: {
          bsonType: 'objectId',
          description: 'Referencia al camión que realizó el viaje'
        },
        IDUserDriver: {
          bsonType: 'objectId',
          description: 'Referencia al conductor'
        },
        IDRoute: {
          bsonType: 'objectId',
          description: 'Referencia al identificador de la ruta utilizada'
        },
        orders: {
          bsonType: 'array',
          description: 'Array de órdenes realizadas en el viaje',
          items: {
            bsonType: 'object',
            required: ['IDOrder'],
            properties: {
              IDOrder: {
                bsonType: 'objectId',
                description: 'Referencia de la orden'
              },
              start_time: {
                bsonType: 'date',
                description: 'Fecha y hora de inicio de entrega de la orden'
              },
              end_time: {
                bsonType: 'date',
                description: 'Hora de finalización de la orden'
              }
            }
          }
        },
        IDStateTrip: {
          bsonType: 'string',
          description: 'Referencia del estado del viaje'
        }
      }
    }
  }
});

// 5. StateTrip Collection
db.createCollection("StateTrip", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'message'
      ],
      properties: {
        _id: {
          bsonType: 'string',
          description: 'Identificador de texto único del estado del viaje'
        },
        message: {
          bsonType: 'string',
          description: 'Descripción específica del estado del viaje'
        }
      }
    }
  }
});

// 6. Routes Collection
db.createCollection("Routes", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'name',
        'deliverDays',
        'IDAssignedUser',
        'active'
      ],
      properties: {
        _id: {
          bsonType: 'objectId',
          description: 'Identificador único de la ruta'
        },
        name: {
          bsonType: 'string',
          description: 'Nombre descriptivo de la ruta'
        },
        deliverDays: {
          bsonType: 'array',
          items: {
            "bsonType": "int"
          },
          description: 'Arreglo que indica cuáles  días de la semana se va a realizar esa ruta. Por convención de librerías de fechas, solo admisibles del 0 al 6, donde 0 es domingo.'
        },
        IDAssignedUser: {
          bsonType: 'objectId',
          description: 'Referencia del conductor asignado a la ruta'
        },
        stores: {
          bsonType: 'array',
          description: 'Array de tiendas que forman parte de la ruta',
          items: {
            bsonType: 'object',
            required: ['IDStore', 'sequence'],
            properties: {
              IDStore: {
                bsonType: 'objectId',
                description: 'Identificador de la tienda'
              },
              sequence: {
                bsonType: 'number',
                minimum: 1,
                description: 'Orden de visita en la ruta'
              }
            }
          }
        },
        active: {
          bsonType: 'bool',
          description: 'Campo para eliminación lógica'
        }
      }
    }
  }
});

// 7. Orders Collection
db.createCollection("Orders", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'date',
        'IDCreatedByUser',
        'IDStore',
        'IDStateOrder'
      ],
      properties: {
        _id: {
          bsonType: 'objectId',
          description: 'Identificador único de la orden'
        },
        date: {
          bsonType: 'date',
          description: 'Fecha de creación de la orden'
        },
        delivered: {
          bsonType: 'date',
          description: 'Fecha de entrega asignada de la orden'
        },
        IDCreatedByUser: {
          bsonType: 'objectId',
          description: 'Referencia al usuario que creó la orden'
        },
        IDStore: {
          bsonType: 'objectId',
          description: 'Referencia de la tienda destinataria'
        },
        IDStateOrder: {
          bsonType: 'string',
          description: 'Referencia al estado de la orden'
        }
      }
    }
  }
});

// 8. StateOrder Collection
db.createCollection("StateOrder", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'message'
      ],
      properties: {
        _id: {
          bsonType: 'string',
          description: 'Identificador de texto único del estado de la orden'
        },
        message: {
          bsonType: 'string',
          description: 'Descripción específica del estado de la orden'
        }
      }
    }
  }
});

// 9. Readings Collection
db.createCollection("Readings", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'date',
        'door_state',
        'temp',
        'perc_humidity',
        'latitude',
        'longitude',
        'IDTruck'
      ],
      properties: {
        _id: {
          bsonType: 'objectId',
          description: 'Identificador único de la lectura'
        },
        date: {
          bsonType: 'date',
          description: 'Fecha y hora exacta de la lectura del sensor'
        },
        door_state: {
          bsonType: 'bool',
          description: 'Estado de la puerta del contenedor (true: abierta, false: cerrada)'
        },
        temp: {
          bsonType: 'number',
          description: 'Temperatura registrada del sensor'
        },
        perc_humidity: {
          bsonType: 'number',
          minimum: 0,
          maximum: 100,
          description: 'Porcentaje de humedad'
        },
        latitude: {
          bsonType: 'number',
          minimum: -90,
          maximum: 90,
          description: 'Coordenada de latitud GPS'
        },
        longitude: {
          bsonType: 'number',
          minimum: -180,
          maximum: 180,
          description: 'Coordenada de longitud GPS'
        },
        IDTruck: {
          bsonType: 'objectId',
          description: 'Referencia del camión que generó la lectura'
        }
      }
    }
  }
});

// 10. DoorEvents Collection
db.createCollection("DoorEvents", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'state',
        'time_opened',
        'IDTruck'
      ],
      properties: {
        _id: {
          bsonType: 'objectId',
          description: 'Identificador único del evento'
        },
        state: {
          bsonType: 'bool',
          description: 'Estado de la puerta (true: abierta, false: cerrada)'
        },
        time_opened: {
          bsonType: 'date',
          description: 'Fecha y hora exacta del evento'
        },
        IDTruck: {
          bsonType: 'objectId',
          description: 'Identificador del camión donde ocurrió el evento'
        }
      }
    }
  }
});

// 11. Stores Collection
db.createCollection("Stores", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'name',
        'phone',
        'location',
        'latitude',
        'longitude',
        'active'
      ],
      properties: {
        _id: {
          bsonType: 'objectId',
          description: 'Identificador único de la tienda'
        },
        name: {
          bsonType: 'string',
          description: 'Nombre comercial de la tienda'
        },
        phone: {
          bsonType: 'string',
          description: 'Número telefónico de contacto'
        },
        location: {
          bsonType: 'string',
          description: 'Dirección completa de la tienda'
        },
        latitude: {
          bsonType: 'number',
          minimum: -90,
          maximum: 90,
          description: 'Coordenada de latitud GPS de la tienda'
        },
        longitude: {
          bsonType: 'number',
          minimum: -180,
          maximum: 180,
          description: 'Coordenada de longitud GPS de la tienda'
        },
        active: {
          bsonType: 'bool',
          description: 'Campo para eliminación lógica'
        }
      }
    }
  }
});

// 12. Weather Collection
db.createCollection("Weather", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'temp',
        'date',
        'postal_code'
      ],
      properties: {
        _id: {
          bsonType: 'objectId',
          description: 'Identificador único del registro climático'
        },
        temp: {
          bsonType: 'number',
          description: 'Temperatura ambiente externa'
        },
        humidity: {
          bsonType: 'number',
          minimum: 0,
          maximum: 100,
          description: 'Humedad relativa del ambiente'
        },
        date: {
          bsonType: 'date',
          description: 'Fecha y hora del registro meteorológico'
        },
        location_key: {
          bsonType: 'string',
          description: 'Clave de ubicación geográfica'
        },
        postal_code: {
          bsonType: 'number',
          description: 'Código postal de la zona'
        }
      }
    }
  }
});

// 13. Alerts Collection
db.createCollection("Alerts", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'date',
        'state',
        'IDReading',
        'IDAlertTypes'
      ],
      properties: {
        _id: {
          bsonType: 'objectId',
          description: 'Identificador único de la alerta'
        },
        date: {
          bsonType: 'date',
          description: 'Fecha y hora de cuando se generó la alerta'
        },
        state: {
          bsonType: 'bool',
          description: 'Estado de la alerta (true: resuelta, false: pendiente)'
        },
        IDReading: {
          bsonType: 'objectId',
          description: 'Referencia a la lectura que disparó la alarma'
        },
        IDAlertTypes: {
          bsonType: 'string',
          description: 'Referencia al tipo de alerta generada'
        }
      }
    }
  }
});

// 14. AlertTypes Collection
db.createCollection("AlertTypes", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'message'
      ],
      properties: {
        _id: {
          bsonType: 'string',
          description: 'Identificador de texto único del tipo de alerta'
        },
        message: {
          bsonType: 'string',
          description: 'Mensaje descriptivo de la alerta'
        }
      }
    }
  }
});

// 15. Parameters Collection
db.createCollection("Parameters", {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        '_id',
        'max_temperature',
        'max_humidity',
        'min_temperature',
        'min_humidity'
      ],
      properties: {
        _id: {
          bsonType: 'number',
          description: 'Identificador único del parámetro'
        },
        max_temperature: {
          bsonType: 'number',
          description: 'Temperatura máxima permitida en grados celsius'
        },
        max_humidity: {
          bsonType: 'number',
          minimum: 0,
          maximum: 100,
          description: 'Porcentaje máximo de humedad'
        },
        min_temperature: {
          bsonType: 'number',
          description: 'Temperatura mínima permitida en grados celsius'
        },
        min_humidity: {
          bsonType: 'number',
          minimum: 0,
          maximum: 100,
          description: 'Porcentaje mínimo de humedad relativa'
        }
      }
    }
  }
});
