// ===== COLECCIÓN: StateTruck =====
db.StateTruck.insertMany([
  { _id: "AV", message: "Available" },
  { _id: "IR", message: "In route" },
  { _id: "IM", message: "In maintenance" },
  { _id: "OS", message: "Out of service" }
]);

// ===== COLECCIÓN: StateTrip =====
db.StateTrip.insertMany([
  { _id: "IP", message: "In progress" },
  { _id: "CP", message: "Completed" },
  { _id: "CN", message: "Cancelled" }
]);

// ===== COLECCIÓN: StateOrder =====
db.StateOrder.insertMany([
  { _id: "PO", message: "Pending order" },
  { _id: "DO", message: "Delivered order" },
  { _id: "CO", message: "Cancelled order" },
  { _id: "LO", message: "Late order" }
]);

// ===== COLECCIÓN: AlertTypes =====
db.AlertTypes.insertMany([
  {
    _id: "HT",
    message: "The container temperature has exceeded the maximum allowed limit"
  },
  {
    _id: "LT",
    message: "The container temperature is below the minimum limit"
  },
  {
    _id: "HH",
    message: "Humidity level has exceeded the maximum allowed"
  },
  {
    _id: "LH",
    message: "Humidity level is below the required minimum"
  },
  {
    _id: "OD",
    message: "Container door has been opened without authorization"
  }
]);

// ===== COLECCIÓN: Parameters =====
db.Parameters.insertMany([
  {
    _id: 1,
    max_temperature: 4,
    max_humidity: 85,
    min_temperature: -2,
    min_humidity: 75
  }
]);

