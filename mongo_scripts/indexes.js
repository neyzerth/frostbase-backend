// 1. Users Collection
db.Users.createIndex({ "email": 1 }, { unique: true });
db.Users.createIndex({ "is_admin": 1 });
db.Users.createIndex({ "active": 1 });
db.Users.createIndex({ "IDTruckDefault": 1 });

// 2. Trucks Collection
db.Trucks.createIndex({ "license_plate": 1 }, { unique: true });
db.Trucks.createIndex({ "IDStateTruck": 1 });
db.Trucks.createIndex({ "brand": 1 });

// 4. Trips Collection
db.Trips.createIndex({ "IDTruck": 1 });
db.Trips.createIndex({ "IDUserDriver": 1 });
db.Trips.createIndex({ "IDRoute": 1 });
db.Trips.createIndex({ "IDStateTrip": 1 });
db.Trips.createIndex({ "start_time": 1 });
db.Trips.createIndex({ "end_time": 1 });
db.Trips.createIndex({ "orders.IDOrder": 1 });
db.Trips.createIndex({ "IDTruck": 1, "start_time": 1 });
db.Trips.createIndex({ "IDUserDriver": 1, "start_time": 1 });
db.Trips.createIndex({ "IDStateTrip": 1, "start_time": 1 });

// 6. Routes Collection
db.Routes.createIndex({ "name": 1 });
db.Routes.createIndex({ "IDAssignedUser": 1 });
db.Routes.createIndex({ "active": 1 });
db.Routes.createIndex({ "deliverDays": 1 });
db.Routes.createIndex({ "stores.IDStore": 1 });
db.Routes.createIndex({ "stores.sequence": 1 });

// 7. Orders Collection
db.Orders.createIndex({ "date": 1 });
db.Orders.createIndex({ "delivered": 1 });
db.Orders.createIndex({ "IDCreatedByUser": 1 });
db.Orders.createIndex({ "IDStore": 1 });
db.Orders.createIndex({ "IDStateOrder": 1 });
db.Orders.createIndex({ "IDStore": 1, "date": 1 });
db.Orders.createIndex({ "IDStateOrder": 1, "date": 1 });
db.Orders.createIndex({ "delivered": 1, "IDStateOrder": 1 });

// 9. Readings Collection
db.Readings.createIndex({ "IDTruck": 1 });
db.Readings.createIndex({ "date": 1 });
db.Readings.createIndex({ "door_state": 1 });
db.Readings.createIndex({ "temp": 1 });
db.Readings.createIndex({ "perc_humidity": 1 });
db.Readings.createIndex({ "IDTruck": 1, "date": -1 });
db.Readings.createIndex({ "latitude": 1, "longitude": 1 });

// 10. DoorEvents Collection
db.DoorEvents.createIndex({ "IDTruck": 1 });
db.DoorEvents.createIndex({ "time_opened": 1 });
db.DoorEvents.createIndex({ "state": 1 });
db.DoorEvents.createIndex({ "IDTruck": 1, "time_opened": 1 });
db.DoorEvents.createIndex({ "IDTruck": 1, "time_opened": -1 }); 

// 11. Stores Collection
db.Stores.createIndex({ "name": 1 });
db.Stores.createIndex({ "active": 1 });
db.Stores.createIndex({ "phone": 1 });
db.Stores.createIndex({ "latitude": 1, "longitude": 1 });

// 12. Weather Collection
db.Weather.createIndex({ "date": 1 });
db.Weather.createIndex({ "postal_code": 1 });
db.Weather.createIndex({ "location_key": 1 });


// 13. Alerts Collection
db.Alerts.createIndex({ "IDReading": 1 });
db.Alerts.createIndex({ "IDAlertTypes": 1 });
db.Alerts.createIndex({ "date": 1 });
db.Alerts.createIndex({ "state": 1 });
db.Alerts.createIndex({ "state": 1, "date": 1 });
db.Alerts.createIndex({ "IDAlertTypes": 1, "date": 1 });

// 16. TripLogs Collection
db.TripLogs.createIndex({ "date": 1 });
db.TripLogs.createIndex({ "IDTrip": 1 });
db.TripLogs.createIndex({ "IDStateTrip": 1 });
db.TripLogs.createIndex({ "orders.IDOrder": 1 });
db.TripLogs.createIndex({ "IDTrip": 1, "date": 1 });

// 17. OsrmRoutes Collection
db.OsrmRoutes.createIndex({ "startLatitude": 1, "startLongitude": 1 });
db.OsrmRoutes.createIndex({ "endLatitude": 1, "endLongitude": 1 });
db.OsrmRoutes.createIndex({ "distance": 1 });
db.OsrmRoutes.createIndex({ "duration": 1 });

// 18. OrderLogs Collection
db.OrderLogs.createIndex({ "date": 1 });
db.OrderLogs.createIndex({ "IDOrder": 1 });
db.OrderLogs.createIndex({ "IDStateOrder": 1 });
db.OrderLogs.createIndex({ "IDOrder": 1, "date": 1 });

// 19. TripsSimulation Collection
db.TripsSimulation.createIndex({ "simulatedTrip._id": 1 });
db.TripsSimulation.createIndex({ "simulatedTrip.IDTruck": 1 });
db.TripsSimulation.createIndex({ "simulatedTrip.start_time": 1 });
db.TripsSimulation.createIndex({ "inserted": 1 });
db.TripsSimulation.createIndex({ "ordersInserted": 1 });

// 20. TruckLogs Collection
db.TruckLogs.createIndex({ "date": 1 });
db.TruckLogs.createIndex({ "IDTruck": 1 });
db.TruckLogs.createIndex({ "IDStateTruck": 1 });
db.TruckLogs.createIndex({ "IDTruck": 1, "date": 1 });

// Para consultas de tiempo real y dashboard
db.Readings.createIndex({ "date": -1 }); // Lecturas más recientes primero
db.Trips.createIndex({ "start_time": -1 }); // Viajes más recientes primero
db.Orders.createIndex({ "date": -1 }); // Órdenes más recientes primero



