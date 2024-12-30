# Bakikur Backend API Dokumentation

## Base URL
```
http://localhost:5040
```

## Authentifizierung

### Login
```http
POST /api/auth/login
```

**Request Body:**
```json
{
    "username": "admin",
    "password": "your-password"
}
```

**Response:**
```json
{
    "token": "eyJhbGciOiJIUzI1NiIs..."
}
```

**Verwendung des Tokens:**
- Fügen Sie den Token zu jedem authentifizierten Request im Header hinzu:
```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
```

## Projekte

### Alle Projekte abrufen
```http
GET /api/projects
```

**Response:**
```json
[
    {
        "id": 1,
        "title": "Projekt Titel",
        "description": "Projektbeschreibung",
        "category": "Web",
        "imageUrl": "/uploads/image.jpg",
        "completionDate": "2024-01-30"
    }
]
```

### Einzelnes Projekt abrufen
```http
GET /api/projects/{id}
```

**Response:**
```json
{
    "id": 1,
    "title": "Projekt Titel",
    "description": "Projektbeschreibung",
    "category": "Web",
    "imageUrl": "/uploads/image.jpg",
    "completionDate": "2024-01-30"
}
```

### Neues Projekt erstellen
```http
POST /api/projects
```

**Headers:**
```http
Authorization: Bearer {token}
Content-Type: multipart/form-data
```

**Form Fields:**
- `title` (required): Projekttitel
- `description` (required): Projektbeschreibung
- `category` (required): Projektkategorie
- `completionDate` (required): Fertigstellungsdatum (YYYY-MM-DD)
- `image` (required): Projektbild (File)

**Response:**
```json
{
    "id": 1,
    "title": "Neues Projekt",
    "description": "Beschreibung",
    "category": "Web",
    "imageUrl": "/uploads/new-image.jpg",
    "completionDate": "2024-01-30"
}
```

### Projekt aktualisieren
```http
PUT /api/projects/{id}
```

**Headers:**
```http
Authorization: Bearer {token}
Content-Type: application/json
```

**Request Body:**
```json
{
    "id": 1,
    "title": "Aktualisierter Titel",
    "description": "Aktualisierte Beschreibung",
    "category": "Mobile",
    "imageUrl": "/uploads/existing-image.jpg",
    "completionDate": "2024-02-01"
}
```

**Response:** 204 No Content

### Projekt löschen
```http
DELETE /api/projects/{id}
```

**Headers:**
```http
Authorization: Bearer {token}
```

**Response:** 204 No Content

## Fehler-Responses

### 401 Unauthorized
```json
{
    "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
    "title": "Unauthorized",
    "status": 401
}
```

### 404 Not Found
```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
    "title": "Not Found",
    "status": 404
}
```

### 400 Bad Request
```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "Bad Request",
    "status": 400,
    "errors": {
        "field": ["Fehlermeldung"]
    }
}
```

## Bilder

- Hochgeladene Bilder sind unter `/uploads/{filename}` verfügbar
- Unterstützte Bildformate: JPG, PNG, GIF
- Maximale Dateigröße: 5MB 