// ===== COLECCIÓN: Stores =====
db.Stores.insertMany([
  // Zona Norte de Tijuana
  { _id: ObjectId("674a3001000000000000001a"), name: "OXXO Zona Norte", phone: "664-123-4501", location: "Av. Revolución 1234, Zona Norte", latitude: 32.5149, longitude: -117.0382, active: true },
  { _id: ObjectId("674a3001000000000000001b"), name: "Seven Eleven Macroplaza", phone: "664-123-4502", location: "Blvd. Fundadores 4567, Macroplaza", latitude: 32.5200, longitude: -117.0420, active: true },
  { _id: ObjectId("674a3001000000000000001c"), name: "Circle K Constitución", phone: "664-123-4503", location: "Av. Constitución 789, Centro", latitude: 32.5180, longitude: -117.0350, active: true },
  { _id: ObjectId("674a3001000000000000001d"), name: "Tienda EXTRA Hipódromo", phone: "664-123-4504", location: "Blvd. Agua Caliente 2345, Hipódromo", latitude: 32.5250, longitude: -117.0500, active: true },
  { _id: ObjectId("674a3001000000000000001e"), name: "Soriana Express Norte", phone: "664-123-4505", location: "Av. Insurgentes 1567, Libertad", latitude: 32.5300, longitude: -117.0450, active: true },
  
  // Zona Este de Tijuana
  { _id: ObjectId("674a3001000000000000002a"), name: "OXXO Mesa de Otay", phone: "664-123-4506", location: "Carr. Mesa de Otay 890, Mesa de Otay", latitude: 32.5420, longitude: -116.9800, active: true },
  { _id: ObjectId("674a3001000000000000002b"), name: "Seven Eleven Otay", phone: "664-123-4507", location: "Blvd. Industrial 1234, Otay", latitude: 32.5380, longitude: -116.9850, active: true },
  { _id: ObjectId("674a3001000000000000002c"), name: "Circle K La Mesa", phone: "664-123-4508", location: "Av. La Mesa 456, La Mesa", latitude: 32.5350, longitude: -116.9900, active: true },
  { _id: ObjectId("674a3001000000000000002d"), name: "Tienda EXTRA Otay Universidad", phone: "664-123-4509", location: "Blvd. Otay Universidad 789, Otay Universidad", latitude: 32.5450, longitude: -116.9750, active: true },
  { _id: ObjectId("674a3001000000000000002e"), name: "Soriana Express Otay", phone: "664-123-4510", location: "Av. Universidad 2345, Otay Universidad", latitude: 32.5480, longitude: -116.9720, active: true },
  
  // Zona Sur de Tijuana
  { _id: ObjectId("674a3001000000000000003a"), name: "OXXO Zona Sur", phone: "664-123-4511", location: "Blvd. Díaz Ordaz 3456, Zona Sur", latitude: 32.4850, longitude: -117.0200, active: true },
  { _id: ObjectId("674a3001000000000000003b"), name: "Seven Eleven Playas", phone: "664-123-4512", location: "Av. Playas de Tijuana 1789, Playas", latitude: 32.4700, longitude: -117.1200, active: true },
  { _id: ObjectId("674a3001000000000000003c"), name: "Circle K Zona Río", phone: "664-123-4513", location: "Paseo de los Héroes 4567, Zona Río", latitude: 32.5020, longitude: -117.0800, active: true },
  { _id: ObjectId("674a3001000000000000003d"), name: "Tienda EXTRA San Antonio", phone: "664-123-4514", location: "Av. San Antonio 890, San Antonio", latitude: 32.4900, longitude: -117.0100, active: true },
  { _id: ObjectId("674a3001000000000000003e"), name: "Soriana Express Playas", phone: "664-123-4515", location: "Av. del Mar 2567, Playas de Tijuana", latitude: 32.4650, longitude: -117.1250, active: true },
  
  // Zona Centro y Oeste
  { _id: ObjectId("674a3001000000000000004a"), name: "OXXO Centro", phone: "664-123-4516", location: "Av. Revolución 567, Centro", latitude: 32.5150, longitude: -117.0380, active: true },
  { _id: ObjectId("674a3001000000000000004b"), name: "Seven Eleven Cacho", phone: "664-123-4517", location: "Blvd. Cuauhtémoc 1890, Cacho", latitude: 32.5100, longitude: -117.0600, active: true },
  { _id: ObjectId("674a3001000000000000004c"), name: "Circle K Madero", phone: "664-123-4518", location: "Av. Madero 234, Madero", latitude: 32.5080, longitude: -117.0400, active: true },
  { _id: ObjectId("674a3001000000000000004d"), name: "Tienda EXTRA Tercera Etapa", phone: "664-123-4519", location: "Blvd. Tercera Etapa 1456, Tercera Etapa", latitude: 32.4950, longitude: -117.0300, active: true },
  { _id: ObjectId("674a3001000000000000004e"), name: "Soriana Express Madero", phone: "664-123-4520", location: "Av. Padre Kino 789, Madero", latitude: 32.5120, longitude: -117.0450, active: true },
  
  // Zona Noreste (adicionales)
  { _id: ObjectId("674a3001000000000000005a"), name: "OXXO Aeropuerto", phone: "664-123-4521", location: "Carr. Aeropuerto 123, Aeropuerto", latitude: 32.5410, longitude: -116.9700, active: true },
  { _id: ObjectId("674a3001000000000000005b"), name: "Seven Eleven Industrial", phone: "664-123-4522", location: "Blvd. Industrial 2890, Industrial", latitude: 32.5320, longitude: -116.9950, active: true },
  { _id: ObjectId("674a3001000000000000005c"), name: "Circle K Alemán", phone: "664-123-4523", location: "Av. Alemán 567, Alemán", latitude: 32.5280, longitude: -117.0100, active: true },
  { _id: ObjectId("674a3001000000000000005d"), name: "Tienda EXTRA Camino Verde", phone: "664-123-4524", location: "Blvd. Camino Verde 890, Camino Verde", latitude: 32.5350, longitude: -117.0050, active: true },
  { _id: ObjectId("674a3001000000000000005e"), name: "Soriana Express Tecnológico", phone: "664-123-4525", location: "Blvd. Tecnológico 1234, Tecnológico", latitude: 32.5220, longitude: -117.0180, active: true },
  
  // Zonas periféricas
  { _id: ObjectId("674a3001000000000000006a"), name: "OXXO Rosarito Km 28", phone: "664-123-4526", location: "Carr. Tijuana-Rosarito Km 28", latitude: 32.4800, longitude: -117.0800, active: true },
  { _id: ObjectId("674a3001000000000000006b"), name: "Seven Eleven La Presa", phone: "664-123-4527", location: "Blvd. La Presa 456, La Presa", latitude: 32.4600, longitude: -116.9900, active: true },
  { _id: ObjectId("674a3001000000000000006c"), name: "Circle K Mariano Matamoros", phone: "664-123-4528", location: "Av. Mariano Matamoros 789, Matamoros", latitude: 32.4750, longitude: -117.0150, active: true },
  { _id: ObjectId("674a3001000000000000006d"), name: "Tienda EXTRA Cerro Colorado", phone: "664-123-4529", location: "Blvd. Cerro Colorado 123, Cerro Colorado", latitude: 32.4500, longitude: -116.9700, active: true },
  { _id: ObjectId("674a3001000000000000006e"), name: "Soriana Express Villa Fontana", phone: "664-123-4530", location: "Av. Villa Fontana 456, Villa Fontana", latitude: 32.4950, longitude: -116.9800, active: true },
  
  // Zona Suroeste y Costeras
  { _id: ObjectId("674a3001000000000000007a"), name: "OXXO El Florido", phone: "664-123-4531", location: "Blvd. El Florido 789, El Florido", latitude: 32.4400, longitude: -117.0500, active: true },
  { _id: ObjectId("674a3001000000000000007b"), name: "Seven Eleven San Ysidro", phone: "664-123-4532", location: "Av. San Ysidro 234, San Ysidro", latitude: 32.4300, longitude: -117.0300, active: true },
  { _id: ObjectId("674a3001000000000000007c"), name: "Circle K Libertad", phone: "664-123-4533", location: "Av. Libertad 567, Libertad", latitude: 32.4200, longitude: -117.0100, active: true },
  { _id: ObjectId("674a3001000000000000007d"), name: "Tienda EXTRA Natura", phone: "664-123-4534", location: "Blvd. Natura 890, Natura", latitude: 32.4100, longitude: -116.9900, active: true },
  { _id: ObjectId("674a3001000000000000007e"), name: "Soriana Express Rosarito Centro", phone: "664-123-4535", location: "Blvd. Rosarito 123, Rosarito Centro", latitude: 32.3600, longitude: -117.0400, active: true },
  
  // Zona Sureste
  { _id: ObjectId("674a3001000000000000008a"), name: "OXXO Tecate Road", phone: "664-123-4536", location: "Carr. Tijuana-Tecate Km 15", latitude: 32.4800, longitude: -116.8500, active: true },
  { _id: ObjectId("674a3001000000000000008b"), name: "Seven Eleven Boulevard 2000", phone: "664-123-4537", location: "Blvd. 2000, 456, Boulevard 2000", latitude: 32.4600, longitude: -116.8800, active: true },
  { _id: ObjectId("674a3001000000000000008c"), name: "Circle K Valle de las Palmas", phone: "664-123-4538", location: "Av. Valle de las Palmas 789", latitude: 32.4200, longitude: -116.8200, active: true },
  { _id: ObjectId("674a3001000000000000008d"), name: "Tienda EXTRA El Refugio", phone: "664-123-4539", location: "Blvd. El Refugio 234, El Refugio", latitude: 32.4000, longitude: -116.8600, active: true },
  { _id: ObjectId("674a3001000000000000008e"), name: "Soriana Express La Rumorosa", phone: "664-123-4540", location: "Carr. La Rumorosa Km 5", latitude: 32.3800, longitude: -116.7900, active: true }
]);

