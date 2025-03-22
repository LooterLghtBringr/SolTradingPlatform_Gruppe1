# SolTradingPlatform

Ziel der Gruppenarbeit �Trading Platform I� im Rahmen dieser Vorlesung ist es, eine moderne, wettbewerbsf�hige Internet-Handelsplattform zu entwickeln, welche als Software-Architektur auf Microservices setzt. Jeder Microservice soll dabei genau eine einzige Aufgabe erf�llen und �ber genau definierte Schnittstelen erreichbar sein bzw. mit anderen Microservices �ber die angebotenen Schnittstellen kommunizieren. 
Denkbar w�ren zum Beispiel folgende Microservices:
�	Shopping-Microservice
�	Bezahl-Microservice
�	Rating-Microservice
�	BauernladenAProduktkatalog-Microservice
�	WeingutBProduktkatalgo-Microservice
�	Storno-Microservice
�	W�hrungsrechner-Microservice
Fokus: Integration von �elektronischen (Gesch�fts)-Prozessen� und Microservices.
Nehmen Sie in Ihrer Ausf�hrung auch Bezug auf die im Artikel �Microservices a definition of this new architectural term�
(Microservices - https://martinfowler.com/articles/microservices.html) beschriebenen Konzepte.

# Aufgabe 1b
Beschreiben Sie zuerst den Ansatz �Domain-Driven Design (DDD) im Zusammenhang mit Microservices.
�berlegen Sie welche weiteren Microservices in Zusammenhang mit der Trading Platform sinnvoll w�ren.
Beschreiben Sie danach die Funktionalit�ten / Verantwortlichkeiten der einzelnen Microservices � Stichwort: Business Capabilities
https://deviq.com/domain-driven-design/ddd-overview
https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/best-practice-an-introduction-to-domain-driven-design

Erstellen Sie eine Detailbeschreibung der angebotenen Schnittstellen inkl. Datenaustauschformate

Erstellen Sie eine Detailbeschreibung der Datenhaltung � Stichwort: Decentralized Data Management

## Domain-Driven Design (im Zusammenhang mit Microservices)

## Weitere Microservices

### Service 1

#### Funktionalit�t

#### Verantwortlichkeit (Business Capabilities)

#### Schnittstellen (API inkl. Datenaustauschformate)

#### Datenhaltung (Decentralized Data Management)

### Service 2

#### Funktionalit�t

#### Verantwortlichkeit (Business Capabilities)

#### Schnittstellen (API inkl. Datenaustauschformate)

#### Datenhaltung (Decentralized Data Management)

### Service 3

#### Funktionalit�t

#### Verantwortlichkeit (Business Capabilities)

#### Schnittstellen (API inkl. Datenaustauschformate)

#### Datenhaltung (Decentralized Data Management)

# Aufgabe 2
Erstellen Sie 2 weitere Microservice Produktkataloge:
Erstellen Sie ein Microservice, welches eine Liste von Produkten anbietet.
Der Inhalt der Liste soll dabei aus einem �microservice local datastore� kommen � (Decentralized Data Management).
Ersetzen Sie die hard codierten Werte im MeiShop/ProductList-Controller durch den Aufruf des soeben erstellen Services.
Ein weiterer Produktkatalog-Service soll Produkte aus einem Text File auf einem FTP-Server auslesen oder einem anderen geeigneten
Persistencestore und zur Verf�gung stellen.

## Microservice - Liste von Produkten

## Microservice - Produkte aus Text File

# Aufgabe 3
Skalierung, Ausfallssicherheit und Logging (Design for failure) f�r CreditPaymentService. Detailsbeschreibung:
Publizieren Sie das Service �IEGEasyCreditCardService� mehrfach und rufen Sie die Services im �Round Robin� Stil auf.
Falls es beim Aufruf eines Service zu einem Fehler kommt, soll es eine Retry-Logik geben, au�erdem soll der aufgetretene Fehler
mit Hilfe eines zentralen Logging-Service (gRPC) protokolliert werden. Nach n erfolglosen Versuchen, soll das n�chste Service
aufgerufen werden. Recherchieren Sie zus�tzlich nach einem geeigneten Framework und Skalierungsm�glichkeiten setzen Sie dieses
gegebenenfalls ein.

## Skalierung, Ausfallssicherheit und Logging f�r CreditPaymentService

# Aufgabe 4
(theoretische) �berlegungen zum Einsatz von Asynchronen Kommunikationsstilen in der Handelsplattform.
https://microservices.io/patterns/communication-style/messaging.html

## Einsatz von Asynchronen Kommunikationsstilen

# Aufgabe 5
Schreiben Sie ein zus�tzliches �Paymentservice�. Dieses Payment-Service soll sowohl JSON, XML-Nachrichten als auch Nachrichten
im Format CSV verarbeiten und erzeugen k�nnen. Orientieren Sie sich an dem Pattern - HTTP Content Negotiation in
REST APIs (restfulapi.net)

## PaymentService

# Aufgabe 6
(theoretische) �berlegungen zu einem PaymentService-Broker. Dieses Service soll zwischen Shops und Payment-Services �vermitteln�.

M�gliche Info-Quellen:
https://www.geeksforgeeks.org/broker-pattern/
https://redis.io/solutions/message-broker-pattern-for-microservices-interservice-communication/
http://www.enterpriseintegrationpatterns.com/patterns/messaging/CanonicalDataModel.html
 
Recherchieren Sie dazu zus�tzliche Patterns und Quellen

## PaymentService-Broker

# Aufgabe 7
Webhook-Subscriber: �berlegen und implementieren Sie ein m�gliches Webhook-Szenario.

## Webhook-Subscriber

# Aufgabe 8
Machen Sie sich mit dem Begriff OData vertraut. �berlegen und implementieren Sie ein m�gliches OData (Service & Client)-Szenario.

## OData (Service & Client)-Szenario

# Aufgabe 9
Machen Sie sich mit dem Begriff SAGA-Pattern vertraut. �berlegen und implementieren Sie ein m�gliches SAGA-Pattern
Szenario(Service & Client)-Szenario

Umgang mit Ausfallsicherheit �Stichwort: Design for failure / Resilient Software Design

## SAGA-Pattern (Service & Client)-Szenario

# Aufgabe 10
Machen Sie sich mit dem Begriff �Open Data� vertraut und beschreiben Sie diesen in einigen wenigen S�tzen.
Beschreiben Sie au�erdem m�gliche Anwendungsf�lle im Zusammenhang mit der Handelsplattform.

## Open Data