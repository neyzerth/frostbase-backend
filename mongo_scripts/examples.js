// ===== COLECCIÓN: Orders (órdenes de ejemplo) =====
db.Orders.insertMany([
    {
        _id: ObjectId("674a7001000000000000001a"),
        date: new Date("2025-06-28"),
        delivered: new Date("2025-06-29"),
        IDCreatedByUser: ObjectId("674a5001000000000000002a"),
        IDStore: ObjectId("674a3001000000000000001a"),
        IDStateOrder: "DO" // Delivered order
    },
    {
        _id: ObjectId("674a7001000000000000001b"),
        date: new Date("2025-06-28"),
        delivered: new Date("2025-06-29"),
        IDCreatedByUser: ObjectId("674a5001000000000000002a"),
        IDStore: ObjectId("674a3001000000000000001c"),
        IDStateOrder: "DO"
    },
    {
        _id: ObjectId("674a7001000000000000001c"),
        date: new Date("2025-06-29"),
        delivered: new Date("2025-06-29"),
        IDCreatedByUser: ObjectId("674a5001000000000000002b"),
        IDStore: ObjectId("674a3001000000000000001b"),
        IDStateOrder: "DO"
    },
    {
        _id: ObjectId("674a7001000000000000001d"),
        date: new Date("2025-06-29"),
        delivered: new Date("2025-06-29"),
        IDCreatedByUser: ObjectId("674a5001000000000000002c"),
        IDStore: ObjectId("674a3001000000000000002a"),
        IDStateOrder: "DO"
    },
    {
        _id: ObjectId("674a7001000000000000001e"),
        date: new Date("2025-06-29"),
        delivered: new Date("2025-06-29"),
        IDCreatedByUser: ObjectId("674a5001000000000000002d"),
        IDStore: ObjectId("674a3001000000000000002d"),
        IDStateOrder: "DO"
    }
]);
// Order logs
db.OrderLogs.insertMany([
  // Logs para Order 1a
  {
    date: new Date("2025-06-28"),
    IDOrder: ObjectId("674a7001000000000000001a"),
    IDStateOrder: "PO"
  },
  {
    date: new Date("2025-06-29"),
    IDOrder: ObjectId("674a7001000000000000001a"),
    IDStateOrder: "PO"
  },

  // Logs para Order 1b
  {
    date: new Date("2025-06-28"),
    IDOrder: ObjectId("674a7001000000000000001b"),
    IDStateOrder: "PO"
  },
  {
    date: new Date("2025-06-29"),
    IDOrder: ObjectId("674a7001000000000000001b"),
    IDStateOrder: "PO"
  },

  // Logs para Order 1c
  {
    date: new Date("2025-06-29"),
    IDOrder: ObjectId("674a7001000000000000001c"),
    IDStateOrder: "PO"
  },
  {
    date: new Date("2025-06-29"),
    IDOrder: ObjectId("674a7001000000000000001c"),
    IDStateOrder: "PO"
  },

  // Logs para Order 1d
  {
    date: new Date("2025-06-29"),
    IDOrder: ObjectId("674a7001000000000000001d"),
    IDStateOrder: "PO"
  },
  {
    date: new Date("2025-06-29"),
    IDOrder: ObjectId("674a7001000000000000001d"),
    IDStateOrder: "PO"
  },

  // Logs para Order 1e
  {
    date: new Date("2025-06-29"),
    IDOrder: ObjectId("674a7001000000000000001e"),
    IDStateOrder: "PO"
  },
  {
    date: new Date("2025-06-29"),
    IDOrder: ObjectId("674a7001000000000000001e"),
    IDStateOrder: "PO"
  }
]);