// ===== COLECCIÓN: Trucks =====
db.Trucks.insertMany([
  { _id: ObjectId("674a4001000000000000001a"), brand: "Freightliner", model: "Cascadia 2022", license_plate: "TIJ-001-RF", IDStateTruck: "AV" },
  { _id: ObjectId("674a4001000000000000001b"), brand: "Volvo", model: "VNL 860 2021", license_plate: "TIJ-002-RF", IDStateTruck: "AV" },
  { _id: ObjectId("674a4001000000000000001c"), brand: "Kenworth", model: "T680 2023", license_plate: "TIJ-003-RF", IDStateTruck: "AV" },
  { _id: ObjectId("674a4001000000000000001d"), brand: "Peterbilt", model: "579 2022", license_plate: "TIJ-004-RF", IDStateTruck: "AV" },
  { _id: ObjectId("674a4001000000000000001e"), brand: "Mack", model: "Anthem 2021", license_plate: "TIJ-005-RF", IDStateTruck: "AV" },
  { _id: ObjectId("674a4001000000000000002a"), brand: "International", model: "LT625 2023", license_plate: "TIJ-006-RF", IDStateTruck: "AV" },
  { _id: ObjectId("674a4001000000000000002b"), brand: "Freightliner", model: "Cascadia 2021", license_plate: "TIJ-007-RF", IDStateTruck: "AV" },
  { _id: ObjectId("674a4001000000000000002c"), brand: "Volvo", model: "VNL 760 2022", license_plate: "TIJ-008-RF", IDStateTruck: "AV" },
  { _id: ObjectId("674a4001000000000000002d"), brand: "Kenworth", model: "T880 2021", license_plate: "TIJ-009-RF", IDStateTruck: "IM" },
  { _id: ObjectId("674a4001000000000000002e"), brand: "Peterbilt", model: "367 2023", license_plate: "TIJ-010-RF", IDStateTruck: "AV" },
  { _id: ObjectId("674a4001000000000000003a"), brand: "Mack", model: "Granite 2022", license_plate: "TIJ-011-RF", IDStateTruck: "AV" },
  { _id: ObjectId("674a4001000000000000003b"), brand: "International", model: "ProStar 2021", license_plate: "TIJ-012-RF", IDStateTruck: "OS" }
]);

