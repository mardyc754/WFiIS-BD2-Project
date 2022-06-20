USE Project;
GO

CREATE XML SCHEMA COLLECTION dbo.MenuSchema AS '
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
';
