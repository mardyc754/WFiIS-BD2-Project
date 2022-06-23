
# Przetwarzanie dokumentów XML - Program do zarządzania menu restauracji

## Marta Dychała

----------------------------------

## 1. Cel projektu

Tematem projektu jest program, który ułatwia zarządzanie menu restauracji. Menu to zapisywane jest w postaci dokumentu XML. Zarządzając menu, dokonuje się modyfikacji na dokumentach XML znajdujących się w bazie danych.

## 2. Wykorzystane technologie

Program został napisany w języku C# po stronie aplikacji konsolowej, zaś do zarządzania bazą danych wykorzystano Microsoft SQL Server. Jako interfejs będący pośrednikiem między bazą danych a API wykorzystano ADO.NET.

Do napisania programu wykorzystano program Visual Studio 2019 w części projektu wykorzystującej C# oraz Microsoft SQL Server Management Studio 18 po stronie bazy danych.

W projekcie używany był Microsoft .NET Framework 4.7.2.

## 3. Struktura projektu

### 3.1. Baza danych

Baza danych projektu `Project` składa się z jednej tabeli - `ProjectXML`. Tabela ta posiada tylko jeden rekord i jedną kolumnę (`menuXML`), w którym znajduje się XML zawierający menu restauracji.

Wszelkie operacje wykonywane za pomocą API, modyfikują jedyny XML znajdujący się w tabeli `ProjectXML`.

Do poprawnego działania API wymagane jest dodanie rekordu do bazy danych posiadającego strukturę opisaną poniżej:

- Węzłem najwyżej w hierarchii jest `<Menu />`,
- `<Category />` są kategoriami w omawianym menu - tworzą poddokumenty składające się z węzłów `<Product />`. To co odróżnia każdą kategorię, to atrybuty:

  - `id` - id kategorii
  - `name` - nazwa kategorii

- `<Product />` to węzły produktów, które posiadają nazwę (węzeł `<Name>`) oraz cenę, bądź ceny (węzeł `<Prices />` posiadający węzły `<Price />`). Ponadto jako atrybut, każdy produkt posiada `id` oraz może posiadać informację, czy jest wegetariański (atrybut `vegetarian`)
- `<Price>` - cena za rozmiar danego produktu, który określany jest przez atrybut `type`. Atrybut ten może posiadać jedynie trzy wartości:
  
  - _medium_ - cena za zwykły rozmiar - węzeł `<Prices />` musi posiadać potomka `<Price />` posiadającego ten atrybut
  - _small_ - cena za mniejszy rozmiar (atrybut opcjonalny)
  - _large_ - cena za większy rozmiar (atrybut opcjonalny)

  W ten sposób `<Prices />` może posiadać od jednego do maksymalnie 3 potomków.

Strukturę dokumentu XML wykorzystanego w API bardziej szczegółowo określa poniższy schemat XSD:

```xml
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema">
 <xs:element name="Menu">
  <xs:complexType>
   <xs:sequence>
    <xs:element ref="Category" minOccurs="0" maxOccurs="unbounded" />
   </xs:sequence>
  </xs:complexType>
 </xs:element>

 <xs:element name="Category">
  <xs:complexType>
   <xs:sequence>
    <xs:element ref="Product" minOccurs="0" maxOccurs="unbounded" />
   </xs:sequence>
   <xs:attribute type="xs:int" name="id" use="required"/>
   <xs:attribute type="xs:string" name="name" use="required"/>
  </xs:complexType>
 </xs:element>

 <xs:element name="Product">
  <xs:complexType>
   <xs:sequence>
    <xs:element name="Name" maxOccurs="1" type="xs:string" />
    <xs:element ref="Prices" maxOccurs="1" />
   </xs:sequence>
   <xs:attribute type="xs:int" name="id" use="required"/>
   <xs:attribute type="xs:boolean" name="vegetarian" />
  </xs:complexType>
 </xs:element>

 <xs:element name="Prices">
  <xs:complexType>
   <xs:sequence>
    <xs:element ref="Price" minOccurs="1" maxOccurs="3" />
   </xs:sequence>
  </xs:complexType>
 </xs:element>

 <xs:element name="Price">
  <xs:complexType>
   <xs:simpleContent>
     <xs:extension base="xs:decimal">
    <xs:attribute name="type" use="required">
     <xs:simpleType>
      <xs:restriction base="xs:string">
       <xs:enumeration value="small" />
       <xs:enumeration value="medium" />
       <xs:enumeration value="large" />
      </xs:restriction>
     </xs:simpleType>
    </xs:attribute>
     </xs:extension>
   </xs:simpleContent>
   </xs:complexType>
 </xs:element>
</xs:schema>

```

### 3.2. API

## 2. Funkcjonalności udostępnione przez API

## 3. Typy danych oraz metody udostępnione w ramach API

## 4. Implementacja API

## 5. Testy jednostkowe

## 6. Podsumowanie, wnioski