// ===== COLECCIÓN: Trips (viajes de ejemplo) =====
db.Trips.insertMany([
    {
        _id: ObjectId("674a8001000000000000001a"),
        start_time: new Date("2025-06-29T08:00:00Z"),
        end_time: new Date("2025-06-29T14:30:00Z"),
        IDTruck: ObjectId("674a4001000000000000001a"),
        IDUserDriver: ObjectId("674a5001000000000000002a"),
        IDRoute: ObjectId("674a6001000000000000001a"),
        orders: [
        {
            IDOrder: ObjectId("674a7001000000000000001a"),
            start_time: new Date("2025-06-29T08:30:00Z"),
            end_time: new Date("2025-06-29T09:00:00Z")
        },
        {
            IDOrder: ObjectId("674a7001000000000000001b"),
            start_time: new Date("2025-06-29T10:15:00Z"),
            end_time: new Date("2025-06-29T10:45:00Z")
        }
        ],
        IDStateTrip: "CP" // Completed
    },
    {
        _id: ObjectId("674a8001000000000000001b"),
        start_time: new Date("2025-06-30T07:30:00Z"),
        end_time: new Date("2025-06-30T12:00:00Z"),
        IDTruck: ObjectId("674a4001000000000000001b"),
        IDUserDriver: ObjectId("674a5001000000000000002b"),
        IDRoute: ObjectId("674a6001000000000000001b"),
        orders: [
            {
                IDOrder: ObjectId("674a7001000000000000001c"),
                start_time: new Date("2025-06-30T08:00:00Z"),
                end_time: new Date("2025-06-30T08:30:00Z")
            }
        ],
        IDStateTrip: "CP"
    }
]);

// Trip logs
db.TripLogs.insertMany([
  // Viaje 1a
  {
    date: new Date("2025-06-29T08:00:00Z"),
    IDTrip: ObjectId("674a8001000000000000001a"),
    IDStateTrip: "IR",
    orders: []
  },
  {
    date: new Date("2025-06-29T08:30:00Z"),
    IDTrip: ObjectId("674a8001000000000000001a"),
    IDStateTrip: "IR",
    orders: [
      {
        IDOrder: ObjectId("674a7001000000000000001a"),
        start_time: new Date("2025-06-29T08:30:00Z"),
        end_time: null
      }
    ]
  },
  {
    date: new Date("2025-06-29T09:00:00Z"),
    IDTrip: ObjectId("674a8001000000000000001a"),
    IDStateTrip: "IR",
    orders: [
      {
        IDOrder: ObjectId("674a7001000000000000001a"),
        start_time: new Date("2025-06-29T08:30:00Z"),
        end_time: new Date("2025-06-29T09:00:00Z")
      }
    ]
  },
  {
    date: new Date("2025-06-29T10:15:00Z"),
    IDTrip: ObjectId("674a8001000000000000001a"),
    IDStateTrip: "IR",
    orders: [
      {
        IDOrder: ObjectId("674a7001000000000000001a"),
        start_time: new Date("2025-06-29T08:30:00Z"),
        end_time: new Date("2025-06-29T09:00:00Z")
      },
      {
        IDOrder: ObjectId("674a7001000000000000001b"),
        start_time: new Date("2025-06-29T10:15:00Z"),
        end_time: null
      }
    ]
  },
  {
    date: new Date("2025-06-29T10:45:00Z"),
    IDTrip: ObjectId("674a8001000000000000001a"),
    IDStateTrip: "IR",
    orders: [
      {
        IDOrder: ObjectId("674a7001000000000000001a"),
        start_time: new Date("2025-06-29T08:30:00Z"),
        end_time: new Date("2025-06-29T09:00:00Z")
      },
      {
        IDOrder: ObjectId("674a7001000000000000001b"),
        start_time: new Date("2025-06-29T10:15:00Z"),
        end_time: new Date("2025-06-29T10:45:00Z")
      }
    ]
  },
  {
    date: new Date("2025-06-29T14:30:00Z"),
    IDTrip: ObjectId("674a8001000000000000001a"),
    IDStateTrip: "CP",
    orders: [
      {
        IDOrder: ObjectId("674a7001000000000000001a"),
        start_time: new Date("2025-06-29T08:30:00Z"),
        end_time: new Date("2025-06-29T09:00:00Z")
      },
      {
        IDOrder: ObjectId("674a7001000000000000001b"),
        start_time: new Date("2025-06-29T10:15:00Z"),
        end_time: new Date("2025-06-29T10:45:00Z")
      }
    ]
  },

  // Viaje 1b
  {
    date: new Date("2025-06-30T07:30:00Z"),
    IDTrip: ObjectId("674a8001000000000000001b"),
    IDStateTrip: "IR",
    orders: []
  },
  {
    date: new Date("2025-06-30T08:00:00Z"),
    IDTrip: ObjectId("674a8001000000000000001b"),
    IDStateTrip: "IR",
    orders: [
      {
        IDOrder: ObjectId("674a7001000000000000001c"),
        start_time: new Date("2025-06-30T08:00:00Z"),
        end_time: null
      }
    ]
  },
  {
    date: new Date("2025-06-30T08:30:00Z"),
    IDTrip: ObjectId("674a8001000000000000001b"),
    IDStateTrip: "IR",
    orders: [
      {
        IDOrder: ObjectId("674a7001000000000000001c"),
        start_time: new Date("2025-06-30T08:00:00Z"),
        end_time: new Date("2025-06-30T08:30:00Z")
      }
    ]
  },
  {
    date: new Date("2025-06-30T12:00:00Z"),
    IDTrip: ObjectId("674a8001000000000000001b"),
    IDStateTrip: "CP",
    orders: [
      {
        IDOrder: ObjectId("674a7001000000000000001c"),
        start_time: new Date("2025-06-30T08:00:00Z"),
        end_time: new Date("2025-06-30T08:30:00Z")
      }
    ]
  }
]);



