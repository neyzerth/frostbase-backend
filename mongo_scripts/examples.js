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

// ===== COLECCIÓN: Trips (viajes de ejemplo) =====
db.Trips.insertMany([
    {
        _id: ObjectId("674a8001000000000000001a"),
        start_time: new Date("2025-06-29T08:00:00Z"),
        end_time: new Date("2025-06-29T14:30:00Z"),
        total_time: 390,
        IDTruck: ObjectId("674a4001000000000000001a"),
        IDUserDriver: ObjectId("674a5001000000000000002a"),
        IDRoute: ObjectId("674a6001000000000000001a"),
        orders: [
        {
            IDOrder: ObjectId("674a7001000000000000001a"),
            IDStore: ObjectId("674a3001000000000000001a"),
            start_time: new Date("2025-06-29T08:30:00Z"),
            end_time: new Date("2025-06-29T09:00:00Z")
        },
        {
            IDOrder: ObjectId("674a7001000000000000001b"),
            IDStore: ObjectId("674a3001000000000000001c"),
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
        total_time: 270,
        IDTruck: ObjectId("674a4001000000000000001b"),
        IDUserDriver: ObjectId("674a5001000000000000002b"),
        IDRoute: ObjectId("674a6001000000000000001b"),
        orders: [
            {
                IDOrder: ObjectId("674a7001000000000000001c"),
                IDStore: ObjectId("674a3001000000000000001b"),
                start_time: new Date("2025-06-30T08:00:00Z"),
                end_time: new Date("2025-06-30T08:30:00Z")
            }
        ],
        IDStateTrip: "CP"
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
