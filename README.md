# SolTradingPlatform

Ziel der Gruppenarbeit „Trading Platform I“ im Rahmen dieser Vorlesung ist es, eine moderne, wettbewerbsfähige Internet-Handelsplattform zu entwickeln, welche als Software-Architektur auf Microservices setzt. Jeder Microservice soll dabei genau eine einzige Aufgabe erfüllen und über genau definierte Schnittstelen erreichbar sein bzw. mit anderen Microservices über die angebotenen Schnittstellen kommunizieren. 
Denkbar wären zum Beispiel folgende Microservices:
•	Shopping-Microservice
•	Bezahl-Microservice
•	Rating-Microservice
•	BauernladenAProduktkatalog-Microservice
•	WeingutBProduktkatalgo-Microservice
•	Storno-Microservice
•	Währungsrechner-Microservice
Fokus: Integration von „elektronischen (Geschäfts)-Prozessen“ und Microservices.
Nehmen Sie in Ihrer Ausführung auch Bezug auf die im Artikel „Microservices a definition of this new architectural term“
(Microservices - https://martinfowler.com/articles/microservices.html) beschriebenen Konzepte.

# Aufgabe 1b
Beschreiben Sie zuerst den Ansatz „Domain-Driven Design (DDD) im Zusammenhang mit Microservices.
Überlegen Sie welche weiteren Microservices in Zusammenhang mit der Trading Platform sinnvoll wären.
Beschreiben Sie danach die Funktionalitäten / Verantwortlichkeiten der einzelnen Microservices – Stichwort: Business Capabilities
https://deviq.com/domain-driven-design/ddd-overview
https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/best-practice-an-introduction-to-domain-driven-design

Erstellen Sie eine Detailbeschreibung der angebotenen Schnittstellen inkl. Datenaustauschformate

Erstellen Sie eine Detailbeschreibung der Datenhaltung – Stichwort: Decentralized Data Management

## Domain-Driven Design (im Zusammenhang mit Microservices)

## Weitere Microservices

### Service 1

#### Funktionalität

#### Verantwortlichkeit (Business Capabilities)

#### Schnittstellen (API inkl. Datenaustauschformate)

#### Datenhaltung (Decentralized Data Management)

### Service 2

#### Funktionalität

#### Verantwortlichkeit (Business Capabilities)

#### Schnittstellen (API inkl. Datenaustauschformate)

#### Datenhaltung (Decentralized Data Management)

### Service 3

#### Funktionalität

#### Verantwortlichkeit (Business Capabilities)

#### Schnittstellen (API inkl. Datenaustauschformate)

#### Datenhaltung (Decentralized Data Management)

# Aufgabe 2
Erstellen Sie 2 weitere Microservice Produktkataloge:
Erstellen Sie ein Microservice, welches eine Liste von Produkten anbietet.
Der Inhalt der Liste soll dabei aus einem „microservice local datastore“ kommen – (Decentralized Data Management).
Ersetzen Sie die hard codierten Werte im MeiShop/ProductList-Controller durch den Aufruf des soeben erstellen Services.
Ein weiterer Produktkatalog-Service soll Produkte aus einem Text File auf einem FTP-Server auslesen oder einem anderen geeigneten
Persistencestore und zur Verfügung stellen.

## Microservice - Liste von Produkten

## Microservice - Produkte aus Text File

# Aufgabe 3
Skalierung, Ausfallssicherheit und Logging (Design for failure) für CreditPaymentService. Detailsbeschreibung:
Publizieren Sie das Service „IEGEasyCreditCardService“ mehrfach und rufen Sie die Services im „Round Robin“ Stil auf.
Falls es beim Aufruf eines Service zu einem Fehler kommt, soll es eine Retry-Logik geben, außerdem soll der aufgetretene Fehler
mit Hilfe eines zentralen Logging-Service (gRPC) protokolliert werden. Nach n erfolglosen Versuchen, soll das nächste Service
aufgerufen werden. Recherchieren Sie zusätzlich nach einem geeigneten Framework und Skalierungsmöglichkeiten setzen Sie dieses
gegebenenfalls ein.

## Skalierung, Ausfallssicherheit und Logging für CreditPaymentService

# Aufgabe 4
(theoretische) Überlegungen zum Einsatz von Asynchronen Kommunikationsstilen in der Handelsplattform.
https://microservices.io/patterns/communication-style/messaging.html

## Einsatz von Asynchronen Kommunikationsstilen

# Aufgabe 5
Schreiben Sie ein zusätzliches „Paymentservice“. Dieses Payment-Service soll sowohl JSON, XML-Nachrichten als auch Nachrichten
im Format CSV verarbeiten und erzeugen können. Orientieren Sie sich an dem Pattern - HTTP Content Negotiation in
REST APIs (restfulapi.net)

## PaymentService

# Aufgabe 6
(theoretische) Überlegungen zu einem PaymentService-Broker. Dieses Service soll zwischen Shops und Payment-Services „vermitteln“.

Mögliche Info-Quellen:
https://www.geeksforgeeks.org/broker-pattern/
https://redis.io/solutions/message-broker-pattern-for-microservices-interservice-communication/
http://www.enterpriseintegrationpatterns.com/patterns/messaging/CanonicalDataModel.html
 
Recherchieren Sie dazu zusätzliche Patterns und Quellen

## PaymentService-Broker

# Aufgabe 7
Webhook-Subscriber: Überlegen und implementieren Sie ein mögliches Webhook-Szenario.

## Webhook-Subscriber

# Aufgabe 8
Machen Sie sich mit dem Begriff OData vertraut. Überlegen und implementieren Sie ein mögliches OData (Service & Client)-Szenario.

## OData (Service & Client)-Szenario

# Aufgabe 9
Machen Sie sich mit dem Begriff SAGA-Pattern vertraut. Überlegen und implementieren Sie ein mögliches SAGA-Pattern
Szenario(Service & Client)-Szenario

Umgang mit Ausfallsicherheit –Stichwort: Design for failure / Resilient Software Design

## SAGA-Pattern (Service & Client)-Szenario

# Aufgabe 10
Machen Sie sich mit dem Begriff „Open Data“ vertraut und beschreiben Sie diesen in einigen wenigen Sätzen.
Beschreiben Sie außerdem mögliche Anwendungsfälle im Zusammenhang mit der Handelsplattform.

## Open Data