// ===== COLECCIÓN: Readings (lecturas de sensores de ejemplo) =====
db.Readings.insertMany([
    {
        _id: ObjectId("674a9001000000000000001a"),
        date: new Date("2025-06-30T09:00:00Z"),
        door_state: false,
        temp: 2.5,
        perc_humidity: 78,
        latitude: 32.5149,
        longitude: -117.0382,
        IDTruck: ObjectId("674a4001000000000000001a")
    },
    {
        _id: ObjectId("674a9001000000000000001b"),
        date: new Date("2025-06-30T09:05:00Z"),
        door_state: false,
        temp: 2.8,
        perc_humidity: 79,
        latitude: 32.5155,
        longitude: -117.0385,
        IDTruck: ObjectId("674a4001000000000000001a")
    },
    {
        _id: ObjectId("674a9001000000000000001c"),
        date: new Date("2025-06-30T09:10:00Z"),
        door_state: true,
        temp: 3.2,
        perc_humidity: 80,
        latitude: 32.5200,
        longitude: -117.0420,
        IDTruck: ObjectId("674a4001000000000000001b")
    },
    {
        _id: ObjectId("674a9001000000000000001d"),
        date: new Date("2025-06-30T09:15:00Z"),
        door_state: false,
        temp: 1.8,
        perc_humidity: 76,
        latitude: 32.5420,
        longitude: -116.9800,
        IDTruck: ObjectId("674a4001000000000000001c")
    },
    {
        _id: ObjectId("674a9001000000000000001e"),
        date: new Date("2025-06-30T09:20:00Z"),
        door_state: false,
        temp: 2.1,
        perc_humidity: 77,
        latitude: 32.5380,
        longitude: -116.9850,
        IDTruck: ObjectId("674a4001000000000000001d")
    }
]);

// ===== COLECCIÓN: DoorEvents (eventos de puerta) =====
db.DoorEvents.insertMany([
    {
        _id: ObjectId("674aa001000000000000001a"),
        state: true,
        time_opened: new Date("2025-06-30T09:10:00Z"),
        IDTruck: ObjectId("674a4001000000000000001b")
    },
    {
        _id: ObjectId("674aa001000000000000001b"),
        state: false,
        time_opened: new Date("2025-06-30T09:25:00Z"),
        IDTruck: ObjectId("674a4001000000000000001b")
    },
    {
        _id: ObjectId("674aa001000000000000001c"),
        state: true,
        time_opened: new Date("2025-06-29T10:15:00Z"),
        IDTruck: ObjectId("674a4001000000000000001a")
    },
    {
        _id: ObjectId("674aa001000000000000001d"),
        state: false,
        time_opened: new Date("2025-06-29T10:45:00Z"),
        IDTruck: ObjectId("674a4001000000000000001a")
    }
]);