// Trucks logs
db.TruckLogs.insertMany([
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000001a"), IDStateTruck: "AV" },
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000001b"), IDStateTruck: "AV" },
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000001c"), IDStateTruck: "AV" },
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000001d"), IDStateTruck: "AV" },
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000001e"), IDStateTruck: "AV" },
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000002a"), IDStateTruck: "AV" },
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000002b"), IDStateTruck: "AV" },
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000002c"), IDStateTruck: "AV" },
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000002d"), IDStateTruck: "IM" },
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000002e"), IDStateTruck: "AV" },
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000003a"), IDStateTruck: "AV" },
  { date: new Date("2025-06-28T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000003b"), IDStateTruck: "AV" },
  { date: new Date("2025-06-30T12:00:00Z"), IDTruck: ObjectId("674a4001000000000000003b"), IDStateTruck: "OS" }
]);


// ===== COLECCIÓN: Users =====
// Administradores
// Se agregan campos is_admin y active
db.Users.insertMany([
  {
    _id: ObjectId("674a5001000000000000001a"),
    first_name: "María Elena",
    last_name: "González",
    middle_name: "Rodríguez",
    email: "mgonzalez@logisticarf.com",
    phone: "664-555-0101",
    birthdate: new Date("1985-03-15"),
    password: "admin123",
    is_admin: true,
    active: true
  },
  {
    _id: ObjectId("674a5001000000000000001b"),
    first_name: "Carlos",
    last_name: "Mendoza",
    middle_name: "López",
    email: "cmendoza@logisticarf.com",
    phone: "664-555-0102",
    birthdate: new Date("1978-08-22"),
    password: "admin456",
    is_admin: true,
    active: true
  },
  {
    _id: ObjectId("674a5001000000000000001c"),
    first_name: "Ana",
    last_name: "Martínez",
    middle_name: "Fernández",
    email: "amartinez@logisticarf.com",
    phone: "664-555-0103",
    birthdate: new Date("1990-12-05"),
    password: "admin789",
    is_admin: true,
    active: true
  }
]);

