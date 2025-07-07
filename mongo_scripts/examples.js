// ===== COLECCIÓN: Orders (órdenes de ejemplo) =====
db.Orders.insertMany([
    {
        _id: ObjectId("674a7001000000000000001a"),
        date: new Date("2025-06-28"),
        delivered: new Date("2025-06-29"),
        IDUser: ObjectId("674a5001000000000000002a"),
        IDStore: ObjectId("674a3001000000000000001a")
    },
    {
        _id: ObjectId("674a7001000000000000001b"),
        date: new Date("2025-06-28"),
        delivered: null,
        IDUser: ObjectId("674a5001000000000000002a"),
        IDStore: ObjectId("674a3001000000000000001c")
    },
    {
        _id: ObjectId("674a7001000000000000001c"),
        date: new Date("2025-06-29"),
        delivered: null,
        IDUser: ObjectId("674a5001000000000000002b"),
        IDStore: ObjectId("674a3001000000000000001b")
    },
    {
        _id: ObjectId("674a7001000000000000001d"),
        date: new Date("2025-06-29"),
        delivered: null,
        IDUser: ObjectId("674a5001000000000000002c"),
        IDStore: ObjectId("674a3001000000000000002a")
    },
    {
        _id: ObjectId("674a7001000000000000001e"),
        date: new Date("2025-06-29"),
        delivered: null,
        IDUser: ObjectId("674a5001000000000000002d"),
        IDStore: ObjectId("674a3001000000000000002d")
    }
]);

// ===== COLECCIÓN: Trips (viajes de ejemplo) =====
db.Trips.insertMany([
    {
        _id: ObjectId("674a8001000000000000001a"),
        date: new Date("2025-06-29"),
        start_hour: new Date("2025-06-29T08:00:00Z"),
        end_hour: new Date("2025-06-29T14:30:00Z"),
        total_time: 390,
        IDRoute: ObjectId("674a6001000000000000001a"),
        orders: [
        {
            IDOrder: ObjectId("674a7001000000000000001a"),
            IDStore: ObjectId("674a3001000000000000001a"),
            time_start: new Date("2025-06-29T08:30:00Z"),
            time_end: new Date("2025-06-29T09:00:00Z")
        },
        {
            IDOrder: ObjectId("674a7001000000000000001b"),
            IDStore: ObjectId("674a3001000000000000001c"),
            time_start: new Date("2025-06-29T10:15:00Z"),
            time_end: new Date("2025-06-29T10:45:00Z")
        }
        ],
            IDStateTrip: "compl"
    },
    {
        _id: ObjectId("674a8001000000000000001b"),
        date: new Date("2025-06-30"),
        start_hour: new Date("2025-06-30T07:30:00Z"),
        end_hour: null,
        total_time: null,
        IDRoute: ObjectId("674a6001000000000000001b"),
        orders: [
            {
                IDOrder: ObjectId("674a7001000000000000001c"),
        IDStore: ObjectId("674a3001000000000000001b"),
        time_start: null,
        time_end: null
            }
        ],
        IDStateTrip: "inpro"
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
        detected_value: 5.2,
        IDAlertTypes: "htemp",
        IDTruck: ObjectId("674a4001000000000000001c")
    },
    {
        _id: ObjectId("674ac001000000000000001b"),
        date: new Date("2025-06-30T09:10:00Z"),
        state: false,
        detected_value: 1,
        IDAlertTypes: "lhum",
        IDTruck: ObjectId("674a4001000000000000001b")
    },
    {
        _id: ObjectId("674ac001000000000000001c"),
        date: new Date("2025-06-28T16:45:00Z"),
        state: true,
        detected_value: 88,
        IDAlertTypes: "odoor",
        IDTruck: ObjectId("674a4001000000000000001e")
    }
]);