// ===== COLECCIÓN: Weather (datos meteorológicos) =====
db.Weather.insertMany([
    {
        _id: ObjectId("674ab001000000000000001a"),
        temp: 24,
        humidity: 65,
        date: new Date("2025-06-30T09:00:00Z"),
        location_key: "TIJ_NORTE",
        postal_code: 22000
    },
    {
        _id: ObjectId("674ab001000000000000001b"),
        temp: 26,
        humidity: 68,
        date: new Date("2025-06-30T12:00:00Z"),
        location_key: "TIJ_CENTRO",
        postal_code: 22010
    },
    {
        _id: ObjectId("674ab001000000000000001c"),
        temp: 22,
        humidity: 70,
        date: new Date("2025-06-30T15:00:00Z"),
        location_key: "TIJ_SUR",
        postal_code: 22500
    },
    {
        _id: ObjectId("674ab001000000000000001d"),
        temp: 25,
        humidity: 62,
        date: new Date("2025-06-30T18:00:00Z"),
        location_key: "TIJ_ESTE",
        postal_code: 22204
    }
]);

// ===== COLECCIÓN: Alerts (alertas generadas) =====
db.Alerts.insertMany([
    {
        _id: ObjectId("674ac001000000000000001a"),
        date: new Date("2025-06-29T11:30:00Z"),
        state: true,
        IDReading: ObjectId("674a9001000000000000001d"),
        IDAlertTypes: "HT",
    },
    {
        _id: ObjectId("674ac001000000000000001b"),
        date: new Date("2025-06-30T09:10:00Z"),
        state: true,
        IDReading: ObjectId("674a9001000000000000001c"),
        IDAlertTypes: "LH",
    },
    {
        _id: ObjectId("674ac001000000000000001c"),
        date: new Date("2025-06-28T16:45:00Z"),
        state: true,
        IDReading: ObjectId("674a9001000000000000001e"),
        IDAlertTypes: "OD",
    }  
]);