// Conductores
// Se agregan campos is_admin y active
db.Users.insertMany([
  {
    _id: ObjectId("674a5001000000000000002a"),
    first_name: "Roberto",
    last_name: "Silva",
    middle_name: "Morales",
    email: "rsilva@logisticarf.com",
    phone: "664-555-0201",
    birthdate: new Date("1982-05-10"),
    password: "driver001",
    is_admin: false,
    IDTruckDefault: ObjectId("674a4001000000000000001a"),
    active: true
  },
  {
    _id: ObjectId("674a5001000000000000002b"),
    first_name: "José",
    last_name: "Ramírez",
    middle_name: "García",
    email: "jramirez@logisticarf.com",
    phone: "664-555-0202",
    birthdate: new Date("1987-11-18"),
    password: "driver002",
    is_admin: false,
    IDTruckDefault: ObjectId("674a4001000000000000001b"),
    active: true
  },
  {
    _id: ObjectId("674a5001000000000000002c"),
    first_name: "Miguel",
    last_name: "Torres",
    middle_name: "Sánchez",
    email: "mtorres@logisticarf.com",
    phone: "664-555-0203",
    birthdate: new Date("1979-02-28"),
    password: "driver003",
    is_admin: false,
    IDTruckDefault: ObjectId("674a4001000000000000001c"),
    active: true
  },
  {
    _id: ObjectId("674a5001000000000000002d"),
    first_name: "Francisco",
    last_name: "Herrera",
    middle_name: "Jiménez",
    email: "fherrera@logisticarf.com",
    phone: "664-555-0204",
    birthdate: new Date("1984-07-14"),
    password: "driver004",
    is_admin: false,
    IDTruckDefault: ObjectId("674a4001000000000000001d"),
    active: true
  },
  {
    _id: ObjectId("674a5001000000000000002e"),
    first_name: "Eduardo",
    last_name: "Vásquez",
    middle_name: "Ruiz",
    email: "evasquez@logisticarf.com",
    phone: "664-555-0205",
    birthdate: new Date("1981-09-03"),
    password: "driver005",
    is_admin: false,
    IDTruckDefault: ObjectId("674a4001000000000000001e"),
    active: true
  },
  {
    _id: ObjectId("674a5001000000000000003a"),
    first_name: "Luis",
    last_name: "Medina",
    middle_name: "Castro",
    email: "lmedina@logisticarf.com",
    phone: "664-555-0206",
    birthdate: new Date("1986-04-20"),
    password: "driver006",
    is_admin: false,
    IDTruckDefault: ObjectId("674a4001000000000000002a"),
    active: true
  },
  {
    _id: ObjectId("674a5001000000000000003b"),
    first_name: "Alejandro",
    last_name: "Ortega",
    middle_name: "Vargas",
    email: "aortega@logisticarf.com",
    phone: "664-555-0207",
    birthdate: new Date("1983-12-30"),
    password: "driver007",
    is_admin: false,
    IDTruckDefault: ObjectId("674a4001000000000000002b"),
    active: true
  },
  {
    _id: ObjectId("674a5001000000000000003c"),
    first_name: "Daniel",
    last_name: "Flores",
    middle_name: "Peña",
    email: "dflores@logisticarf.com",
    phone: "664-555-0208",
    birthdate: new Date("1988-06-12"),
    password: "driver008",
    is_admin: false,
    IDTruckDefault: ObjectId("674a4001000000000000002c"),
    active: true
  }
]);

