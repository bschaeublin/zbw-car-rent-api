![alt text][logo]

[logo]: ./images/logo.png "Logo Title Text 2"

Autovermietung CarRent
======================

Diese Dokumentation wurde auf der Grundlage von arc 42 erstellt.

**Über arc42**

arc42, das Template zur Dokumentation von Software- und
Systemarchitekturen.

Erstellt von Dr. Gernot Starke, Dr. Peter Hruschka und Mitwirkenden.

Template Revision: 7.0 DE (asciidoc-based), January 2017

© We acknowledge that this document uses material from the arc 42
architecture template, <http://www.arc42.de>. Created by Dr. Peter
Hruschka & Dr. Gernot Starke.

Einführung und Ziele {#section-introduction-and-goals}
====================

Aufgabenstellung {#_aufgabenstellung}
----------------
**Autovermietungssoftware CarRent**

CarRent ist die schnelle und einfache Art, ein Auto für den nächsten Trip zu reservieren. 
Die Reservierung soll einfach und unkompliziert sein.
CarRent Webclient soll für die Mitarbeiter von CarRent und deren Kunden eine intuitive Software sein um 
Autos zu reservieren und mieten.

Das Ziel ist eine Software, welche über Webclients gleichzeitig Fahrzeuge reserviert und verwaltet werden können.
 

Qualitätsziele {#_qualit_tsziele}
--------------
1. Performance: CarRent soll schnelle Antwortzeiten (<1 Sek.) und für parallele Anfragen in kleiner Anzahl ausgelegt sein.
2. Datenqualität: Keine Redundanzen, referentielle Integrität und Datenkonsistenz.
3. Usability: Die Benutzeroberfläche soll übersichtlich und intuitiv gestaltet sein.
4. Installierbarkeit: CarRent soll einfach eingerichtet werden können.
5. Erweiterbarkeit: CarRent soll für zukünftige Erweiterungen offen sein.

Stakeholder {#_stakeholder}
-----------


| Rolle                | Kontakt             | Erwartungshaltung                              |
|----------------------|---------------------|------------------------------------------------|
| CEO CarRent          | Peter Müller        | Stabile und flexible Software, Kostenkontrolle |
| Kunden CarRent       |  K.Meier Vertretung | Schnelle Antwortzeiten, Intuitive Bedienung    |
| Mitarbeiter CarRent  |  M. Bär Vertretung  | Inutive Bedienung, Erweiterbarkeit             |
| Softwareentwickler   |  B. Schäublin       | Klare Anforderungen                            |



Randbedingungen {#section-architecture-constraints}
===============
<dl>
<dt> Developer Tools </dt>
<dd> VS2017 / R# … </dd>
<dd> Visual Studio Code / … </dd>
<dt>  Client Tier </dt>
<dd> Angular </dd>
<dd> Apache / Nginx </dd>
<dt>  Server Tier </dt>
<dd> ASP.NET Core </dd>
<dd> NLog </dd>
<dd> NHibernate if RDBMS </dd>
<dd> MongoDB if DocumentDB </dd>
<dt>  Testing </dt>
<dd> Z.B MSTest / Moq / FluentAssertion </dd>
<dt>  Data Tier (Choose) </dt>
<dd> SQL Server 2016 Express | Developer </dd>
<dd> MongoDB </dd>
<dt>  Build, Release und Metrik Tools </dt>
<dd> NuGet </dd>
<dd> Cake </dd>
<dd> Sonar </dd>
<dd> Proget </dd>
</dl>

Kontextabgrenzung {#section-system-scope-and-context}
=================
CarRent ist ein egenständiges Reservierungssystem für Autos.  
Systemintern gibt es eine Schnittstelle zwischen GUI und BussinesAplication sowie zwischen BussinesAplication und Datenbank.  
CarRent hat keine Schnittstellen zu Fremdsystemen wie z.B. Zahlungssystemen oder Reparaturverwaltung für Autos.

Fachlicher Kontext {#_fachlicher_kontext}
------------------

![alt text](./images/FachlicherKontext.png "Fachlicher Kontext")

Kunden benutzen in einer ersten Phase das CarRent System nur über einen Sachbearbeiter. Der Sachbearbeiter führt alle interaktionen mit dem CarRent-System über das WebFrontend.  
In einer späteren Phase erhalten die Kunden ein eigenes Benutzerinterface. In diese können sie dann selbstständig Autos reservieren.

Technischer Kontext {#_technischer_kontext}
-------------------

![alt text](./images/DeploymentDiagramm.png "Technischer Kontext")

Über ein WebBrowser wird via HTTP auf den WebClient zugegriffen.  
Der WebClient macht seinerseitz Abragen über HTTP auf die WebAPI.
Diese speichert dann die Daten lokal auf der MSSQL Datenbank mit einem SQL-Protokoll.

Lösungsstrategie {#section-solution-strategy}
================
Die folgende Tabelle stellt die Qualitätsziele von CarRent passenden Architekturansätzen gegenüber, und erleichtert so einen Einstieg in die Lösung.

| Qualitätsziel        | Dem zuträgliche Ansätze in der Architektur                                            |
|----------------------|---------------------------------------------------------------------------------------|
| Performance          | mit den HTTP Requests werden nur einzelne Daten abgefragt, keine komplexen Strukturen |        
| Datenqualität        | Die MSSQL Datenbank übernimmt dei persistente Speicherung der Daten.                  |
| Usability            | Das WebFrontend wird mit Agular einach und übersichtlich gestaltet                    |
| Installierbarkeit    | Das WebFrontend benötigt keine Installation.                                          |
| Erweiterbarkeit      | Objektorientierte Programmierung und stabile Interfaces                               |

Bausteinsicht {#section-building-block-view}
=============

Whitebox Gesamtsystem {#_whitebox_gesamtsystem}
---------------------

***&lt;Übersichtsdiagramm&gt;***

Begründung

:   *&lt;Erläuternder Text&gt;*

Enthaltene Bausteine

:   *&lt;Beschreibung der enhaltenen Bausteine (Blackboxen)&gt;*

Wichtige Schnittstellen

:   *&lt;Beschreibung wichtiger Schnittstellen&gt;*

### &lt;Name Blackbox 1&gt; {#__name_blackbox_1}

*&lt;Zweck/Verantwortung&gt;*

*&lt;Schnittstelle(n)&gt;*

*&lt;(Optional) Qualitäts-/Leistungsmerkmale&gt;*

*&lt;(Optional) Ablageort/Datei(en)&gt;*

*&lt;(Optional) Erfüllte Anforderungen&gt;*

*&lt;(optional) Offene Punkte/Probleme/Risiken&gt;*

### &lt;Name Blackbox 2&gt; {#__name_blackbox_2}

*&lt;Blackbox-Template&gt;*

### &lt;Name Blackbox n&gt; {#__name_blackbox_n}

*&lt;Blackbox-Template&gt;*

### &lt;Name Schnittstelle 1&gt; {#__name_schnittstelle_1}

…

### &lt;Name Schnittstelle m&gt; {#__name_schnittstelle_m}

Ebene 2 {#_ebene_2}
-------

### Whitebox *&lt;Baustein 1&gt;* {#_whitebox_emphasis_baustein_1_emphasis}

*&lt;Whitebox-Template&gt;*

### Whitebox *&lt;Baustein 2&gt;* {#_whitebox_emphasis_baustein_2_emphasis}

*&lt;Whitebox-Template&gt;*

…

### Whitebox *&lt;Baustein m&gt;* {#_whitebox_emphasis_baustein_m_emphasis}

*&lt;Whitebox-Template&gt;*

Ebene 3 {#_ebene_3}
-------

### Whitebox &lt;\_Baustein x.1\_&gt; {#_whitebox_baustein_x_1}

*&lt;Whitebox-Template&gt;*

### Whitebox &lt;\_Baustein x.2\_&gt; {#_whitebox_baustein_x_2}

*&lt;Whitebox-Template&gt;*

### Whitebox &lt;\_Baustein y.1\_&gt; {#_whitebox_baustein_y_1}

*&lt;Whitebox-Template&gt;*

Laufzeitsicht {#section-runtime-view}
=============

*&lt;Bezeichnung Laufzeitszenario 1&gt;* {#__emphasis_bezeichnung_laufzeitszenario_1_emphasis}
----------------------------------------

-   &lt;hier Laufzeitdiagramm oder Ablaufbeschreibung einfügen&gt;

-   &lt;hier Besonderheiten bei dem Zusammenspiel der Bausteine in
    diesem Szenario erläutern&gt;

*&lt;Bezeichnung Laufzeitszenario 2&gt;* {#__emphasis_bezeichnung_laufzeitszenario_2_emphasis}
----------------------------------------

…

*&lt;Bezeichnung Laufzeitszenario n&gt;* {#__emphasis_bezeichnung_laufzeitszenario_n_emphasis}
----------------------------------------

…

Verteilungssicht {#section-deployment-view}
================

Infrastruktur Ebene 1 {#_infrastruktur_ebene_1}
---------------------

***&lt;Übersichtsdiagramm&gt;***

Begründung

:   *&lt;Erläuternder Text&gt;*

Qualitäts- und/oder Leistungsmerkmale

:   *&lt;Erläuternder Text&gt;*

Zuordnung von Bausteinen zu Infrastruktur

:   *&lt;Beschreibung der Zuordnung&gt;*

Infrastruktur Ebene 2 {#_infrastruktur_ebene_2}
---------------------

### *&lt;Infrastrukturelement 1&gt;* {#__emphasis_infrastrukturelement_1_emphasis}

*&lt;Diagramm + Erläuterungen&gt;*

### *&lt;Infrastrukturelement 2&gt;* {#__emphasis_infrastrukturelement_2_emphasis}

*&lt;Diagramm + Erläuterungen&gt;*

…

### *&lt;Infrastrukturelement n&gt;* {#__emphasis_infrastrukturelement_n_emphasis}

*&lt;Diagramm + Erläuterungen&gt;*

Querschnittliche Konzepte {#section-concepts}
=========================

*&lt;Konzept 1&gt;* {#__emphasis_konzept_1_emphasis}
-------------------

*&lt;Erklärung&gt;*

*&lt;Konzept 2&gt;* {#__emphasis_konzept_2_emphasis}
-------------------

*&lt;Erklärung&gt;*

…

*&lt;Konzept n&gt;* {#__emphasis_konzept_n_emphasis}
-------------------

*&lt;Erklärung&gt;*

Entwurfsentscheidungen {#section-design-decisions}
======================

Qualitätsanforderungen {#section-quality-scenarios}
======================

Qualitätsbaum {#_qualit_tsbaum}
-------------

Qualitätsszenarien {#_qualit_tsszenarien}
------------------

Risiken und technische Schulden {#section-technical-risks}
===============================

Glossar {#section-glossary}
=======

+----------------------+----------------------------------------------+
| Begriff              | Definition                                   |
+======================+==============================================+
| *&lt;Begriff-1&gt;*  | *&lt;Definition-1&gt;*                       |
+----------------------+----------------------------------------------+
| *&lt;Begriff-2*      | *&lt;Definition-2&gt;*                       |
+----------------------+----------------------------------------------+