// READINGS - PRUEBAS A FALTA DE SIMULADOR XD - SEMANA DEL 20 AL 30 DE AGOSTO
db.Readings.insertMany([
  {
    _id: ObjectId("674a9001000000000000002f"),
    date: ISODate("2025-07-20T10:00:00.000Z"),
    door_state: false,
    temp: 2.7,
    perc_humidity: 76,
    latitude: 32.5148,
    longitude: -117.0383,
    IDTruck: ObjectId("674a4001000000000000002d")
  },
  {
    _id: ObjectId("674a90010000000000000030"),
    date: ISODate("2025-07-21T14:30:00.000Z"),
    door_state: true,
    temp: 3.1,
    perc_humidity: 72,
    latitude: 32.5152,
    longitude: -117.0379,
    IDTruck: ObjectId("674a4001000000000000003a")
  },
  {
    _id: ObjectId("674a90010000000000000031"),
    date: ISODate("2025-07-22T08:45:00.000Z"),
    door_state: false,
    temp: 1.9,
    perc_humidity: 85,
    latitude: 32.5145,
    longitude: -117.0386,
    IDTruck: ObjectId("674a4001000000000000001b")
  },
  {
    _id: ObjectId("674a90010000000000000032"),
    date: ISODate("2025-07-23T16:20:00.000Z"),
    door_state: true,
    temp: 4.0,
    perc_humidity: 68,
    latitude: 32.5158,
    longitude: -117.0373,
    IDTruck: ObjectId("674a4001000000000000003b")
  },
  {
    _id: ObjectId("674a90010000000000000033"),
    date: ISODate("2025-07-24T11:15:00.000Z"),
    door_state: false,
    temp: 2.3,
    perc_humidity: 79,
    latitude: 32.5149,
    longitude: -117.0382,
    IDTruck: ObjectId("674a4001000000000000002e")
  },
  {
    _id: ObjectId("674a90010000000000000034"),
    date: ISODate("2025-07-25T09:30:00.000Z"),
    door_state: true,
    temp: 2.9,
    perc_humidity: 74,
    latitude: 32.5151,
    longitude: -117.0380,
    IDTruck: ObjectId("674a4001000000000000001c")
  },
  {
    _id: ObjectId("674a90010000000000000035"),
    date: ISODate("2025-07-26T13:45:00.000Z"),
    door_state: false,
    temp: 3.5,
    perc_humidity: 71,
    latitude: 32.5143,
    longitude: -117.0388,
    IDTruck: ObjectId("674a4001000000000000002a")
  },
  {
    _id: ObjectId("674a90010000000000000036"),
    date: ISODate("2025-07-27T07:00:00.000Z"),
    door_state: true,
    temp: 1.5,
    perc_humidity: 88,
    latitude: 32.5155,
    longitude: -117.0376,
    IDTruck: ObjectId("674a4001000000000000001d")
  },
  {
    _id: ObjectId("674a90010000000000000037"),
    date: ISODate("2025-07-28T15:10:00.000Z"),
    door_state: false,
    temp: 4.2,
    perc_humidity: 65,
    latitude: 32.5140,
    longitude: -117.0391,
    IDTruck: ObjectId("674a4001000000000000003a")
  },
  {
    _id: ObjectId("674a90010000000000000038"),
    date: ISODate("2025-07-29T12:25:00.000Z"),
    door_state: true,
    temp: 2.0,
    perc_humidity: 82,
    latitude: 32.5157,
    longitude: -117.0374,
    IDTruck: ObjectId("674a4001000000000000002b")
  },
  {
    _id: ObjectId("674a90010000000000000039"),
    date: ISODate("2025-07-20T17:50:00.000Z"),
    door_state: false,
    temp: 3.8,
    perc_humidity: 69,
    latitude: 32.5142,
    longitude: -117.0389,
    IDTruck: ObjectId("674a4001000000000000001e")
  },
  {
    _id: ObjectId("674a9001000000000000003a"),
    date: ISODate("2025-07-21T06:15:00.000Z"),
    door_state: true,
    temp: 1.2,
    perc_humidity: 90,
    latitude: 32.5159,
    longitude: -117.0372,
    IDTruck: ObjectId("674a4001000000000000002c")
  },
  {
    _id: ObjectId("674a9001000000000000003b"),
    date: ISODate("2025-07-30T19:30:00.000Z"),
    door_state: false,
    temp: 4.5,
    perc_humidity: 63,
    latitude: 32.5141,
    longitude: -117.0390,
    IDTruck: ObjectId("674a4001000000000000003b")
  },
  {
    _id: ObjectId("674a9001000000000000003c"),
    date: ISODate("2025-07-22T10:40:00.000Z"),
    door_state: true,
    temp: 2.4,
    perc_humidity: 77,
    latitude: 32.5150,
    longitude: -117.0381,
    IDTruck: ObjectId("674a4001000000000000001a")
  },
  {
    _id: ObjectId("674a9001000000000000003d"),
    date: ISODate("2025-07-23T08:05:00.000Z"),
    door_state: false,
    temp: 1.8,
    perc_humidity: 86,
    latitude: 32.5146,
    longitude: -117.0385,
    IDTruck: ObjectId("674a4001000000000000002d")
  },
  {
    _id: ObjectId("674a9001000000000000003e"),
    date: ISODate("2025-07-31T14:15:00.000Z"),
    door_state: true,
    temp: 3.9,
    perc_humidity: 67,
    latitude: 32.5153,
    longitude: -117.0378,
    IDTruck: ObjectId("674a4001000000000000001b")
  },
  {
    _id: ObjectId("674a9001000000000000003f"),
    date: ISODate("2025-07-25T18:20:00.000Z"),
    door_state: false,
    temp: 4.1,
    perc_humidity: 66,
    latitude: 32.5144,
    longitude: -117.0387,
    IDTruck: ObjectId("674a4001000000000000003a")
  },
  {
    _id: ObjectId("674a90010000000000000040"),
    date: ISODate("2025-07-26T09:55:00.000Z"),
    door_state: true,
    temp: 2.1,
    perc_humidity: 81,
    latitude: 32.5156,
    longitude: -117.0375,
    IDTruck: ObjectId("674a4001000000000000002e")
  },
  {
    _id: ObjectId("674a90010000000000000041"),
    date: ISODate("2025-07-27T16:30:00.000Z"),
    door_state: false,
    temp: 3.6,
    perc_humidity: 70,
    latitude: 32.5147,
    longitude: -117.0384,
    IDTruck: ObjectId("674a4001000000000000001c")
  },
  {
    _id: ObjectId("674a90010000000000000042"),
    date: ISODate("2025-07-28T07:45:00.000Z"),
    door_state: true,
    temp: 1.3,
    perc_humidity: 89,
    latitude: 32.5154,
    longitude: -117.0377,
    IDTruck: ObjectId("674a4001000000000000002a")
  }
]);