// ===== COLECCIÓN: Routes =====
// Cambiar IDUser a IDAssignedUser y agregar active
db.Routes.insertMany([
  {
    _id: ObjectId("674a6001000000000000001a"),
    name: "Ruta Norte - Zona Centro",
    deliverDays: [2, 5],
    IDAssignedUser: ObjectId("674a5001000000000000002a"),
    stores: [
      { IDStore: ObjectId("674a3001000000000000001a"), sequence: 1 },
      { IDStore: ObjectId("674a3001000000000000001c"), sequence: 2 },
      { IDStore: ObjectId("674a3001000000000000004a"), sequence: 3 },
      { IDStore: ObjectId("674a3001000000000000004c"), sequence: 4 },
      { IDStore: ObjectId("674a3001000000000000004e"), sequence: 5 }
    ],
    active: true
  },
  {
    _id: ObjectId("674a6001000000000000001b"),
    name: "Ruta Norte - Hipódromo",
    deliverDays: [1, 2, 6],
    IDAssignedUser: ObjectId("674a5001000000000000002b"),
    stores: [
      { IDStore: ObjectId("674a3001000000000000001b"), sequence: 1 },
      { IDStore: ObjectId("674a3001000000000000001d"), sequence: 2 },
      { IDStore: ObjectId("674a3001000000000000001e"), sequence: 3 },
      { IDStore: ObjectId("674a3001000000000000004b"), sequence: 4 },
      { IDStore: ObjectId("674a3001000000000000005c"), sequence: 5 }
    ],
    active: true
  },
  {
    _id: ObjectId("674a6001000000000000001c"),
    name: "Ruta Este - Mesa de Otay",
    IDAssignedUser: ObjectId("674a5001000000000000002c"),
    deliverDays: [1, 4],
    stores: [
      { IDStore: ObjectId("674a3001000000000000002a"), sequence: 1 },
      { IDStore: ObjectId("674a3001000000000000002b"), sequence: 2 },
      { IDStore: ObjectId("674a3001000000000000002c"), sequence: 3 },
      { IDStore: ObjectId("674a3001000000000000005a"), sequence: 4 },
      { IDStore: ObjectId("674a3001000000000000005b"), sequence: 5 }
    ],
    active: true
  },
  {
    _id: ObjectId("674a6001000000000000001d"),
    name: "Ruta Este - Otay Universidad",
    deliverDays: [1, 3],
    IDAssignedUser: ObjectId("674a5001000000000000002d"),
    stores: [
      { IDStore: ObjectId("674a3001000000000000002d"), sequence: 1 },
      { IDStore: ObjectId("674a3001000000000000002e"), sequence: 2 },
      { IDStore: ObjectId("674a3001000000000000005d"), sequence: 3 },
      { IDStore: ObjectId("674a3001000000000000005e"), sequence: 4 },
      { IDStore: ObjectId("674a3001000000000000006e"), sequence: 5 }
    ],
    active: true
  },
  {
    _id: ObjectId("674a6001000000000000001e"),
    name: "Ruta Sur - Playas",
    deliverDays: [3, 6],
    IDAssignedUser: ObjectId("674a5001000000000000002e"),
    stores: [
      { IDStore: ObjectId("674a3001000000000000003b"), sequence: 1 },
      { IDStore: ObjectId("674a3001000000000000003c"), sequence: 2 },
      { IDStore: ObjectId("674a3001000000000000003e"), sequence: 3 },
      { IDStore: ObjectId("674a3001000000000000006a"), sequence: 4 },
      { IDStore: ObjectId("674a3001000000000000007e"), sequence: 5 }
    ],
    active: true
  },
  {
    _id: ObjectId("674a6001000000000000002a"),
    name: "Ruta Sur - Zona Sur",
    deliverDays: [2],
    IDAssignedUser: ObjectId("674a5001000000000000003a"),
    stores: [
      { IDStore: ObjectId("674a3001000000000000003a"), sequence: 1 },
      { IDStore: ObjectId("674a3001000000000000003d"), sequence: 2 },
      { IDStore: ObjectId("674a3001000000000000004d"), sequence: 3 },
      { IDStore: ObjectId("674a3001000000000000006c"), sequence: 4 },
      { IDStore: ObjectId("674a3001000000000000007a"), sequence: 5 }
    ],
    active: true
  },
  {
    _id: ObjectId("674a6001000000000000002b"),
    name: "Ruta Sureste - La Presa",
    deliverDays: [1, 4],
    IDAssignedUser: ObjectId("674a5001000000000000003b"),
    stores: [
      { IDStore: ObjectId("674a3001000000000000006b"), sequence: 1 },
      { IDStore: ObjectId("674a3001000000000000006d"), sequence: 2 },
      { IDStore: ObjectId("674a3001000000000000008a"), sequence: 3 },
      { IDStore: ObjectId("674a3001000000000000008b"), sequence: 4 },
      { IDStore: ObjectId("674a3001000000000000008c"), sequence: 5 }
    ],
    active: true
  },
  {
    _id: ObjectId("674a6001000000000000002c"),
    name: "Ruta Suroeste - Fronteriza",
    deliverDays: [4],
    IDAssignedUser: ObjectId("674a5001000000000000003c"),
    stores: [
      { IDStore: ObjectId("674a3001000000000000007b"), sequence: 1 },
      { IDStore: ObjectId("674a3001000000000000007c"), sequence: 2 },
      { IDStore: ObjectId("674a3001000000000000007d"), sequence: 3 },
      { IDStore: ObjectId("674a3001000000000000008d"), sequence: 4 },
      { IDStore: ObjectId("674a3001000000000000008e"), sequence: 5 }
    ],
    active: true
  }
]);

