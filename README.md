# SolTradingPlatform

Ziel der Gruppenarbeit „Trading Platform I“ im Rahmen dieser Vorlesung ist es, eine moderne, wettbewerbsfähige Internet-Handelsplattform 
zu entwickeln, welche als Software-Architektur auf Microservices setzt. Jeder Microservice soll dabei genau eine einzige Aufgabe erfüllen 
und über genau definierte Schnittstelen erreichbar sein bzw. mit anderen Microservices über die angebotenen Schnittstellen kommunizieren. 

Denkbar wären zum Beispiel folgende Microservices:
•	Shopping-Microservice
•	Bezahl-Microservice
•	Rating-Microservice
•	BauernladenAProduktkatalog-Microservicey
•	WeingutBProduktkatalgo-Microservice
•	Storno-Microservice
•	Währungsrechner-Microservice
Fokus: Integration von „elektronischen (Geschäfts)-Prozessen“ und Microservices.
Nehmen Sie in Ihrer Ausführung auch Bezug auf die im Artikel „Microservices a definition of this new architectural term“
(Microservices - https://martinfowler.com/articles/microservices.html) beschriebenen Konzepte.

# Aufgabe 1b - TODO: implementierte Microservices beschreiben (Funktionalität, Schnittstellen, Datenaustaschformate, Datenhaltung)
Beschreiben Sie zuerst den Ansatz „Domain-Driven Design (DDD) im Zusammenhang mit Microservices.
Überlegen Sie welche weiteren Microservices in Zusammenhang mit der Trading Platform sinnvoll wären.
Beschreiben Sie danach die Funktionalitäten / Verantwortlichkeiten der einzelnen Microservices – Stichwort: Business Capabilities
https://deviq.com/domain-driven-design/ddd-overview
https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/best-practice-an-introduction-to-domain-driven-design

Erstellen Sie eine Detailbeschreibung der angebotenen Schnittstellen inkl. Datenaustauschformate

Erstellen Sie eine Detailbeschreibung der Datenhaltung – Stichwort: Decentralized Data Management

## Domain-Driven Design (im Zusammenhang mit Microservices)

DDD ist ein Softwareentwicklungsansatz, der komplexe Domänen in klar abgegrenzte Bounded Contexts unterteilt.
Jeder Bounded Context definiert eine eindeutige Geschäftsdomäne mit eigener Sprache (Ubiquitous Language) und Regeln.

Microservices setzen DDD-Prinzipien ideal um:

-Jeder Microservice entspricht einem Bounded Context
-Business Capabilities (Geschäftsfähigkeiten) werden isoliert abgebildet
-Autonome Teams pro Service (Conway’s Law)
-Decentralized Data Management (jeder Service verwaltet seine Daten selbst)

Vorteile:  
✅ Klare Trennung der Verantwortlichkeiten  
✅ Unabhängige Skalierbarkeit  
✅ Technologische Freiheit pro Service  

## Weitere Microservices

Zusätzlich zu den genannten Services wären sinnvoll:

| Microservice          | Business Capability     | Verantwortlichkeit                     |
|:----------------------|:------------------------|:---------------------------------------|
| User-Service          | Benutzerverwaltung      | Registrierung, Login, Profilmanagement |
| Order-Service         | Auftragsabwicklung      | Bestellungen erstellen/tracken         |
| Notification-Service  | Benachrichtigungen      | E-Mails/SMS zu Bestellungen            |
| Search-Service        | Produktsuche            | Volltextsuche, Filterung               |
| Analytics-Service     | Geschäftsanalysen       | Verkaufsstatistiken                    |

### Shopping-Service
#### Funktionalität
- Warenkorbverwaltung (Hinzufügen/Entfernen von Artikeln)
- Checkout-Prozess-Initialisierung
- Preisberechnung inkl. Rabatte

#### Verantwortlichkeit (Business Capabilities)
- Abbildung des Einkaufserlebnisses
- Zusammenführung von Produktdaten aus verschiedenen Katalogen
- Vorbereitung der Bestellung für den Order-Service

#### Schnittstellen (API inkl. Datenaustauschformate)
    http
    POST /api/cart/items
    Content-Type: application/json
    {
      "productId": "prod_123",
      "quantity": 2
    }

    GET /api/cart
    → Returns:
    {
      "items": [
        {
          "productId": "prod_123",
          "name": "Bio-Äpfel",
          "price": 2.99,
          "quantity": 2
        }
      ],
      "total": 5.98
    }

#### Datenhaltung (Decentralized Data Management)
- Redis: Für Warenkorbdaten (schneller Key-Value-Store)
- TTL: 24h für nicht abgeschlossene Warenkörbe
- Isoliert: Kein Zugriff auf Produktdatenbanken anderer Services

### Payment-Service
#### Funktionalität
- Zahlungsabwicklung (Kreditkarte, PayPal, etc.)
- Transaktionsverfolgung
- Rückerstattungen

#### Verantwortlichkeit (Business Capabilities)
- Sicherer Zahlungsabschluss
- Compliance (PCI-DSS)
- Zahlungsgateway-Integration

#### Schnittstellen
    http
    POST /api/payments
    {
      "orderId": "ord_789",
      "amount": 29.99,
      "currency": "EUR",
      "paymentMethod": "creditcard",
      "cardToken": "tok_visa123"
    }

    → Response:
    {
      "paymentId": "pay_456",
      "status": "completed"
    }

#### Datenhaltung
- PostgreSQL: ACID-konforme Transaktionsdaten
- Verschlüsselt: Kreditkartentokens separat gespeichert
- Kein Zugriff: Auf Bestelldaten (nur Order-ID-Referenz)

### Product-Service (BauernladenA)
#### Funktionalität
- Produktdatenverwaltung
- Lagerbestandsaktualisierung
- Kategorisierung

#### Verantwortlichkeit
- Zentrale Produktwahrheit für einen Anbieter
- Verfügbarkeitsmanagement
- Preispflege

#### Schnittstellen
    http
    GET /api/products?category=fruits
    → Returns:
    [
      {
        "id": "prod_123",
        "name": "Bio-Äpfel",
        "price": 2.99,
        "stock": 150,
        "farmerId": "farm_A"
      }
    ]

#### Datenhaltung
- MongoDB: Flexibles Schema für variable Produktattribute
- Ownership: Nur dieser Service schreibt Produktdaten
- Cache: Redis für häufig abgerufene Produkte

### Order-Service
#### Funktionalität
- Bestellungsmanagement
- Statusverfolgung (Bearbeitung, Versand)
- Retourenverwaltung

#### Verantwortlichkeit
- Lebenszyklus einer Bestellung
- Integration von Payment und Shipping
- Rechnungsgenerierung

#### Schnittstellen
    http
    POST /api/orders
    {
      "userId": "usr_42",
      "items": [
        {
          "productId": "prod_123",
          "quantity": 2
        }
      ]
    }

    → Response:
    {
      "orderId": "ord_789",
      "status": "processing"
    }

#### Datenhaltung
- MySQL: Relationale Struktur für Bestelldetails
- Event Sourcing: Zustandsänderungen als Event-Stream
- Isoliert: Kein direkter Zugriff auf Warenkorbdaten

### Notification-Service
#### Funktionalität
- Versand von E-Mails/SMS
- Benachrichtigungsvorlagen
- Zustellungsverfolgung

#### Verantwortlichkeit
- Kommunikationskanal zu Kunden
- Personalisierte Nachrichten
- Opt-in Management

#### Schnittstellen
    http
    POST /api/notifications
    {
      "type": "order_confirmation",
      "userId": "usr_42",
      "orderId": "ord_789",
      "channel": "email"
    }

#### Datenhaltung
- Cassandra: Skalierbare Speicherung von Sendelogs
- Keine Persistenz: Nachrichtenvorlagen im Code
- Temporal: Logs nach 90 Tagen automatisch gelöscht

# Aufgabe 2 - "Coding Produktkataloge"
Erstellen Sie 2 weitere Microservice Produktkataloge:
Erstellen Sie ein Microservice, welches eine Liste von Produkten anbietet.
Der Inhalt der Liste soll dabei aus einem „microservice local datastore“ kommen – (Decentralized Data Management).
Ersetzen Sie die hard codierten Werte im MeiShop/ProductList-Controller durch den Aufruf des soeben erstellen Services.
Ein weiterer Produktkatalog-Service soll Produkte aus einem Text File auf einem FTP-Server auslesen oder einem anderen geeigneten
Persistencestore und zur Verfügung stellen.

## Microservice ProductCatalogJson - Produkte aus JSON-Datei
Dieser Microservice stellt eine Liste von Produkten bereit, die in einer JSON-Datei gespeichert sind. Die JSON-Datei wird lokal im Microservice gespeichert und kann über
eine REST-API abgerufen werden. Der Microservice implementiert grundlegende CRUD-Operationen (Create, Read, Update, Delete) für die Produkte.

## Microservice ProductCatalogSqlite - Produkte aus Sqlite-Datenbank
Dieser Microservice stellt eine Liste von Produkten bereit, die in einer Sqlite-Datenbank gespeichert sind. Die Datenbank wird lokal im Microservice gespeichert und kann über
eine REST-API abgerufen werden.

# Aufgabe 3 - Coding (retry, zentrales Logging-Service, Fallback) - TODO: kurze beschreibung
Skalierung, Ausfallssicherheit und Logging (Design for failure) für CreditPaymentService. Detailsbeschreibung:
Publizieren Sie das Service „IEGEasyCreditCardService“ mehrfach und rufen Sie die Services im „Round Robin“ Stil auf.
Falls es beim Aufruf eines Service zu einem Fehler kommt, soll es eine Retry-Logik geben, außerdem soll der aufgetretene Fehler
mit Hilfe eines zentralen Logging-Service (gRPC) protokolliert werden. Nach n erfolglosen Versuchen, soll das nächste Service
aufgerufen werden. Recherchieren Sie zusätzlich nach einem geeigneten Framework und Skalierungsmöglichkeiten setzen Sie dieses
gegebenenfalls ein.

## Skalierung, Ausfallssicherheit und Logging für CreditPaymentService

# Aufgabe 4 - Ansynchrone Kommunikation
(theoretische) Überlegungen zum Einsatz von Asynchronen Kommunikationsstilen in der Handelsplattform.

https://microservices.io/patterns/communication-style/messaging.html

## Einsatz von Asynchronen Kommunikationsstilen

### Synchrone vs. Asynchrone Kommunikation

Synchrone Kommunikation - einfach, konsistent, unmittelbar
Sender und Empfänger einer Nachricht sind gleichzeitig aktiv. Ist die gebräuchlichste Art der Interaktion bei Web-API's welche Protokelle wie HTTP oder RPC verwenden. 
Stellen einer Anfrage -> Erhalten der Anfrage: Es kann gewährleistet werden, dass Daten aktuell und über alle Dienste hinweg konsistent sind. 
Datenfluss ist wie ein Telefonanruf aufgebaut: Ein Dienst ruft einen anderen an - er wartet bis der andere Dienst abhebt. Davor kann er nicht weiterarbeiten!
Nachteile: Koppelung, Latenz, Verfügbarkeit. Dienste werden durch Abhängigkeiten schwerer zu skalieren. Anfoderungen blockieren, Risiko für Time-Outs 

Asynchrone Kommunikation - entkoppelt, flexibel, resilient
Sender und Empfänger sind nicht gleichzeitig aktiv. Weniger gebräuchlich aber flexiblere Art der Kommunikation bei Web-API's, Protokolle wie AMQP oder MQTT. 
Stellen der Anfrage -> Abholen der Anfrage: Dienste sind nicht abhängig von einander. Dienste sind flexibel skalierbar und ausfallssicherer. 
Datenfluss ist wie eine Nachrichtendienst aufgebaut: Ein Dienst schickt eine Nachricht und der andere Dienst reagiert wenn er Zeit hat. Der Sender kann weiterarbeiten. 
Nachteile: Komplexität, Inkonsistenzen, Latenz. Entwerfen, Testen und Debuggen wird aufwändiger, Kompromiss zwischen Zuverlässigkeit und Latenz. 

### Anwendungsbeispiel aus der Plattform

Order-Service -> Notification Service: 
Bestellung wird erstellt und asynchron "abgeholt". Design for Failure Prinzip: Wenn einer der Dienste Offline ist kann der andere weiterarbeiten. 
Order Service muss nicht auf Antwort warten, reduziert Antwortzeit für Benutzer und erhöht Systemgeschwindigkeit

Payment-Service -> Order-Service
Nach erfolgreicher Zahlung wird Bestellung freigegeben
Fehlertoleranz erhöht sich - Zahlung erfolgreich und Order-Service nicht erreichbar: Könnte kritisch sein! 
Bei asynchroner Kommunikation kein Problem, Nachricht wird später verarbeitet. 

Analytics-Service
Nutzungsdaten werden als Events an den Analytics Service weitergegeben, dieser konsumiert diese nur und ist dahingehend nicht abhängig. 
Asynchrone Kommunikation durch bessere skalierbarkeit ist durch die potenziell große Anzahl von Events die bessere Wahl. 

### Mögliche Technologien 

RabbitMQ, Apache Kafka

# Aufgabe 5 - Coding Paymentservice - TODO: kurze beschreibung
Schreiben Sie ein zusätzliches „Paymentservice“. Dieses Payment-Service soll sowohl JSON, XML-Nachrichten als auch Nachrichten
im Format CSV verarbeiten und erzeugen können. Orientieren Sie sich an dem Pattern - HTTP Content Negotiation in
REST APIs (restfulapi.net)

## PaymentService

# Aufgabe 6 - (theoretische) Überlegungen zu einem PaymentService-Broker
(theoretische) Überlegungen zu einem PaymentService-Broker. Dieses Service soll zwischen Shops und Payment-Services „vermitteln“.

Mögliche Info-Quellen:
https://www.geeksforgeeks.org/broker-pattern/
https://redis.io/solutions/message-broker-pattern-for-microservices-interservice-communication/
http://www.enterpriseintegrationpatterns.com/patterns/messaging/CanonicalDataModel.html
 
Recherchieren Sie dazu zusätzliche Patterns und Quellen

## PaymentService-Broker

### Broker-Pattern
Architekturprinzip, bei dem ein Vermittler (= "Broker") als Mittelschicht zwischen Client und Server fungiert. 
Beteiligte kennen sich also nicht direkt sondern kommunizieren ausschließlich über Broker. 
Vorteile: Lose Koppelung, Flexible Erweiterung (wenn zb. neue Payment Services integriert werden sollen muss der Shop nicht angepasst werden), zentralisierte Steuerung

### Sinnhaftigkeit für Trading Plattform
Unterschiedliche Payment-Servies die sich in Zukunft auch verändern können:
Aktuell: Kreditkartenzahlung, Paypal, Vorauskasse, Rechnung
Zukunft: Kryptowährungen
Broker nimmt alle Anfragen entgegen und entscheidet anhand eines Sets von Regeln und Konfigurationen welches Payment-Service aufgerufen wird 

### Message Broker (anhand des Redis-Beispiels)
Ein Message Broker wie Redis Streams oder Kafka unterstützt asynchrone Kommunikation und erlaubt daher mehreren Clients (z.B. Payment-Services) auf Nachrichten reagieren zu können. 
Entwurf eines Redis-basierter Payment-Broker:

1. Broker publiziert Nachricht in Stream "payment_requests"
2. Je nach Service-Verfügbarkeit oder Methode konsumiert der passende Service die Nachricht

Vorteile laut Redis:

- Asynchrone Verarbeitung
- Automatische Wiederholung bei Fehlern
- Entkopplung von Sender / Empfänger

### Canonical Data Model (= CDM)
Stellst sicher, dall alle Services ein einheitliches Format verwenden. Somit eine Art "Korsett" für die Daten
Vorteile:
- Vereinfachte Integration neuer Services
- Klar definierte Kommunikation
- Weniger Konvertierungslogik

### Conclusio 
Ein PaymentService-Broker in der Microservice-basierten Handelsplattform…

…vermittelt intelligent zwischen verschiedenen Zahlungsdiensten
…nutzt idealerweise einen Message-Broker für asynchrone, fehlertolerante Verarbeitung
…arbeitet mit einem Canonical Data Model, um alle Services unabhängig und standardisiert anzusprechen
…erlaubt einfache Erweiterbarkeit und bessere Skalierbarkeit im Zahlungssystem


# Aufgabe 7 - Coding Webhook - TODO: kurze beschreibung
Webhook-Subscriber: Überlegen und implementieren Sie ein mögliches Webhook-Szenario.

## Webhook-Subscriber

# Aufgabe 8 - Coding OData
Machen Sie sich mit dem Begriff OData vertraut. Überlegen und implementieren Sie ein mögliches OData (Service & Client)-Szenario.

## OData (Service & Client)-Szenario
In dem Microservice ProductCatalogJson wurde OData implementiert. Die Produkte werden als QueryableCollection bereitgestellt und
es werden im ODataClient (Console Application) exemplarisch 3 Abfragen durchgeführt und ausgegeben.

# Aufgabe 9 - Coding SAGA-Pattern
Machen Sie sich mit dem Begriff SAGA-Pattern vertraut. Überlegen und implementieren Sie ein mögliches SAGA-Pattern
Szenario(Service & Client)-Szenario

Umgang mit Ausfallsicherheit –Stichwort: Design for failure / Resilient Software Design

## SAGA-Pattern (Service & Client)-Szenario
Das SAGA Pattern wurde im Microservice PaymentService implementiert. Es werden zwei Endpunkte bereitgestellt (ReservePayment und CompensatePayment).
Diese Endpunkte sind für die Reservierung und Kompensation von Zahlungen zuständig.

# Aufgabe 10 - Open Data
Machen Sie sich mit dem Begriff „Open Data“ vertraut und beschreiben Sie diesen in einigen wenigen Sätzen.
Beschreiben Sie außerdem mögliche Anwendungsfälle im Zusammenhang mit der Handelsplattform.

## Open Data
Open Data bezeichnet frei zugängliche Daten, die von Organisationen, Regierungen oder Unternehmen öffentlich bereitgestellt werden und ohne rechtliche, technische oder
finanzielle Beschränkungen genutzt, weiterverarbeitet und geteilt werden können. Diese Daten sind in der Regel in maschinenlesbaren Formaten verfügbar und folgen offenen
Standards, um maximale Interoperabilität zu gewährleisten.

Charakteristika sind:
- Lizenzfreiheit (z. B. CC0, Creative Commons)
- Offene Formate (CSV, JSON, XML)
- Kostenfreier Zugang

Mögliche Anwendungsfälle für Handelsplattformen:
- Open Data kann Handelsplattformen erheblich bereichern, indem externe Datenquellen integriert werden. Wirtschaftsdaten von Statistikämtern können zur Marktanalyse und
Trendvorhersage genutzt werden, während Wetterdaten besonders für Rohstoff- und Agrarmärkte relevant sind. Demografische Daten ermöglichen eine bessere Zielgruppensegmentierung und
personalisierte Produktempfehlungen.

- Verkehrsdaten können Logistikkosten optimieren und Lieferzeiten verbessern, während Geodaten standortbasierte Services und regionale Marktanalysen unterstützen. Wechselkurse und
Finanzmarktdaten von Zentralbanken können für internationale Transaktionen und Risikobewertungen eingesetzt werden.

- Zusätzlich können Open Data-Quellen die Compliance erleichtern, indem beispielsweise Sanktionslisten oder Handelsregulierungen automatisch abgeglichen werden. Die Integration von
Open Data erhöht somit die Datenqualität, reduziert Kosten für proprietäre Datenquellen und ermöglicht innovative Services, die der Handelsplattform einen Wettbewerbsvorteil
verschaffen können.

Weitere Anwendungsfälle könnten sein:
- Produktdatenbank - Nutzung offener Warenklassifikationen (z. B. UNSPSC) zur Standardisierung der Produktkategorien.

- Marktanalysen - Integration öffentlicher Demografie-Daten (z. B. Statistik Austria) für zielgruppenspezifische Produktempfehlungen.

- Preisbenchmarking - Vergleich mit offenen Preisindizes oder EU-weiten Handelsdaten zur dynamischen Preisoptimierung.

- Nachhaltigkeitsbewertung - Einbindung von CO₂-Datenbanken (z. B. OpenFootprint) für Ökobilanzierungen.

- Geodaten - Nutzung von OpenStreetMap für Lieferzeitenberechnungen oder Standortanalysen.