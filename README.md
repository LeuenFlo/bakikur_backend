# Bakikur Backend

Backend API für die Bakikur Portfolio-Website.

## Features

- Projekt-Management (CRUD Operationen)
- Admin-Authentifizierung mit JWT
- Bildupload für Projekte
- SQLite Datenbank

## Setup

1. Repository klonen:
```bash
git clone https://github.com/LeuenFlo/bakikur_backend.git
cd bakikur_backend
```

2. Dependencies installieren:
```bash
dotnet restore
```

3. Datenbank initialisieren:
```bash
dotnet ef database update
```

4. Anwendung starten:
```bash
dotnet run
```

Die API ist dann unter `http://localhost:5040` verfügbar.

## API Endpoints

### Authentifizierung

- POST `/api/auth/login`
  ```json
  {
    "username": "admin",
    "password": "your-password"
  }
  ```

### Projekte

- GET `/api/projects` - Alle Projekte abrufen
- GET `/api/projects/{id}` - Einzelnes Projekt abrufen
- POST `/api/projects` - Neues Projekt erstellen (Auth erforderlich)
- PUT `/api/projects/{id}` - Projekt aktualisieren (Auth erforderlich)
- DELETE `/api/projects/{id}` - Projekt löschen (Auth erforderlich)

## Konfiguration

Die Konfiguration erfolgt in `appsettings.json`:

- Datenbank-Verbindung
- JWT-Einstellungen
- Admin-Zugangsdaten
- CORS-Einstellungen

## Lizenz

MIT 