//Osrm Routes
db.OsrmRoutes.insertMany([
  {
    _id: ObjectId("688866db5925b815307305ec"),
    startLatitude: 32.45900929216648,
    startLongitude: -116.97966765227373,
    endLatitude: 32.51,
    endLongitude: -117.06,
    distance: 13952.9,
    duration: 878.2
  },
  {
    _id: ObjectId("688866db5925b815307305f1"),
    startLatitude: 32.51,
    startLongitude: -117.06,
    endLatitude: 32.528,
    endLongitude: -117.01,
    distance: 7255.3,
    duration: 573.2
  },
  {
    _id: ObjectId("688866dc5925b815307305f6"),
    startLatitude: 32.528,
    startLongitude: -117.01,
    endLatitude: 32.53,
    endLongitude: -117.045,
    distance: 4949.9,
    duration: 407.4
  },
  {
    _id: ObjectId("688866dd5925b815307305fb"),
    startLatitude: 32.53,
    startLongitude: -117.045,
    endLatitude: 32.52,
    endLongitude: -117.042,
    distance: 2083,
    duration: 237.7
  },
  {
    _id: ObjectId("688866de5925b81530730600"),
    startLatitude: 32.52,
    startLongitude: -117.042,
    endLatitude: 32.525,
    endLongitude: -117.05,
    distance: 1646.7,
    duration: 185.1
  },
  {
    _id: ObjectId("688866df5925b81530730605"),
    startLatitude: 32.525,
    startLongitude: -117.05,
    endLatitude: 32.52,
    endLongitude: -117.042,
    distance: 1646.7,
    duration: 183
  },
  {
    _id: ObjectId("688866e15925b8153073060a"),
    startLatitude: 32.52,
    startLongitude: -117.042,
    endLatitude: 32.45900929216648,
    endLongitude: -116.97966765227373,
    distance: 15712.7,
    duration: 986.7
  },
  {
    _id: ObjectId("68886b58c34a05703966ac10"),
    startLatitude: 32.45900929216648,
    startLongitude: -116.97966765227373,
    endLatitude: 32.5149,
    endLongitude: -117.0382,
    distance: 15040.7,
    duration: 1053.2
  },
  {
    _id: ObjectId("68886b59c34a05703966ac15"),
    startLatitude: 32.5149,
    startLongitude: -117.0382,
    endLatitude: 32.518,
    endLongitude: -117.035,
    distance: 648.4,
    duration: 89.7
  },
  {
    _id: ObjectId("68886b5bc34a05703966ac1a"),
    startLatitude: 32.518,
    startLongitude: -117.035,
    endLatitude: 32.515,
    endLongitude: -117.038,
    distance: 696.6,
    duration: 91.1
  },
  {
    _id: ObjectId("68886b5bc34a05703966ac1f"),
    startLatitude: 32.515,
    startLongitude: -117.038,
    endLatitude: 32.508,
    endLongitude: -117.04,
    distance: 1486.8,
    duration: 157.4
  },
  {
    _id: ObjectId("68886b5cc34a05703966ac24"),
    startLatitude: 32.508,
    startLongitude: -117.04,
    endLatitude: 32.512,
    endLongitude: -117.045,
    distance: 1178.9,
    duration: 115
  },
  {
    _id: ObjectId("68886b5dc34a05703966ac29"),
    startLatitude: 32.512,
    startLongitude: -117.045,
    endLatitude: 32.518,
    endLongitude: -117.035,
    distance: 2053.2,
    duration: 209.5
  },
  {
    _id: ObjectId("68886b5ec34a05703966ac2e"),
    startLatitude: 32.518,
    startLongitude: -117.035,
    endLatitude: 32.5149,
    endLongitude: -117.0382,
    distance: 717.6,
    duration: 94.1
  },
  {
    _id: ObjectId("68886b5fc34a05703966ac37"),
    startLatitude: 32.518,
    startLongitude: -117.035,
    endLatitude: 32.45900929216648,
    endLongitude: -116.97966765227373,
    distance: 15210.6,
    duration: 926.9
  }
]);
